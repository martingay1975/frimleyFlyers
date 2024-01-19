using FF.DataEntry.Dto;
using FF.DataEntry.Utils;

namespace FF.DataEntry.Api
{
    public partial class Manager
    {
        public const int Year = 2024;
        private Root2024 root;
        private Finder RaceFinder;
        private string basePath;

        public RaceManager? RaceManager { get; private set; }
        public RecordsManager? RecordsManager { get; private set; }
        public AthletesManager AthletesManager { get; private set; }

        public string GetBasePath(string seasonFilePath)
        {
            var fileInfo = new FileInfo(seasonFilePath);
            this.basePath = fileInfo.Directory?.FullName ?? throw new InvalidOperationException();
            return this.basePath;
        }

        public async Task CreateNewAsync(string seasonFilePath, Func<Task>? updateParkrunFor5kmTimes)
        {
            GetBasePath(seasonFilePath);
            this.AthletesManager = new AthletesManager();

            this.root = Root2024.CreateDefault();
            this.RaceFinder = new Finder(this.root);
            this.RaceManager = new RaceManager(this.RaceFinder);
            this.RecordsManager = new RecordsManager(this.root.Records);

            if (updateParkrunFor5kmTimes != null)
            {
                await updateParkrunFor5kmTimes.Invoke();
            }

            await RecordsManager.PopulateWithAthletesAsync(this.AthletesManager, Year - 1);
        }

        public async Task InitAsync(string seasonFilePath)
        {
            GetBasePath(seasonFilePath);
            this.root = await RaceDataSerializer<Root2024>.ReadAsync(seasonFilePath);
            if (this.root == null)
            {
                throw new Exception($"Unable to set the root object from '{seasonFilePath}'");
            }

            var raceFinder = new Finder(this.root);
            this.RaceManager = new RaceManager(raceFinder);
            this.RecordsManager = new RecordsManager(this.root.Records);
            this.RaceFinder = new Finder(this.root);

            this.AthletesManager = new AthletesManager();
            var seasonsAthletes = this.RecordsManager.Records.Select(record => record.Name).ToList();
            await this.AthletesManager.PopulateWithParkrunListAsync(this.basePath, seasonsAthletes, false);

            //CalculateParkrunTourist();
            //CalculateFLPNovember();
            var overallPositions = CalculateCurrentYear();
            CsvOutput.ProduceCSV(root, overallPositions, seasonFilePath + ".csv");
        }


        public async Task SaveAsync(string filePath)
        {
            if (this.RaceManager == null)
            {
                throw new NullReferenceException(nameof(RaceManager));
            }

            await RaceDataSerializer<Root2024>.WriteAsync(this.root, filePath);
        }

        public class OverallScores
        {
            public string Name { get; set; }
            public int OverallPoints { get; set; }
            public int FLPPoints { get; set; }
            public int TouristPoints { get; set; }
            public Time? BaseLineTime { get; set; }
        }


        public List<OverallScores> CalculateCurrentYear()
        {
            var scores = new List<OverallScores>();

            // Loops around each race
            foreach (var ffRace in this.root.Races)
            {
                var raceEvent = ffRace.Events[0];
                raceEvent.ResetResults();

                // Loops around each person
                foreach (var record in this.RecordsManager.Records)
                {
                    var athlete = this.AthletesManager.FindAthleteByName(record.Name);

                    // get all the parkruns for the athlete for the specific dates.
                    var raceEventDate = raceEvent.GetDate();
                    var athleteCurrentYearParkrunForDate = this.AthletesManager.GetParkrunInDate(athlete.ParkrunRunList, raceEventDate, raceEventDate).FirstOrDefault();
                    if (athleteCurrentYearParkrunForDate == null)
                    {
                        // The athlete has not run any parkrun on this qualifying date, so can skip onto the next athlete.
                        continue;
                    }

                    var athlete5kmPB = record.FiveKm.GetTimeSpan();
                    // We have the athlete, we have the parkrun data and we have the FF event.
                    var isFlp = athleteCurrentYearParkrunForDate.Event == ParkrunRun.FRIMLEYLODGE_EVENTNAME;
                    var comment = isFlp ? null : $"{athleteCurrentYearParkrunForDate.Event}"; // no need for a comment on FLP
                    var racePersonTime = new RacePersonScoreTime(athlete.Name, athleteCurrentYearParkrunForDate.RaceTime, athlete5kmPB, isFlp, comment);
                    raceEvent.Results?.Add(racePersonTime);
                }

                // Get the athletes in order, with best PctDifference at the top
                var results = raceEvent.Results?
                    .Cast<RacePersonScoreTime>()
                    .OrderBy(res => res.PctDifference).ToList();

                for (var index = 0; index < results?.Count; index++)
                {
                    var racePersonScoreTime = results[index];
                    var points = index < this.root.PointsScheme.Count() ? this.root.PointsScheme[index] : 0;
                    racePersonScoreTime.SetPoints(points, this.root.PointsPbBonus);
                    racePersonScoreTime.Position = index + 1;
                }
            }

            // Now get the best 5 FLP events and 2 Tourist events
            var events = this.RaceFinder.GetAllEvents();
            foreach (var record in this.RecordsManager.Records)
            {
                // for each athlete
                var flp = new List<RacePersonScoreTime>();
                var tourist = new List<RacePersonScoreTime>();
                foreach (var evt in events)
                {
                    var athleteForEvent = evt?.Results?.FirstOrDefault(racePersonScoreTime => racePersonScoreTime.Name == record.Name) as RacePersonScoreTime;
                    if (athleteForEvent != null)
                    {
                        if (athleteForEvent.IsFlp)
                        {
                            flp.Add(athleteForEvent);
                        }
                        else
                        {
                            tourist.Add(athleteForEvent);
                        }
                    }
                }

                var flpPoints = process(flp, 5);
                var touristPoints = process(tourist, 2);
                scores.Add(
                    new OverallScores
                    {
                        Name = record.Name,
                        FLPPoints = flpPoints,
                        TouristPoints = touristPoints,
                        OverallPoints = flpPoints + touristPoints,
                        BaseLineTime = record.FiveKm
                    });
            }

            // get the overall result in order of total points
            var orderedOverallScores = scores
                .OrderByDescending(sc => sc.OverallPoints)
                .ThenBy(sc => sc.Name).ToList();

            return orderedOverallScores;


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

        public void CalculateResults()
        {
            var events = this.RaceFinder.GetAllEvents();
            var results2023 = new Results2023();
            foreach (var evt in events)
            {
                var eventResults = new EventResults(evt);

            }
        }

        public class Results2023
        {
            public Results2023()
            {
                this.AllEventResults = new List<EventResults>();
            }

            public List<EventResults> AllEventResults { get; set; }
        }

        public class EventResults
        {
            public EventResults(RaceEvent raceEvent)
            {
                RaceEvent = raceEvent;
                Results = new List<RacePersonScoreTime>();
            }

            public RaceEvent RaceEvent { get; private set; }
            public List<RacePersonScoreTime> Results { get; private set; }
        }


        public void CalculateFLPNovember()
        {
            const string raceEvent = "FLP November";
            var parkrunNovemer1stEvent = new DateTime(2022, 11, 5);

            // found the event - effectively scrub whats there and recalculate.
            for (var currentDate = parkrunNovemer1stEvent; currentDate < new DateTime(2022, 12, 1); currentDate = currentDate.AddDays(7))
            {
                var parkrunRaceEvent = this.RaceFinder.FindEvent(raceEvent, currentDate);
                if (parkrunRaceEvent == null)
                {
                    throw new ArgumentNullException(nameof(parkrunRaceEvent));
                }

                parkrunRaceEvent.ResetResults();
                foreach (var record in this.RecordsManager.Records)
                {
                    var name = record.Name;
                    var quickestTourestForYear = this.AthletesManager.GetFrimleyLodgeQuickest(name, currentDate, currentDate);
                    if (quickestTourestForYear != null)
                    {
                        var racePersonTime = new RacePersonTime(name, quickestTourestForYear.RaceTime, $"{quickestTourestForYear.Event} - {quickestTourestForYear.Date:d}");
                        parkrunRaceEvent.Results.Add(racePersonTime);
                    }
                }
            }
        }

        public void CalculateParkrunTourist()
        {
            const string raceEvent = "parkrun Tourist";
            var parkrunTouristDate = new DateTime(2022, 2, 5);
            var parkrunRaceEvent = this.RaceFinder.FindEvent(raceEvent, parkrunTouristDate);

            // found the event - effectively scrub whats there and recalculate.
            parkrunRaceEvent.ResetResults();
            foreach (var record in this.RecordsManager.Records)
            {
                var name = record.Name;
                var quickestTourestForYear = this.AthletesManager.GetTouristQuickest(name, new DateTime(Year, 2, 1), new DateTime(Year, 10, 30));
                if (quickestTourestForYear != null)
                {
                    var racePersonTime = new RacePersonTime(name, quickestTourestForYear.RaceTime, $"{quickestTourestForYear.Event} - {quickestTourestForYear.Date:d}");
                    parkrunRaceEvent.Results.Add(racePersonTime);
                }
            }
        }
    }
}
