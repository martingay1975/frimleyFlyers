namespace FF.DataEntry.Api
{
    public class Finder
    {
        public Finder(Root root)
        {
            Root = root;
        }

        private Root Root { get; }

        public Race FindRace(string label)
        {
            var race = Root.Races.SingleOrDefault(race => race.Label == label);
            return race ?? throw new Exception($"Unable to find the race {label}");
        }

        public RaceEvent FindEvent(string label, DateTime date)
        {
            var race = FindRace(label);
            var raceEvent = race.Events.SingleOrDefault(raceEvent => raceEvent.GetDate() == date);
            return raceEvent ?? throw new Exception($"Unable to find the event with {date}");
        }
    }
}
