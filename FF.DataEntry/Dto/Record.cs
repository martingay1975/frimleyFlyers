using FF.DataEntry.Dto;

namespace FF.DataEntry
{
    public class Record
    {
        public Record(string name)
        {
            this.Name = name;
            this.FiveKm = new Time();
            this.TenKm = new Time();
            this.TenMiles = new Time();
            this.HalfMarathon = new Time();
        }

        public string Name { get; set; }
        public Time FiveKm { get; set; }
        public Time TenKm { get; set; }
        public Time TenMiles { get; set; }
        public Time HalfMarathon { get; set; }
    }
}