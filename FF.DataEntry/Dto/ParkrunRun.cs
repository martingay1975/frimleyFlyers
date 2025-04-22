namespace FF.DataEntry.Dto
{
    public class ParkrunRun
    {
        public string Event { get; set; }
        public DateTime Date { get; set; }
        public int EventNo { get; set; }
        public int Position { get; set; }
        public TimeSpan RaceTime { get; set; }
        public decimal AgeGrading { get; set; }
        public bool Pb { get; set; }
    }
}
