using FF.DataEntry.Dto;

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

        public IEnumerable<Race> FindRacesContainLabel(string label)
        {
            var races = Root.Races.Where(race => race.Label.Contains(label));
            return races.Any() ? races : throw new Exception($"Unable to find any races {label}");
        }

        public RaceEvent FindEvent(string raceLabel, DateTime date)
        {
            var race = FindRace(raceLabel);
            var raceEvent = race.Events.SingleOrDefault(raceEvent => raceEvent.GetDate() == date);
            return raceEvent ?? throw new Exception($"Unable to find the event with {date}");
        }

        public IEnumerable<RaceEvent> FindEventsContainLabel(string raceLabel)
        {
            var races = FindRacesContainLabel(raceLabel);
            var t = races.SelectMany(raceEvent => raceEvent.Events);
            return t;
        }

        public IEnumerable<string> GetRaces()
        {
            return this.Root.Races.Select(race => race.Label);
        }

        public IEnumerable<DateTime> GetEvents(string raceLabel)
        {
            var race = FindRace(raceLabel);
            return race.Events.Select(raceEvent => raceEvent.GetDate());
        }

        public IEnumerable<RaceEvent> GetAllEvents()
        {
            return this.Root.Races.SelectMany(race => race.Events);
        }
    }
}
