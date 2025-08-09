
using FF.DataEntry.Utils;
using System.Diagnostics;
using System.Text.Json;

namespace FF.DataEntry.Dto
{
    public static class AthleteExtension
    {
        public static IEnumerable<ParkrunRun> GetParkrunListInDate(this Athlete athlete, int? year = null)
        {
            if (year == null)
            {
                return athlete.ParkrunRunList;
            }
            else
            {
                DateTime startTime = new DateTime(year.Value, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                DateTime endTime = new DateTime(year.Value, 12, 31, 23, 59, 59, DateTimeKind.Utc);
                return athlete.ParkrunRunList.Where(parkrunRun => parkrunRun.Date >= startTime && parkrunRun.Date <= endTime);
            }
        }

        public static IEnumerable<ParkrunRun> GetParkrunListInDate(this Athlete athlete, DateTime startTime, DateTime endTime)
        {
            return athlete.ParkrunRunList.Where(parkrunRun => parkrunRun.Date >= startTime && parkrunRun.Date <= endTime);
        }

        public static ParkrunRun GetQuickestParkrun(this Athlete athlete, int? year = null) =>
             athlete.GetParkrunListInDate(year).OrderBy(parkrunRun => parkrunRun.RaceTime).First();


        public static IEnumerable<ParkrunRun> GetOrderedByDateDescending(this Athlete athlete)
            => athlete.ParkrunRunList.OrderByDescending(parkrunRun => parkrunRun.Date);
        

        public static ParkrunRun? LatestRunIsQuickestSince(this Athlete athlete)
        {
            bool first = true;
            ParkrunRun latestParkrun = null;
            foreach(ParkrunRun? parkrunRun in athlete.GetOrderedByDateDescending())
            {
                if (first)
                {
                    latestParkrun = parkrunRun;
                    first = false;
                }
                else if (latestParkrun?.RaceTime > parkrunRun.RaceTime)
                {
                    return parkrunRun;
                }
            }

            return null;
        }

        public static HashSet<string> GetParkrunEvents(this Athlete athlete) =>
            athlete.ParkrunRunList
                .Select(parkrunRun => parkrunRun.Event)
                .ToHashSet();

        public static async Task PopulateParkrunListAsync(this Athlete athlete, string athletesPath, bool getFromParkrunSite)
        {
            Debug.WriteLine($"{athlete.Name} - Processing athlete");
            string athletePath = Path.Combine(athletesPath, athlete.Name + ".json");
            if (File.Exists(athletePath) && !getFromParkrunSite)
            {
                // just load the file that is already on disk
                using (FileStream stream = File.OpenRead(athletePath))
                {
                    try
                    {
                        Athlete? loadedAthlete = await JsonSerializer.DeserializeAsync<Athlete>(stream, JsonSerializerDefaultOptions.Options);
                        athlete.ParkrunRunList = loadedAthlete?.ParkrunRunList.Where(parkrun => parkrun.Event.IndexOf("junior") == -1).ToList() ?? throw new InvalidOperationException();
                    }
                    catch (Exception)
                    {
                        Debug.WriteLine($"{athlete.Name} - Getting parkrun data from disk (not parkrun site) {athletePath}.");
                        throw;
                    }
                }
                Debug.WriteLine($"{athlete.Name} - Got parkrun data from disk (not parkrun site). {athlete.ParkrunRunList.Count} parkruns");
            }
            else
            {
                // Do some scraping
                if (!string.IsNullOrEmpty(athlete.ParkrunId))
                {
                    ParkrunWebsite parkrunWebsite = new ParkrunWebsite();
                    Debug.WriteLine($"{athlete.Name} - Going to parkrun website to get data");
                    athlete.ParkrunRunList = await parkrunWebsite.GetAllAsync(athlete.ParkrunId).ConfigureAwait(false);

                    // save results locally so don't need to scrape again.... soon anyway.
                    if (File.Exists(athletePath))
                    {
                        File.Delete(athletePath);
                    }

                    using (FileStream stream = File.OpenWrite(athletePath))
                    {
                        await JsonSerializer.SerializeAsync(stream, athlete, JsonSerializerDefaultOptions.Options);

                    }
                    Debug.WriteLine($"{athlete.Name} - Got parkrun data");
                }
            }
        }
    }
}
