namespace FF.DataEntry.Dto
{
    public class RacePersonScoreTime : RacePersonTime
    {
        public RacePersonScoreTime(string name, TimeSpan raceTime, TimeSpan pb, bool isFlp = true, string? notes = null) : base(name, raceTime, notes)
        {
            IsFlp = isFlp;
            PctDifference = CalculatePercentageFromPb(pb, raceTime);
        }

        public double PctDifference { get; set; }
        public int Points { get; set; }
        public int Position { get; set; }
        public bool IsFlp { get; set; }
        public bool IsScoringPoints { get; set; }

        public void SetPoints(int points, int pbPoints)
        {
            Points = points;
            if (PctDifference < 0)
            {
                Points += pbPoints;
            }
        }

        public static double CalculatePercentageFromPb(TimeSpan pb, TimeSpan raceTime)
        {
            return ((raceTime.TotalSeconds / pb.TotalSeconds) - 1) * 100;
        }
    }
}
