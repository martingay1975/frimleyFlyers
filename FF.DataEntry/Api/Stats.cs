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
                    rows.Add(new AthleteStats
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
                        PBLocation = quickestParkrun.Event
                    });
                }
            }

            return rows;
        }

        private static int Alphabeteer(HashSet<string> events)
        {
            var hashSetOfChars = events.Select(eventName => char.ToLower(eventName[0])).ToHashSet();
            return GetPercentage(hashSetOfChars.Count, 25); // X is not counted!
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
        }

    }
}
