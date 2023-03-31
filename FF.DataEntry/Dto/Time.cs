namespace FF.DataEntry.Dto
{
    public class Time
    {
        public Time()
        {

        }

        public Time(TimeSpan timeSpan)
        {
            SetTime(timeSpan);
        }

        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }

        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(Hour, Minute, Second);
        }

        public void SetTime(TimeSpan timespan)
        {
            Hour = timespan.Hours;
            Minute = timespan.Minutes;
            Second = timespan.Seconds;
        }
    }
}
