namespace FF.DataEntry.Api
{
    public class RaceManager
    {
        public Root Root { get; }
        private Finder RaceFinder { get; }

        public RaceManager(Root root, Finder raceFinder)
        {
            Root = root;
            RaceFinder = raceFinder;
        }

        public void Update(string oldLabel, string newLabel, RaceDistance newRaceDistance, string newWebsite, string newIcon)
        {
            var race = RaceFinder.FindRace(oldLabel);
            race.Label = newLabel;
            race.Distance = newRaceDistance;
            race.Website = newWebsite;
            race.Icon = newIcon;
        }

        public void AddEvent(string label, RaceEvent newEvent)
        {
            var race = RaceFinder.FindRace(label);
            race.Events.Add(newEvent);
        }
    }
}
