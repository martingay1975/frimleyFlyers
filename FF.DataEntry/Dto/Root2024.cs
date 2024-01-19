namespace FF.DataEntry.Dto
{
    public class Root2024 : Root
    {
        public int TakeNBestFLPScores { get; set; }
        public int TakeNBestTouristScores { get; set; }

        public static List<DateTime> Dates = new List<DateTime>()
        {
            new DateTime(2024, 1, 27),
            new DateTime(2024, 2, 24),
            new DateTime(2024, 3, 30),
            new DateTime(2024, 4, 27),
            new DateTime(2024, 5, 25),
            new DateTime(2024, 6, 29),
            new DateTime(2024, 7, 27),
            new DateTime(2024, 8, 31),
            new DateTime(2024, 9, 28),
            new DateTime(2024, 10,26),
            new DateTime(2024, 11,30)
        };

        public Root2024() : base()
        {
        }

        public static Root2024 CreateDefault()
        {
            var root = new Root2024()
            {
                PointsPbBonus = 5,
                EntryCost = 10,
            };

            root.Races = Root2024.Dates.Select(ffDate => CreateSingleEventRace($"parkrun " + ffDate.ToString("MMM d"), ffDate, RaceDistance.FiveKm)).ToList();
            return root;
        }
    }
}