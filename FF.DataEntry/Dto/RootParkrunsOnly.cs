namespace FF.DataEntry.Dto
{
    public class RootParkrunsOnly : Root
    {
        public int Year { get; private set; }
        public int TakeNBestHomeScores { get; set; }
        public int TakeNBestTouristScores { get; set; }

        public List<DateTime> Dates => GetLastSaturdays(Year);

        public static RootParkrunsOnly CreateDefault(int year)
        {
            var root = new RootParkrunsOnly
            {
                Year = year,
                PointsPbBonus = 5,
                EntryCost = 10,
            };

            root.Races = root.Dates.Select(ffDate => CreateSingleEventRace($"parkrun " + ffDate.ToString("MMM d"), ffDate, RaceDistance.FiveKm)).ToList();
            return root;
        }

        private static List<DateTime> GetLastSaturdays(int year)
        {
            List<DateTime> lastSaturdays = new List<DateTime>();

            for (int month = 1; month <= 11; month++)
            {
                // Get the last day of the month
                var lastDayOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month), 0, 0, 0, DateTimeKind.Utc);

                // Find the last Saturday by going backwards from the last day of the month
                var daysToLastSaturday = (int)lastDayOfMonth.DayOfWeek - (int)DayOfWeek.Saturday;
                if (daysToLastSaturday < 0)
                    daysToLastSaturday += 7;

                var lastSaturday = lastDayOfMonth.AddDays(-daysToLastSaturday);
                lastSaturdays.Add(lastSaturday);
            }

            return lastSaturdays;
        }
    }
}