using System.Text.Json.Serialization;

namespace FF.DataEntry.Dto
{
    public class RacePersonTime
    {
        public RacePersonTime()
        {
            Name = String.Empty;
            Time = new Time(TimeSpan.Zero);
            Notes = null;
        }

        public RacePersonTime(string name, TimeSpan timespan, string? notes = null)
        {
            Name = name;
            Time = new Time(timespan);
            Notes = notes;
        }

        public string Name { get; set; }

        public Time Time { get; set; }

        public string? Notes { get; set; }
    }
}
