//namespace FF.DataEntry.Dto
//{
//    public class Root2023 : Root
//    {
//        public int TakeNBestFLPScores { get; set; }
//        public int TakeNBestTouristScores { get; set; }

//        public static List<DateTime> Dates2023Events = new List<DateTime>()
//        {
//            new DateTime(2023, 1, 28),
//            new DateTime(2023, 2, 25),
//            new DateTime(2023, 3, 25),
//            new DateTime(2023, 4, 29),
//            new DateTime(2023, 5, 27),
//            new DateTime(2023, 6, 24),
//            new DateTime(2023, 7, 29),
//            new DateTime(2023, 8, 26),
//            new DateTime(2023, 9, 30),
//            new DateTime(2023, 10,28),
//            new DateTime(2023, 11,25)
//        };


//        public Root2023() : base()
//        {
//        }

//        public static Root2023 CreateDefault()
//        {
//            var root2023 = new Root2023()
//            {
//                PointsPbBonus = 5,
//                EntryCost = 5,
//            };

//            root2023.Races = Root2023.Dates2023Events.Select(ffDate => CreateSingleEventRace($"parkrun " + ffDate.ToString("MMM d"), ffDate, RaceDistance.FiveKm)).ToList();
//            return root2023;
//        }
//    }
//}