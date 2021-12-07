using System.Text.Json.Serialization;

namespace FF.DataEntry.Dto
{
    public class RacePersonTime
    {
        public RacePersonTime()
        {
            Name = String.Empty;
            Time = new Time(TimeSpan.Zero);
        }

        public RacePersonTime(string name, TimeSpan timespan)
        {
            Name = name;
            Time = new Time(timespan);
        }

        public string Name { get; set; }

        public Time Time { get; set; }
    }
}
