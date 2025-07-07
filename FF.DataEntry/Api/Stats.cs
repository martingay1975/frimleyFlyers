using CsvHelper;
using CsvHelper.Configuration;
using FF.DataEntry.Dto;
using System.Globalization;

namespace FF.DataEntry.Api
{
    public static class Stats
    {
        public static void GetByTotalParkruns(List<Athlete> athletes, string basePath)
        {
            var rows = GetStats(athletes);
            var orderedByQuickest = rows.OrderByDescending(pr => pr.ParkrunsCount);
            ProduceStats(orderedByQuickest, Path.Combine(basePath, "byParkrunCount.csv"));
        }

        public static void GetByMilestone(List<Athlete> athletes, string basePath)
        {
            var rows = GetStats(athletes);
            var milestoneCounts = new HashSet<int> { 47, 48, 49, 97, 98, 99, 198, 199, 247, 248, 249, 298, 299, 398, 399, 497, 498, 499, 598, 599, 698, 699 };
            var milestoneClose = rows.Where(row => milestoneCounts.Contains(row.ParkrunsCount))
                .OrderBy(pr => pr.ParkrunsCount)
                .ToList();
            ProduceStats(milestoneClose, Path.Combine(basePath, "byMilestone.csv"));
        }

        private static List<AthleteStats> GetStats(IEnumerable<Athlete> athletes)
        {
            var rows = new List<AthleteStats>();
            foreach (var athlete in athletes)
            {
                var quickestParkurnLastYear = athlete.GetQuickestParkrun(2024);
                var quickestParkrun = athlete.GetQuickestParkrun();
                var parkRunEvents = athlete.GetParkrunEvents();
                if (quickestParkurnLastYear != null)
                {
                    var athleteStats = new AthleteStats
                    {
                        Name = athlete.Name,
                        BestTime2024 = quickestParkurnLastYear.RaceTime,
                        BestLocation2024 = quickestParkurnLastYear.Event,
                        ParkrunsCount = athlete.ParkrunRunList.Count,
                        ParkrunVenueCount = parkRunEvents.Count,
                        Alphabeteer = Alphabeteer(parkRunEvents),
                        StopWatchBingo = StopWatchBingo(athlete.ParkrunRunList),
                        CompassClub = CompassClub(parkRunEvents),
                        FirstParkrun = (athlete.ParkrunRunList.OrderBy(pr => pr.Date).First()).Date.ToShortDateString(),
                        HomeFor2025 = athlete.HomePakrunName,
                        PB = quickestParkrun.RaceTime,
                        PBDate = quickestParkrun.Date.ToShortDateString(),
                        PBLocation = quickestParkrun.Event,
                        InternationalsCount = GetInternationalCountries(athlete.ParkrunRunList).Count(),
                        Internationals = Internationals(athlete.ParkrunRunList)
                    };

                    (athleteStats.NendyParkrun, athleteStats.NendyDistanceMiles, athleteStats.NendyClosestNCompleted, athleteStats.FurthestParkrun, athleteStats.FurthestDistanceMiles) = GetDistanceBasedStats(parkRunEvents);
                    rows.Add(athleteStats);
                }
            }

            return rows;
        }

        private static (string nendyParkrun, int nendyDistanceMiles, int nendyClosestNCompleted, string furthestParkrun, int furthestDistanceMiles) GetDistanceBasedStats(IEnumerable<string> parkrunEventsDone)
        {
            const double metresToMiles = 0.000621371d;
            var parkrunEventDoneHashset = parkrunEventsDone.ToHashSet();
            OrderedDictionary<string, double> dontTouch = ParkrunLocations.GetDistanceFrom(ParkrunLocation.FRIMLEYLODGE_EVENTNAME);

            var closeParkrunNotDone = GetNendy();

            var furtherParkrunRun = dontTouch.Last(kvp => parkrunEventDoneHashset.Contains(kvp.Key));
            return (nendyParkrun: closeParkrunNotDone.parkrun,
                nendyDistanceMiles: (int)Math.Round(closeParkrunNotDone.distance * metresToMiles, 0),
                nendyClosestNCompleted: closeParkrunNotDone.index,
                furthestParkrun: furtherParkrunRun.Key,
                furthestDistanceMiles: (int)Math.Round(furtherParkrunRun.Value * metresToMiles, 0));

            (string parkrun, double distance, int index) GetNendy()
            {
                for (var index = 0; index < dontTouch.Keys.Count; index++)
                {
                    var kvp = dontTouch.ElementAt(index);
                    if (!parkrunEventDoneHashset.Contains(kvp.Key))
                    {
                        return (kvp.Key, kvp.Value, index + 1);
                    }
                }

                throw new Exception("Couldn't find a parkrun not done! Impossible.");
            }
        }

        private static int Alphabeteer(HashSet<string> events)
        {
            var hashSetOfChars = events.Select(eventName => char.ToLower(eventName[0])).ToHashSet();
            return GetPercentage(hashSetOfChars.Count, 25); // X is not counted!
        }

        private static IEnumerable<string> GetInternationalCountries(List<ParkrunRun> parkrunRunList)
            => parkrunRunList.Select(pr => ParkrunLocations.GetCountry(pr.Event)).Where(country => country != Country.CountryCodes[97] && country != Country.CountryCodes[0]);

        private static string Internationals(List<ParkrunRun> parkrunRunList)
        {
            var countriesAndTheirCounts = GetInternationalCountries(parkrunRunList)
                            .GroupBy(country => country)
                            .Select(group => $"{group.Key}={group.Count()}");

            return string.Join(", ", countriesAndTheirCounts);
        }

        private static int StopWatchBingo(IEnumerable<ParkrunRun> parkruns)
        {
            var seconds = parkruns.Select(parkrun => parkrun.RaceTime.Seconds).ToHashSet();
            return GetPercentage(seconds.Count, 60);
        }

        private static int CompassClub(HashSet<string> events)
        {
            var beginWiths = new List<string> { "North", "South", "West", "East" };
            foreach (var prEvent in events)
            {
                foreach (var beginWith in beginWiths)
                {
                    if (prEvent.StartsWith(beginWith))
                    {
                        beginWiths.Remove(beginWith);
                        break;
                    }
                }
            }

            return 100 - GetPercentage(beginWiths.Count, 4);
        }

        private static void ProduceStats(IEnumerable<AthleteStats> rows, string outputPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header again.
                HasHeaderRecord = true,
            };

            using (var writer = new StreamWriter(outputPath))
            using (var csvWriter = new CsvWriter(writer, config))
            {
                csvWriter.WriteRecords(rows);
            }
        }
        private static int GetPercentage(int count, int total)
        {
            var percentage = (count / (decimal)total) * 100;
            return (int)Math.Round(percentage, MidpointRounding.AwayFromZero);
        }

        public class AthleteStats
        {
            public string Name { get; set; }
            public TimeSpan BestTime2024 { get; set; }
            public string BestLocation2024 { get; set; }
            public int ParkrunsCount { get; set; }
            public int ParkrunVenueCount { get; set; }
            public int Alphabeteer { get; set; }
            public int StopWatchBingo { get; set; }
            public int CompassClub { get; set; }
            public string FirstParkrun { get; set; }
            public string HomeFor2025 { get; set; }
            public TimeSpan PB { get; set; }
            public string PBDate { get; set; }
            public string PBLocation { get; set; }
            public string NendyParkrun { get; set; }
            public int NendyDistanceMiles { get; set; }
            public int NendyClosestNCompleted { get; set; }
            public string FurthestParkrun { get; set; }
            public int FurthestDistanceMiles { get; set; }

            public int InternationalsCount { get; set; }
            public string Internationals { get; set; }

        }
    }
}
