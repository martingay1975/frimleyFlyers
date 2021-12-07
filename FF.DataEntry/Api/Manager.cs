namespace FF.DataEntry.Api
{
    public class Manager
    {
        public RaceManager? RaceManager { get; private set; }

        public async Task InitAsync(string filePath)
        {
            var root = await RaceData.ReadAsync(filePath);
            if (root == null)
            {
                throw new Exception($"Unable to set the root object from '{filePath}'");
            }

            var raceFinder = new Finder(root);
            this.RaceManager = new RaceManager(raceFinder);
        }
    }
}
