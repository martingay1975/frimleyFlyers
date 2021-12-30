using FF.DataEntry.Dto;
using FF.DataEntry.Utils;

namespace FF.DataEntry.Api
{
    public class Manager
    {
        public const int Year = 2022;

        private Root root;
        public RaceManager? RaceManager { get; private set; }
        public RecordsManager? RecordsManager { get; private set; }
        public AthletesManager AthletesManager { get; private set; }

        private string basePath;

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
            await this.AthletesManager.PopulateWithParkrunList(this.basePath, false);

            this.root = Root.CreateDefault(Year);
            var raceFinder = new Finder(this.root);
            this.RaceManager = new RaceManager(raceFinder);
            this.RecordsManager = new RecordsManager(this.root.Records);
            await RecordsManager.PopulateWithAthletesAsync(this.AthletesManager, Year);
        }

        public async Task InitAsync(string seasonFilePath)
        {
            GetBasePath(seasonFilePath);
            this.AthletesManager = new AthletesManager();
            await this.AthletesManager.PopulateWithParkrunList(this.basePath, false);

            this.root = await RaceData.ReadAsync(seasonFilePath);
            if (this.root == null)
            {
                throw new Exception($"Unable to set the root object from '{seasonFilePath}'");
            }

            var raceFinder = new Finder(this.root);
            this.RaceManager = new RaceManager(raceFinder);
            this.RecordsManager = new RecordsManager(this.root.Records);
        }

        public async Task SaveAsync(string filePath)
        {
            if (this.RaceManager == null)
            {
                throw new NullReferenceException(nameof(RaceManager));
            }

            await RaceData.WriteAsync(this.root, filePath);
        }
    }
}
