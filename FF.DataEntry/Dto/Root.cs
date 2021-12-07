namespace FF.DataEntry
{
    public class Root
    {
        public Root()
        {
            LeagueTableColumnsToShow = new List<int>();
            PointsScheme = new List<int>();
            Records = new List<Record>();
            Races = new List<Race>();
        }

        public List<int> LeagueTableColumnsToShow { get; set; }
        public int TakeNBestScores { get; set; }
        public List<int> PointsScheme { get; set; }
        public int PointsPbBonus { get; set; }
        public double EntryCost { get; set; }
        public List<Record> Records { get; set; }
        public List<Race> Races { get; set; }
    }
}