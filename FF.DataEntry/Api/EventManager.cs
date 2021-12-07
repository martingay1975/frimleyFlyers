namespace FF.DataEntry.Api
{
    internal class EventManager
    {
        public EventManager(Root root)
        {
            Root = root;
        }

        private Root Root { get; }

        public RaceEvent CreateEvent(DateTime date, RaceDistance raceDistance)
        {
            var raceEvent = new RaceEvent() {  Distance = (int)raceDistance, Results = new List<Dto.RacePersonTime>() };
            raceEvent.SetDate(date);
            return raceEvent;
        }

        public void AddResult()
        {

        }
    }
}
