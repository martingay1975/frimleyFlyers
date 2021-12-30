

using FF.DataEntry;
using FF.DataEntry.Utils;

public class RaceTimePredictor
{
    private const string url = "https://assets.hearstapps.com/rwtools/race-time-predictor.html";

    private static string GetRaceDistanceValue(RaceDistance raceDistance)
    {
        switch (raceDistance)
        {
            case RaceDistance.FiveKm: return "5k";
            case RaceDistance.TenKm: return "10k";
            case RaceDistance.TenMiles: return "10m";
            case RaceDistance.HalfMarathon: return "half";
            default: throw new ArgumentOutOfRangeException(nameof(raceDistance));
        }
    }

    public async static Task<TimeSpan> GetPredictor(RaceDistance raceDistance, TimeSpan fiveKm)
    {
        TimeSpan ret = default(TimeSpan);
        using (var puppeteer = new PuppeteerHelper())
        {
            await puppeteer.StartAsync();
            await puppeteer.OpenAsync(url, async (page, response) =>
            {
                // select the option on the target race distance
                await puppeteer.SelectOptionAsync(page, "#frace", GetRaceDistanceValue(raceDistance));

                // select the base race distance of the submitted time
                await puppeteer.SelectOptionAsync(page, "#r1", GetRaceDistanceValue(RaceDistance.FiveKm));

                // enter the hours, minutes, seconds of the base race 
                await puppeteer.EnterTextAsync(page, "#r1t_hours", fiveKm.Hours.ToString());
                await puppeteer.EnterTextAsync(page, "#r1t_minutes", fiveKm.Minutes.ToString());
                await puppeteer.EnterTextAsync(page, "#r1t_seconds", fiveKm.Seconds.ToString());

                // click the 'Calculate' button.
                await puppeteer.HitButtonAsync(page, ".form-submit", true);

                // get the results
                var result = await puppeteer.GetInnerHtmlAsync(page, "#results");
                
                // parse the results html
                ret = ParseResults(result);
            });
        }

        return ret;
    }

    private static TimeSpan ParseResults(string value)
    {
        // <div id="results" style=""><p>Your predicted <b>10k</b> time is <b>42:48</b> With a pace of <b>6:53/mile</b> or <b>4:17/km</b> <a href="#" id="rs">Revise</a></p></div>
        var bolds = value.Split("<b>");
        if (bolds.Length < 3)
        {
            throw new Exception("Unknown results");
        }

        var boldTime = bolds[2];
        var endIndex = boldTime.IndexOf("</b>");
        var timeString = boldTime.Substring(0, endIndex);
        var timeParts = timeString.Split(":");
        if (timeParts.Length == 2)
        {
            timeString = $"0:{timeString}";
        }

        return TimeSpan.Parse(timeString);
    }
}