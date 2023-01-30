namespace FF.DataEntry.Dto
{
    public class Root2023 : Root
    {
        public const string labelFlp = "FLP";
        public const string labelTourist = "Tourist";

        public int TakeNBestFLPScores { get; set; }
        public int TakeNBestTouristScores { get; set; }

        public static List<DateTime> Dates2023Events = new List<DateTime>()
        {
            new DateTime(2023, 1, 28),
            new DateTime(2023, 2, 25),
            new DateTime(2023, 3, 25),
            new DateTime(2023, 4, 29),
            new DateTime(2023, 5, 27),
            new DateTime(2023, 6, 24),
            new DateTime(2023, 7, 29),
            new DateTime(2023, 8, 26),
            new DateTime(2023, 9, 30),
            new DateTime(2023, 10,28),
            new DateTime(2023, 11,25)
        };


        public Root2023() : base()
        {
        }

        public static Root2023 CreateDefault()
        {
            var root2023 = new Root2023()
            {
                PointsPbBonus = 5,
                EntryCost = 5,
            };

            foreach (var ffDate in Root2023.Dates2023Events)
            {
                var dateString = ffDate.ToString("MMM d");
                var flpRace = CreateSingleEventRace($"{labelFlp} {dateString}", ffDate, RaceDistance.FiveKm);
                var touristRace = CreateSingleEventRace($"{labelTourist} {dateString}", ffDate, RaceDistance.FiveKm);
                root2023.Races.Add(flpRace);
                root2023.Races.Add(touristRace);
            }
            return root2023;
        }
    }

    public class Root
    {
        public Root()
        {
            LeagueTableColumnsToShow = new List<int>();
            PointsScheme = new List<int>();
            Records = new List<Record>();
            Races = new List<Race>();
            PointsScheme.AddRange(new[] { 21, 17, 14, 12, 10, 8, 6, 4, 2, 1 });
        }

        public List<int> LeagueTableColumnsToShow { get; set; }
        public int TakeNBestScores { get; set; }
        public List<int> PointsScheme { get; set; }
        public int PointsPbBonus { get; set; }
        public double EntryCost { get; set; }
        public List<Record> Records { get; set; }
        public List<Race> Races { get; set; }

        public static Root CreateDefault(int year)
        {
            var root = new Root()
            {
                TakeNBestScores = 5,
                PointsPbBonus = 5,
                EntryCost = 10,
            };

            var touristParkrun = CreateTouristParkrun(year);
            var wokinghamHalfMarathon = CreateSingleEventRace("Wokingham Half", new DateTime(year, 2, 27), RaceDistance.HalfMarathon);
            var frimleyParkHosp = CreateSingleEventRace("Frimley Park Hospital", new DateTime(year, 5, 1), RaceDistance.TenKm);
            var yateleySeries = CreateYateleySeries(year);
            var greatSouthRun = CreateSingleEventRace("Great South Run", new DateTime(year, 10, 1), RaceDistance.TenMiles);
            var fleet = CreateSingleEventRace("Fleet OR Rushmoor 10km", new DateTime(year, 10, 1), RaceDistance.TenKm);
            var frimleyLodgeNovember = CreateFrimleyLodgeNovemberParkrun(year);

            root.Races.AddRange(new[] { touristParkrun, wokinghamHalfMarathon, frimleyParkHosp, yateleySeries, greatSouthRun, fleet, frimleyLodgeNovember });

            return root;
        }

        public static Race CreateSingleEventRace(string label, DateTime date, RaceDistance raceDistance)
        {
            var race = new Race()
            {
                Label = label,
                Distance = raceDistance
            };

            var raceEvent = new RaceEvent() { Distance = (int)raceDistance };
            raceEvent.SetDate(date);
            race.Events.Add(raceEvent);
            return race;
        }

        private static Race CreateYateleySeries(int year)
        {
            var race = new Race()
            {
                Label = "Yateley Series",
                Distance = RaceDistance.TenKm
            };

            var raceEvent1 = new RaceEvent();
            raceEvent1.SetDate(new DateTime(year, 6, 1));
            raceEvent1.Distance = (int)RaceDistance.FiveKm;

            var raceEvent2 = new RaceEvent();
            raceEvent2.SetDate(new DateTime(year, 7, 6));
            raceEvent2.Distance = (int)RaceDistance.FiveKm;

            var raceEvent3 = new RaceEvent();
            raceEvent3.SetDate(new DateTime(year, 8, 3));
            raceEvent3.Distance = (int)RaceDistance.FiveKm;

            race.Events.AddRange(new[] { raceEvent1, raceEvent2, raceEvent3 });
            return race;
        }

        private static Race CreateTouristParkrun(int year)
        {
            var race = new Race
            {
                Label = "FLP Tourist",
                Distance = RaceDistance.FiveKm
            };

            var currentDate = new DateTime(year, 2, 5);
            while (currentDate.Month < 11)
            {
                var raceEvent = new RaceEvent();
                raceEvent.SetDate(currentDate);
                raceEvent.Distance = (int)RaceDistance.FiveKm;
                race.Events.Add(raceEvent);

                currentDate = currentDate.AddDays(7);
            }

            return race;
        }

        public static Race CreateFrimleyLodgeNovemberParkrun(int year)
        {
            var race = new Race();
            race.Label = "FLP November";
            race.Distance = RaceDistance.FiveKm;

            var currentDate = new DateTime(year, 11, 5);
            while (currentDate.Month == 11)
            {
                var raceEvent = new RaceEvent();
                raceEvent.SetDate(currentDate);
                raceEvent.Distance = (int)RaceDistance.FiveKm;
                race.Events.Add(raceEvent);

                currentDate = currentDate.AddDays(7);
            }

            return race;
        }
    }
}