using CsvHelper;
using CsvHelper.Configuration;
using FF.DataEntry.Dto;
using System.Globalization;
using static FF.DataEntry.Api.League;

namespace FF.DataEntry.Api
{
    internal static class LeagueCsv
    {
        internal static void OneEventCsv(List<RacePersonScoreTime> raceEventResults, string outputPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header again.
                HasHeaderRecord = false,
            };

            using var writer = new StreamWriter(outputPath);
            using var csvWriter = new CsvWriter(writer, config);

            // Row 1
            csvWriter.WriteField("Position");
            csvWriter.WriteField("Name");
            csvWriter.WriteField("Location");
            csvWriter.WriteField("Time");
            csvWriter.WriteField("%");
            csvWriter.WriteField("Pts");
            csvWriter.NextRecord();

            foreach (var raceEventResult in raceEventResults)
            {
                csvWriter.WriteField(raceEventResult.Position);
                csvWriter.WriteField(raceEventResult.Name);
                csvWriter.WriteField(raceEventResult.IsHome ? "Home" : raceEventResult.Notes);
                csvWriter.WriteField(raceEventResult.Time.GetTimeSpan().ToString(@"mm\:ss") + "\t");
                csvWriter.WriteField(string.Format("{0:N2}%", raceEventResult.PctDifference));
                csvWriter.WriteField(raceEventResult.Points);
                csvWriter.NextRecord();
            }

        }

        private static int GetLastEventRaced(List<RaceEvent> allEvents)
        {
            for (var idx = 0; idx < allEvents.Count; idx++)
            {
                if (!HasRaceBeenRun(idx))
                {
                    return idx - 1;
                }
            }
            throw new Exception("No races run");

            // local function
            bool HasRaceBeenRun(int raceIdx) => allEvents[raceIdx]?.Results?.Find(res => res.Time.HasValue)?.Time.HasValue ?? false;
        }

        internal static void WholeSeasonLastRaceSpotligtCsv(Root root, List<OverallScores> overallPositions, string outputPath)
        {
            var finder = new Finder(root);
            var allEvents = finder.GetAllEvents().ToList();
            var spotlightEventIdx = GetLastEventRaced(allEvents);

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
                    csvWriter.WriteField("");
                    for (var evtIndex = 0; evtIndex <= spotlightEventIdx; evtIndex++)
                    {
                        csvWriter.WriteField($"{allEvents[evtIndex].GetDate().ToString("MMMM")}");
                    }

                    csvWriter.NextRecord();

                    // Row 2
                    csvWriter.WriteField("Name");
                    csvWriter.WriteField("Pts");
                    csvWriter.WriteField("Home Pts");
                    csvWriter.WriteField("Tourist Pts");
                    csvWriter.WriteField("Prev Best");

                    // Loop around each of the events
                    for (var evtIndex = 0; evtIndex <= spotlightEventIdx; evtIndex++)
                    {
                        csvWriter.WriteField("Pts");
                    }

                    csvWriter.NextRecord();

                    // Loop around each athlete
                    foreach (var overallScore in overallPositions)
                    {
                        csvWriter.WriteField(overallScore.Name);
                        csvWriter.WriteField(overallScore.OverallPoints);
                        csvWriter.WriteField(overallScore.HomePoints);
                        csvWriter.WriteField(overallScore.TouristPoints);
                        csvWriter.WriteField(overallScore.BaseLineTime?.GetTimeSpan().ToString(@"mm\:ss") + "\t");

                        for (var evtIndex = 0; evtIndex <= spotlightEventIdx; evtIndex++)
                        {
                            var evt = allEvents[evtIndex];
                            var racePersonScoreTime = evt?.Results?.Find(rc => rc.Name == overallScore.Name) as RacePersonScoreTime;
                            csvWriter.WriteField(racePersonScoreTime?.Points.ToString(CultureInfo.InvariantCulture) ?? "");
                        }

                        csvWriter.NextRecord();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        internal static void WholeSeasonCsv(Root root, List<OverallScores> overallPositions, string outputPath)
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
                    csvWriter.WriteField("Home Pts");
                    csvWriter.WriteField("Tourist Pts");
                    csvWriter.WriteField("Prev Best");

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
                        csvWriter.WriteField(overallScore.HomePoints);
                        csvWriter.WriteField(overallScore.TouristPoints);
                        csvWriter.WriteField(overallScore.BaseLineTime?.GetTimeSpan().ToString(@"mm\:ss") + "\t");

                        for (var evtIndex = 0; evtIndex < allEvents.Count(); evtIndex++)
                        {
                            var evt = allEvents[evtIndex];
                            var racePersonScoreTime = evt.Results.FirstOrDefault(rc => rc.Name == overallScore.Name) as RacePersonScoreTime;

                            if (racePersonScoreTime != null)
                            {
                                csvWriter.WriteField(racePersonScoreTime.IsHome ? "Home PR" : racePersonScoreTime.Notes);
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
            catch (Exception)
            {
            }
        }

    }
}
