namespace FF.DataEntry.Api
{
    public class Manager
    {
        public RaceManager? RaceManager { get; private set; }
        public RecordsManager? RecordsManager { get; private set; }

        public async Task InitAsync(string filePath)
        {
            Root root = await RaceData.ReadAsync(filePath);
            if (root == null)
            {
                throw new Exception($"Unable to set the root object from '{filePath}'");
            }

            var raceFinder = new Finder(root);
            this.RaceManager = new RaceManager(root, raceFinder);
            this.RecordsManager = new RecordsManager(root.Records);
        }

        public async Task Save(string filePath)
        {
            if (this.RaceManager == null)
            {
                throw new NullReferenceException(nameof(RaceManager));
            }

            await RaceData.WriteAsync(this.RaceManager.Root, filePath);
        }
    }
}
