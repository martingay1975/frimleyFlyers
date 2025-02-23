namespace FF.DataEntry.Api
{
    internal static class CsvOutput
    {
        internal class BestInYear
        {
            public string Name { get; set; }
            public TimeSpan Time { get; set; }
            public string Location { get; set; }
            public string Date { get; set; }
            public int ParkrunsCount { get; set; }

        }
    }
}
