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
            List<AthleteStats> rows = GetStats(athletes);
            IOrderedEnumerable<AthleteStats> orderedByQuickest = rows.OrderByDescending(pr => pr.ParkrunsCount);
            ProduceStats(orderedByQuickest, Path.Combine(basePath, "byParkrunCount.csv"));
        }

        public static void GetByMilestone(List<Athlete> athletes, string basePath)
        {
            List<AthleteStats> rows = GetStats(athletes);
            HashSet<int> milestoneCounts = new HashSet<int> { 47, 48, 49, 97, 98, 99, 198, 199, 247, 248, 249, 298, 299, 398, 399, 497, 498, 499, 598, 599, 698, 699 };
            List<AthleteStats> milestoneClose = rows.Where(row => milestoneCounts.Contains(row.ParkrunsCount))
                .OrderBy(pr => pr.ParkrunsCount)
                .ToList();
            ProduceStats(milestoneClose, Path.Combine(basePath, "byMilestone.csv"));
        }

        private static List<AthleteStats> GetStats(IEnumerable<Athlete> athletes)
        {
            List<AthleteStats> rows = new List<AthleteStats>();
            foreach (Athlete athlete in athletes)
            {
                ParkrunRun quickestParkurnLastYear = athlete.GetQuickestParkrun(2024);
                ParkrunRun quickestParkrun = athlete.GetQuickestParkrun();
                ParkrunRun? latestRunIsQuickestSince = athlete.LatestRunIsQuickestSince();
                HashSet<string> parkRunEvents = athlete.GetParkrunEvents();
                (int index, int floating) wilsonIndex = WilsonIndex(athlete.ParkrunRunList);
                if (quickestParkurnLastYear != null)
                {
                    AthleteStats athleteStats = new()
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
                        FrimleyLodgeCount = FrimleyLodgeCount(athlete.ParkrunRunList),
                        PB = quickestParkrun.RaceTime,
                        PBDate = quickestParkrun.Date.ToShortDateString(),
                        PBLocation = quickestParkrun.Event,
                        InternationalsCount = GetInternationalCountries(athlete.ParkrunRunList).Count(),
                        Internationals = Internationals(athlete.ParkrunRunList),
                        WilsonIndex = wilsonIndex.index,
                        WilsonFloatingIndex = wilsonIndex.floating,
                        LatestRunIsQuickestSince = latestRunIsQuickestSince?.Date.ToShortDateString() ?? string.Empty,
                    };

                    (athleteStats.NendyParkrun, athleteStats.NendyDistanceMiles, athleteStats.NendyClosestNCompleted, athleteStats.FurthestParkrun, athleteStats.FurthestDistanceMiles) = GetDistanceBasedStats(parkRunEvents);
                    rows.Add(athleteStats);
                }
            }

            return rows;
        }


        private static (int index, int floating) WilsonIndex(List<ParkrunRun> parkrunList)
        {
            IOrderedEnumerable<int> eventNumbers = parkrunList.Select(parkrun => parkrun.EventNo).ToHashSet<int>().OrderBy(x => x);
            int longestStreak = 0;
            int currentStreak = 0;
            int consecutiveNumber = 0;
            bool isFirstStreak = eventNumbers.First() == 1;
            int firstStreak = 0;

            foreach (int eventNumber in eventNumbers)
            {
                if (eventNumber == consecutiveNumber + 1)
                {
                    currentStreak++;
                }
                else
                {
                    if (isFirstStreak)
                    {
                        firstStreak = currentStreak;
                        isFirstStreak = false;
                    }

                    longestStreak = Math.Max(longestStreak, currentStreak);
                    currentStreak = 0;
                }

                consecutiveNumber = eventNumber;
            }

            return (firstStreak, longestStreak);
        }

        private static (string nendyParkrun, int nendyDistanceMiles, int nendyClosestNCompleted, string furthestParkrun, int furthestDistanceMiles) GetDistanceBasedStats(IEnumerable<string> parkrunEventsDone)
        {
            const double metresToMiles = 0.000621371d;
            HashSet<string> parkrunEventDoneHashset = parkrunEventsDone.ToHashSet();
            OrderedDictionary<string, double> dontTouch = ParkrunLocations.GetDistanceFrom(ParkrunLocation.FRIMLEYLODGE_EVENTNAME);

            (string parkrun, double distance, int index) closeParkrunNotDone = GetNendy();

            KeyValuePair<string, double> furtherParkrunRun = dontTouch.Last(kvp => parkrunEventDoneHashset.Contains(kvp.Key));
            return (nendyParkrun: closeParkrunNotDone.parkrun,
                nendyDistanceMiles: (int)Math.Round(closeParkrunNotDone.distance * metresToMiles, 0),
                nendyClosestNCompleted: closeParkrunNotDone.index,
                furthestParkrun: furtherParkrunRun.Key,
                furthestDistanceMiles: (int)Math.Round(furtherParkrunRun.Value * metresToMiles, 0));

            (string parkrun, double distance, int index) GetNendy()
            {
                for (int index = 0; index < dontTouch.Keys.Count; index++)
                {
                    KeyValuePair<string, double> kvp = dontTouch.ElementAt(index);
                    if (!parkrunEventDoneHashset.Contains(kvp.Key))
                    {
                        return (kvp.Key, kvp.Value, index + 1);
                    }
                }

                throw new Exception("Couldn't find a parkrun not done! Impossible.");
            }
        }

        private static int FrimleyLodgeCount(List<ParkrunRun> parkrunList) =>
            parkrunList.Where(parkrun => parkrun.Event == ParkrunLocation.FRIMLEYLODGE_EVENTNAME).Count();
        

        private static int Alphabeteer(HashSet<string> events)
        {
            HashSet<char> hashSetOfChars = events.Select(eventName => char.ToLower(eventName[0])).ToHashSet();
            return GetPercentage(hashSetOfChars.Count, 25); // X is not counted!
        }

        private static IEnumerable<string> GetInternationalCountries(List<ParkrunRun> parkrunRunList)
            => parkrunRunList.Select(pr => ParkrunLocations.GetCountry(pr.Event)).Where(country => country != Country.CountryCodes[97] && country != Country.CountryCodes[0]);

        private static string Internationals(List<ParkrunRun> parkrunRunList)
        {
            IEnumerable<string> countriesAndTheirCounts = GetInternationalCountries(parkrunRunList)
                            .GroupBy(country => country)
                            .Select(group => $"{group.Key}={group.Count()}");

            return string.Join(", ", countriesAndTheirCounts);
        }

        private static int StopWatchBingo(IEnumerable<ParkrunRun> parkruns)
        {
            HashSet<int> seconds = parkruns.Select(parkrun => parkrun.RaceTime.Seconds).ToHashSet();
            return GetPercentage(seconds.Count, 60);
        }

        private static int CompassClub(HashSet<string> events)
        {
            List<string> beginWiths = new List<string> { "North", "South", "West", "East" };
            foreach (string prEvent in events)
            {
                foreach (string beginWith in beginWiths)
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
            CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header again.
                HasHeaderRecord = true,
            };

            using (StreamWriter writer = new StreamWriter(outputPath))
            using (CsvWriter csvWriter = new CsvWriter(writer, config))
            {
                csvWriter.WriteRecords(rows);
            }
        }
        private static int GetPercentage(int count, int total)
        {
            decimal percentage = (count / (decimal)total) * 100;
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
            public int FrimleyLodgeCount { get; set; }
            public string NendyParkrun { get; set; }
            public int NendyDistanceMiles { get; set; }
            public int NendyClosestNCompleted { get; set; }
            public string FurthestParkrun { get; set; }
            public int FurthestDistanceMiles { get; set; }
            public int InternationalsCount { get; set; }
            public string Internationals { get; set; }
            public int WilsonIndex { get; set; }
            public int WilsonFloatingIndex { get; set; }
            public string LatestRunIsQuickestSince {get; set;}
        }
    }
}
