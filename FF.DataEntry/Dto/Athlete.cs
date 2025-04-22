using FF.DataEntry.Api;

namespace FF.DataEntry.Dto
{
    public class Athlete
    {
        public Athlete()
        {
            this.Name = string.Empty;
            this.ParkrunRunList = new List<ParkrunRun>();
            this.HomePakrunName = ParkrunLocation.FRIMLEYLODGE_EVENTNAME;
        }

        public string Name { get; set; }
        public string? StravaId { get; set; }
        public string? ParkrunId { get; set; }
        public string HomePakrunName { get; set; }
        public List<ParkrunRun> ParkrunRunList { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
