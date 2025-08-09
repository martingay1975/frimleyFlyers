using FF.DataEntry.Utils;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using static FF.DataEntry.Api.Stats;

namespace FF.DataEntry.Api
{
    internal static class TopTrumps
    {
        public static async Task BuildTopTrump(this AthleteStats athleteStats, string basePath)
        {
            string outputPath = Path.Combine(basePath, "topTrumps", $"{athleteStats.Name}.html");

            Dictionary<string, string> replacements = new Dictionary<string, string>
            {
                {"Value1", athleteStats.ParkrunsCount.ToString()},
                {"Value2", athleteStats.FrimleyLodgeCount.ToString()},
                {"Value3", athleteStats.FirstParkrun},
                {"Value4", athleteStats.PB.ToString()},
                {"Value5", athleteStats.PBDate},
                {"Value6", athleteStats.PBLocation},
                {"Value7", athleteStats.ParkrunVenueCount.ToString()},
                {"Value8", $"{athleteStats.Alphabeteer}%"},
                {"Value9", athleteStats.NendyParkrun},
                {"Value10", $"{athleteStats.NendyDistanceMiles} miles"},
                {"Value11", athleteStats.NendyClosestNCompleted.ToString()},
                {"Value12", athleteStats.FurthestParkrun},
                {"Value13", $"{athleteStats.FurthestDistanceMiles} miles"},
                {"Value14", athleteStats.InternationalsCount.ToString()},
                {"Value15", athleteStats.InternationalBreakdown},
                {"Value16", $"{athleteStats.StopWatchBingo}%"},
                {"Value17", $"{athleteStats.CompassClub}%" },
                {"Value18", athleteStats.WilsonIndex.ToString()},
                {"Value19", athleteStats.WilsonFloatingIndex.ToString()},
                {"Value20", athleteStats.LastParkrunTime.ToString()},
                {"Value21", athleteStats.LatestRunIsQuickestSince},
                {"Value22", athleteStats.BestTimeCurrentYear.ToString()},
                {"Value23", athleteStats.BestLocationCurrentYear},
            };

            using Stream? resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("FF.DataEntry.Resources.topTrump.html");
            using StreamReader streamReader = new StreamReader(resource);
            string template = streamReader.ReadToEnd();

            // Pattern matches Value1, Value2, ..., ValueN
            string pattern = @"Value\d+";
            string result = Regex.Replace(template, pattern, match =>
            {
                string key = match.Value;
                return replacements.TryGetValue(key, out string? replacement) ? replacement : key;
            });

            string html = result.Replace("Alex Parkrunner", HttpUtility.HtmlEncode(athleteStats.Name));
            html = html.Replace("https://via.placeholder.com/120", $"../athletesPic/{athleteStats.Name}.jpg");

            File.WriteAllText(outputPath, html);

            await TakePhoto(outputPath);

        }

        private static async Task TakePhoto(string path)
        {
            using (PuppeteerHelper puppeteer = new PuppeteerHelper())
            {
                await puppeteer.StartAsync();
                await puppeteer.OpenAsync(path, async (page, response) =>
                {
                    await page.ScreenshotAsync(path + ".jpg", new PuppeteerSharp.ScreenshotOptions() { FullPage = true});
                });
            }
        }
    }
}
