using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

// Data source from:
// https://images.parkrun.com/events.json

public static class ParkrunLocationJson
{
    public static async Task<ParkrunData?> Get()
    {
        const string address = @"https://images.parkrun.com/events.json";
        using var httpClient = new HttpClient();
        try
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var ret = await httpClient.GetFromJsonAsync<ParkrunData>(address).ConfigureAwait(false);
            return ret;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}

public class ParkrunData
{
    [JsonPropertyName("countries")]
    public Dictionary<int, Country> Countries { get; set; }

    [JsonPropertyName("events")]
    public EventCollection Events { get; set; }
}

public class Country
{
    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("bounds")]
    public List<double> Bounds { get; set; }
}

public class EventCollection
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("features")]
    public List<EventFeature> Features { get; set; }
}

public class EventFeature
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("geometry")]
    public Geometry Geometry { get; set; }

    [JsonPropertyName("properties")]
    public EventProperties Properties { get; set; }
}

public class Geometry
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("coordinates")]
    public List<double> Coordinates { get; set; }

    public double Logitude => Coordinates[0];
    public double Lattitude => Coordinates[1];
}

public class EventProperties
{
    [JsonPropertyName("eventname")]
    public string EventName { get; set; }

    [JsonPropertyName("EventLongName")]
    public string EventLongName { get; set; }

    [JsonPropertyName("EventShortName")]
    public string EventShortName { get; set; }

    [JsonPropertyName("LocalisedEventLongName")]
    public string LocalisedEventLongName { get; set; }

    [JsonPropertyName("countrycode")]
    public int CountryCode { get; set; }

    [JsonPropertyName("seriesid")]
    public int SeriesId { get; set; }

    [JsonPropertyName("EventLocation")]
    public string EventLocation { get; set; }
}
