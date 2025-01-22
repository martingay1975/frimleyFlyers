namespace FF.DataEntry.Dto
{
    public class ParkrunRun
    {
        public const string FRIMLEYLODGE_EVENTNAME = "Frimley Lodge";
        public const string ROTHERVALLEY = "Rother Valley";

        public string Event { get; set; }
        public DateTime Date { get; set; }
        public int EventNo { get; set; }
        public int Position { get; set; }
        public TimeSpan RaceTime { get; set; }
        public decimal AgeGrading { get; set; }
        public bool Pb { get; set; }
    }
}
