namespace FF.DataEntry.Dto
{
    public class RaceEvent
    {
        public RaceEvent()
        {
            ResetResults();
        }

        public int[] Date { get; set; }
        public int? Distance { get; set; }
        public List<RacePersonTime>? Results { get; set; }

        public void SetDate(DateTime date) => Date = new int[] { date.Year, date.Month - 1, date.Day };
        public DateTime GetDate() => new DateTime(Date[0], Date[1] + 1, Date[2]);

        public void ResetResults()
        {
            Results = new List<RacePersonTime>();
        }
    }
}