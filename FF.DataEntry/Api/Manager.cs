using FF.DataEntry.Dto;
using FF.DataEntry.Utils;

namespace FF.DataEntry.Api
{
    public partial class Manager
    {
        public const int Year = 2025;
        private RootParkrunsOnly root;
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

        public void CreateFFLeagueCsv(string seasonFilePath)
        {
            League.CalculateTable(this.basePath, seasonFilePath, RecordsManager, root, AthletesManager, RaceFinder);
        }

        public async Task CreateNewAsync(string seasonFilePath, Func<Task>? updateParkrunFor5kmTimes)
        {
            GetBasePath(seasonFilePath);
            this.AthletesManager = new AthletesManager();
            await this.AthletesManager.InitAsync(basePath);

            this.root = RootParkrunsOnly.CreateDefault(Year);
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
            this.root = await RaceDataSerializer<RootParkrunsOnly>.ReadAsync(seasonFilePath);
            if (this.root == null)
            {
                throw new Exception($"Unable to set the root object from '{seasonFilePath}'");
            }

            var raceFinder = new Finder(this.root);
            this.RaceManager = new RaceManager(raceFinder);
            this.RecordsManager = new RecordsManager(this.root.Records);
            this.RaceFinder = new Finder(this.root);

            this.AthletesManager = new AthletesManager();
            await this.AthletesManager.InitAsync(this.basePath);
        }

        public async Task SaveAsync(string filePath)
        {
            if (this.RaceManager == null)
            {
                throw new NullReferenceException(nameof(RaceManager));
            }

            await RaceDataSerializer<RootParkrunsOnly>.WriteAsync(this.root, filePath);
        }
    }
}
