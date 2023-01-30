using FF.DataEntry.Dto;
using FF.DataEntry.Utils;

namespace FF.DataEntry.Api
{
    public class Manager
    {
        public const int Year = 2023;
        private Root2023 root;
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

            this.root = Root2023.CreateDefault();
            this.RaceFinder = new Finder(this.root);
            this.RaceManager = new RaceManager(this.RaceFinder);
            this.RecordsManager = new RecordsManager(this.root.Records);

            if (updateParkrunFor5kmTimes != null)
            {
                await updateParkrunFor5kmTimes.Invoke();
            }

            await RecordsManager.PopulateWithAthletesAsync(this.AthletesManager, Year);
        }

        public async Task InitAsync(string seasonFilePath)
        {
            GetBasePath(seasonFilePath);
            this.root = await RaceDataSerializer<Root2023>.ReadAsync(seasonFilePath);
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
            Calculate2023();
        }

        public async Task SaveAsync(string filePath)
        {
            if (this.RaceManager == null)
            {
                throw new NullReferenceException(nameof(RaceManager));
            }

            await RaceDataSerializer<Root2023>.WriteAsync(this.root, filePath);
        }

        public void Calculate2023()
        {
            foreach (var ffRace in this.root.Races)
            {
                var raceEvent = ffRace.Events[0];
                raceEvent.ResetResults();
                var isFlp = ffRace.Label.StartsWith(Root2023.labelFlp);

                foreach (var record in this.RecordsManager.Records)
                {
                    var athlete = this.AthletesManager.FindAthleteByName(record.Name);

                    // get all the parkruns for the athlete for the specific dates.
                    var raceEventDate = raceEvent.GetDate();
                    var athlete2023ParkrunForDate = this.AthletesManager.GetParkrunInDate(athlete.ParkrunRunList, raceEventDate, raceEventDate).FirstOrDefault();
                    if (athlete2023ParkrunForDate == null)
                    {
                        // The athlete has not run any parkrun on this qualifying date, so can skip onto the next athlete.
                        continue;
                    }

                    // We have the athlete, we have the parkrun data and we have the FF event.
                    var comment = isFlp ? "" : $"{athlete2023ParkrunForDate.Event}"; // no need for a comment on FLP
                    var racePersonTime = new RacePersonTime(athlete.Name, athlete2023ParkrunForDate.RaceTime, comment);
                    raceEvent.Results?.Add(racePersonTime);
                }
            }
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
