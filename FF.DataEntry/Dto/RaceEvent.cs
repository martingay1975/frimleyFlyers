using FF.DataEntry.Dto;

namespace FF.DataEntry
{
    public class RaceEvent
    {
        public RaceEvent()
        {

        }

        //public RaceEvent(DateTime date, RaceDistance raceDistance)
        //{
        //    SetDate(date);
        //    Distance = (int)raceDistance;
        //    Results = new List<RacePersonTime>();
        //}

        public int[] Date { get; set; }
        public int? Distance { get; set; }
        public List<RacePersonTime>? Results { get; set; }

        public void SetDate(DateTime date) => this.Date = new int[] { date.Year, date.Month -1, date.Day};
        public DateTime GetDate() => new DateTime(Date[0], Date[1] + 1, Date[2]);
    }
}