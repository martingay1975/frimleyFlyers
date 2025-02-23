using FF.DataEntry.Dto;
using FF.DataEntry.Utils;

namespace FF.DataEntry.Api
{
    internal static class League
    {
        public static void CalculateTable(string basePath, string seasonFilePath,
            RecordsManager recordsManager, Root root, AthletesManager athletesManager, Finder raceFinder)
        {
            var scores = new List<OverallScores>();

            // Loops around each race
            for (var raceEventNo = 0; raceEventNo < root.Races.Count; raceEventNo++)
            {
                var ffRace = root.Races[raceEventNo];
                var raceEvent = ffRace.Events[0];
                raceEvent.ResetResults();

                // Loops around each person
                foreach (var record in recordsManager.Records)
                {
                    var athlete = athletesManager.FindAthleteByName(record.Name);

                    // get all the parkruns for the athlete for the specific dates.
                    var raceEventDate = raceEvent.GetDate();
                    var athleteCurrentYearParkrunForDate = athlete.GetParkrunListInDate(raceEventDate, raceEventDate).FirstOrDefault();
                    if (athleteCurrentYearParkrunForDate == null)
                    {
                        // The athlete has not run any parkrun on this qualifying date, so can skip onto the next athlete.
                        continue;
                    }

                    var athlete5kmPB = record.FiveKm.GetTimeSpan();

                    // We have the athlete, we have the parkrun data and we have the FF event.
                    var isHome = athleteCurrentYearParkrunForDate.Event == athlete.HomePakrunName;
                    var comment = isHome ? null : $"{athleteCurrentYearParkrunForDate.Event}"; // no need for a comment on FLP
                    var racePersonTime = new RacePersonScoreTime(athlete.Name, athleteCurrentYearParkrunForDate.RaceTime, athlete5kmPB, isHome, comment);
                    raceEvent.Results?.Add(racePersonTime);
                }

                // Get the athletes in order, with best PctDifference for the season at the top
                if (raceEvent.Results == null || raceEvent.Results?.Count == 0)
                {
                    continue;
                }

                var results = raceEvent.Results
                    .Cast<RacePersonScoreTime>()
                    .OrderBy(res => res.PctDifference).ToList();

                for (var index = 0; index < results?.Count; index++)
                {
                    var racePersonScoreTime = results[index];
                    var points = index < root.PointsScheme.Count ? root.PointsScheme[index] : 0;
                    racePersonScoreTime.SetPoints(points, root.PointsPbBonus);
                    racePersonScoreTime.Position = index + 1;
                }

                // Now produce a CSV for the results of the particular event:
                LeagueCsv.OneEventCsv(results, $"{basePath}\\{raceEventNo + 1:00}-{ffRace.Label}.csv");
            }

            // Now get the best 5 Home events and 2 Tourist events
            var events = raceFinder.GetAllEvents();
            foreach (var record in recordsManager.Records)
            {
                // for each athlete
                var home = new List<RacePersonScoreTime>();
                var tourist = new List<RacePersonScoreTime>();
                foreach (var evt in events)
                {
                    var athleteForEvent = evt?.Results?.FirstOrDefault(racePersonScoreTime => racePersonScoreTime.Name == record.Name) as RacePersonScoreTime;
                    if (athleteForEvent != null)
                    {
                        if (athleteForEvent.IsHome)
                        {
                            home.Add(athleteForEvent);
                        }
                        else
                        {
                            tourist.Add(athleteForEvent);
                        }
                    }
                }

                var homePoints = process(home, 5);
                var touristPoints = process(tourist, 2);
                scores.Add(
                    new OverallScores
                    {
                        Name = record.Name,
                        HomePoints = homePoints,
                        TouristPoints = touristPoints,
                        OverallPoints = homePoints + touristPoints,
                        BaseLineTime = record.FiveKm
                    });
            }

            // get the overall result in order of total points
            var orderedOverallScores = scores
                .OrderByDescending(sc => sc.OverallPoints)
                .ThenBy(sc => sc.Name)
                .ToList();

            // Whole season, all stats
            LeagueCsv.WholeSeasonCsv(root, orderedOverallScores, $"{seasonFilePath}-all.csv");

            // Whole season, focus on the latest month
            LeagueCsv.WholeSeasonLastRaceSpotligtCsv(root, orderedOverallScores, $"{seasonFilePath}-spotlight.csv");

            // local functions
            int process(List<RacePersonScoreTime> list, int take)
            {
                var racePersonScoreTimes = list.OrderByDescending(racePersonScoreTime => racePersonScoreTime.Points).Take(take);
                foreach (var racePersonScoreTime in racePersonScoreTimes)
                {
                    racePersonScoreTime.IsScoringPoints = true;
                }

                return racePersonScoreTimes.Sum(racePersonScoreTime => racePersonScoreTime.Points);
            }
        }

        public class OverallScores
        {
            public string Name { get; set; }
            public int OverallPoints { get; set; }
            public int HomePoints { get; set; }
            public int TouristPoints { get; set; }
            public Time? BaseLineTime { get; set; }
        }
    }
}
