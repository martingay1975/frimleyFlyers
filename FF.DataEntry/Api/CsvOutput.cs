using CsvHelper;
using CsvHelper.Configuration;
using FF.DataEntry.Dto;
using System.Globalization;
using static FF.DataEntry.Api.Manager;

namespace FF.DataEntry.Api
{
    internal static class CsvOutput
    {
        internal static void ProduceCSV2023(Root2023 root, List<OverallScores> overallPositions, string outputPath)
        {
            var finder = new Finder(root);
            var allEvents = finder.GetAllEvents().ToList();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header again.
                HasHeaderRecord = false,

            };

            try
            {
                using (var writer = new StreamWriter(outputPath))
                using (var csvWriter = new CsvWriter(writer, config))
                {
                    // Row 1
                    csvWriter.WriteField("");
                    csvWriter.WriteField("Overall");
                    csvWriter.WriteField("");
                    csvWriter.WriteField("");
                    for (var evtIndex = 0; evtIndex < allEvents.Count(); evtIndex++)
                    {
                        csvWriter.WriteField($"{allEvents[evtIndex].GetDate().ToString("MMMM")}");
                        csvWriter.WriteField("");
                        csvWriter.WriteField("");
                        csvWriter.WriteField("");
                    }
                    csvWriter.NextRecord();

                    // Row 2
                    csvWriter.WriteField("Name");
                    csvWriter.WriteField("Pts");
                    csvWriter.WriteField("FLP Pts");
                    csvWriter.WriteField("Tourist Pts");

                    for (var evtIndex = 0; evtIndex < allEvents.Count(); evtIndex++)
                    {
                        csvWriter.WriteField("Location");
                        csvWriter.WriteField("Time");
                        csvWriter.WriteField("%");
                        csvWriter.WriteField("Pts");
                    }
                    csvWriter.NextRecord();

                    foreach (var overallScore in overallPositions)
                    {
                        csvWriter.WriteField(overallScore.Name);
                        csvWriter.WriteField(overallScore.OverallPoints);
                        csvWriter.WriteField(overallScore.FLPPoints);
                        csvWriter.WriteField(overallScore.TouristPoints);

                        for (var evtIndex = 0; evtIndex < allEvents.Count(); evtIndex++)
                        {
                            var evt = allEvents[evtIndex];
                            var racePersonScoreTime = evt.Results.FirstOrDefault(rc => rc.Name == overallScore.Name) as RacePersonScoreTime;

                            if (racePersonScoreTime != null)
                            {
                                csvWriter.WriteField(racePersonScoreTime.IsFlp ? "FLP" : racePersonScoreTime.Notes);
                                csvWriter.WriteField(racePersonScoreTime?.Time.GetTimeSpan().ToString(@"mm\:ss") + "\t");
                                csvWriter.WriteField(string.Format("{0:N2}%", racePersonScoreTime?.PctDifference));
                                csvWriter.WriteField(racePersonScoreTime?.Points);
                            }
                            else
                            {
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                                csvWriter.WriteField("");
                            }
                        }
                        csvWriter.NextRecord();
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public class OutputRaceDto
        {
            public TimeSpan TimeEvent1 { get; set; }
            public double PctDifferentEvent1 { get; set; }
            public int PointsEvent1 { get; set; }
        }

        //public class OutputDtoMap : ClassMap<OutputDto>
        //{
        //    public OutputDtoMap()
        //    {
        //        Map(outputDto => outputDto.Name);
        //        Map(outputDto => outputDto.Races);
        //        foreach (var outputRaceDto in outputDto.Races)
        //    }
        //}

        //public class OutputDto
        //{
        //    public string Name { get; set; }
        //    public List<OutputRaceDto> Races { get; set; }
        //    public int FlpPoints { get; set; }
        //    public int TouristPoints { get; set; }
        //    public int TotalPoints { get; set; }


        //    //public TimeSpan TimeEvent2 { get; set; }
        //    //public double PctDifferentEvent2 { get; set; }
        //    //public int PointsEvent2 { get; set; }

        //    //public TimeSpan TimeEvent3 { get; set; }
        //    //public double PctDifferentEvent3 { get; set; }
        //    //public int PointsEvent3 { get; set; }


        //}
    }
}
