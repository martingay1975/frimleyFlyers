using FF.DataEntry.Dto;
using FF.DataEntry.Utils;

namespace FF.DataEntry.Api
{
    public class Manager
    {
        public const int Year = 2022;
        private Root root;
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

        public async Task CreateNewAsync(string seasonFilePath)
        {
            GetBasePath(seasonFilePath);
            this.AthletesManager = new AthletesManager();
            await this.AthletesManager.PopulateWithParkrunListAsync(this.basePath, false);

            this.root = Root.CreateDefault(Year);
            this.RaceFinder = new Finder(this.root);
            this.RaceManager = new RaceManager(this.RaceFinder);
            this.RecordsManager = new RecordsManager(this.root.Records);
            await RecordsManager.PopulateWithAthletesAsync(this.AthletesManager, Year);
        }

        public async Task InitAsync(string seasonFilePath)
        {
            GetBasePath(seasonFilePath);
            this.AthletesManager = new AthletesManager();
            await this.AthletesManager.PopulateWithParkrunListAsync(this.basePath, false);

            this.root = await RaceData.ReadAsync(seasonFilePath);
            if (this.root == null)
            {
                throw new Exception($"Unable to set the root object from '{seasonFilePath}'");
            }

            var raceFinder = new Finder(this.root);
            this.RaceManager = new RaceManager(raceFinder);
            this.RecordsManager = new RecordsManager(this.root.Records);
            this.RaceFinder = new Finder(this.root);

            CalculateParkrunTourist();
        }

        public async Task SaveAsync(string filePath)
        {
            if (this.RaceManager == null)
            {
                throw new NullReferenceException(nameof(RaceManager));
            }

            await RaceData.WriteAsync(this.root, filePath);
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
