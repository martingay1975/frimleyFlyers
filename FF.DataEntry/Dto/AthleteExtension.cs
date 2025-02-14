
using FF.DataEntry.Utils;
using System.Diagnostics;
using System.Text.Json;

namespace FF.DataEntry.Dto
{
    public static class AthleteExtension
    {
        public static IEnumerable<ParkrunRun> GetParkrunListInDate(this Athlete athlete, DateTime startTime, DateTime endTime)
        {
            return athlete.ParkrunRunList.Where(parkrunRun => parkrunRun.Date >= startTime && parkrunRun.Date <= endTime);
        }

        public static ParkrunRun GetQuickestParkrun(this Athlete athlete)
        {
            var selected = athlete.ParkrunRunList.OrderBy(parkrunRun => parkrunRun.RaceTime).First();
            return selected;
        }

        public static ParkrunRun GetQuickestParkrun(this Athlete athlete, int year)
        {
            var startDate = new DateTime(year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var endDate = new DateTime(year, 12, 31, 23, 59, 59, DateTimeKind.Utc);
            var parkrunsInSeasonForAthlete = athlete.GetParkrunListInDate(startDate, endDate);
            var selected = parkrunsInSeasonForAthlete.OrderBy(parkrunRun => parkrunRun.RaceTime).First();
            return selected;
        }

        public static HashSet<string> GetParkrunEvents(this Athlete athlete) =>
            athlete.ParkrunRunList
                .Select(parkrunRun => parkrunRun.Event)
                .ToHashSet();

        public static async Task PopulateParkrunListAsync(this Athlete athlete, string athletesPath, bool getFromParkrunSite)
        {
            Debug.WriteLine($"{athlete.Name} - Processing athlete");
            var athletePath = Path.Combine(athletesPath, athlete.Name + ".json");
            if (File.Exists(athletePath) && !getFromParkrunSite)
            {
                // just load the file that is already on disk
                using (var stream = File.OpenRead(athletePath))
                {
                    try
                    {
                        var loadedAthlete = await JsonSerializer.DeserializeAsync<Athlete>(stream, JsonSerializerDefaultOptions.Options);
                        athlete.ParkrunRunList = loadedAthlete?.ParkrunRunList ?? throw new InvalidOperationException();
                    }
                    catch (Exception ex)
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
                    var parkrunWebsite = new ParkrunWebsite();
                    Debug.WriteLine($"{athlete.Name} - Going to parkrun website to get data");
                    athlete.ParkrunRunList = await parkrunWebsite.GetAllAsync(athlete.ParkrunId).ConfigureAwait(false);

                    // save results locally so don't need to scrape again.... soon anyway.
                    using (var stream = File.OpenWrite(athletePath))
                    {
                        await JsonSerializer.SerializeAsync(stream, athlete, JsonSerializerDefaultOptions.Options);
                    }
                    Debug.WriteLine($"{athlete.Name} - Got parkrun data");
                }
            }
        }
    }
}
