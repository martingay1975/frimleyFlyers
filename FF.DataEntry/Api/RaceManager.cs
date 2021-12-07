﻿namespace FF.DataEntry.Api
{
    public class RaceManager
    {
        private Finder RaceFinder { get; }

        public RaceManager(Finder raceFinder)
        {
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