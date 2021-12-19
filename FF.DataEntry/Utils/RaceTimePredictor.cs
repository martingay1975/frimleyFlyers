

public class RaceTimePredictor
{
    private const string url = "https://assets.hearstapps.com/rwtools/race-time-predictor.html";

    public static TimeSpan GetPredictor(TimeSpan fiveKm)
    {
        // frace = target race, option =  marathon | half | 10m | 10k | 5m | 5k
        // r1 = recent race, option =  marathon | half | 10m | 10k | 5m | 5k
        // r1t_hours r1t_minutes r1t_seconds = recent race hours, minutes, seconds

        // calculate button class = "form-submit"
        // results has a div with id = "results"

        var r ="<div id=\"results\" style=\"\"><p>Your predicted <b>10k</b> time is <b>42:48</b> With a pace of <b>6:53/mile</b> or <b>4:17/km</b> <a href=\"#\" id=\"rs\">Revise</a></p></div>";

        return ParseResults(r);
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