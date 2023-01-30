namespace FF.DataEntry.Dto
{
    public class Record
    {
        public Record(string name)
        {
            Name = name;
            FiveKm = new Time();
            TenKm = new Time();
            TenMiles = new Time();
            HalfMarathon = new Time();
        }

        public string Name { get; set; }
        public Time FiveKm { get; set; }
        public Time TenKm { get; set; }
        public Time TenMiles { get; set; }
        public Time HalfMarathon { get; set; }
    }
}