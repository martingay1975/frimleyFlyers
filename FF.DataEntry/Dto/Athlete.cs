
namespace FF.DataEntry.Dto
{
    public class Athlete
    {
        public Athlete()
        {
            this.Name = string.Empty;
        }

		public string Name { get; set; }
		public string? StravaId { get; set; }
		public string? ParkrunId { get; set; }
        public List<ParkrunRun> ParkrunRunList { get; set; }
    }
}
