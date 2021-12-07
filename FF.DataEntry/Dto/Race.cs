namespace FF.DataEntry
{
    public class Race
    {
        public Race()
        {
            Events = new List<RaceEvent>();
            Label = string.Empty;
            Distance = RaceDistance.FiveKm;
            Website = String.Empty;
            Icon = String.Empty;
        }

        public string Label { get; set; }
        public RaceDistance Distance { get; set; }
        public string Website { get; set; }
        public string Icon { get; set; }
        public List<RaceEvent> Events { get; set; }
    }
}