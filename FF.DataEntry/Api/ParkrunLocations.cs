namespace FF.DataEntry.Api
{
    // https://images.parkrun.com/events.json

    public class ParkrunLocation
    {
        public const string FRIMLEYLODGE_EVENTNAME = "Frimley Lodge";
        public const string ROTHERVALLEY = "Rother Valley";

        public ParkrunLocation() { }

        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public List<string> Countries { get; set; }

        //
        // Summary:
        //     Returns the distance between the latitude and longitude coordinates that are
        //     specified by this System.Device.Location.GeoCoordinate and another specified
        //     System.Device.Location.GeoCoordinate.
        //
        // Parameters:
        //   other:
        //     The System.Device.Location.GeoCoordinate for the location to calculate the distance
        //     to.
        //
        // Returns:
        //     The distance between the two coordinates, in meters.
        public double GetDistanceTo(ParkrunLocation other)
        {
            if (double.IsNaN(Latitude) || double.IsNaN(Longitude) || double.IsNaN(other.Latitude) || double.IsNaN(other.Longitude))
            {
                throw new ArgumentException("Argument_LatitudeOrLongitudeIsNotANumber");
            }

            //double num = double.NaN;
            double num2 = Latitude * (Math.PI / 180.0);
            double num3 = Longitude * (Math.PI / 180.0);
            double num4 = other.Latitude * (Math.PI / 180.0);
            double num5 = other.Longitude * (Math.PI / 180.0);
            double num6 = num5 - num3;
            double num7 = num4 - num2;
            double num8 = Math.Pow(Math.Sin(num7 / 2.0), 2.0) + Math.Cos(num2) * Math.Cos(num4) * Math.Pow(Math.Sin(num6 / 2.0), 2.0);
            double num9 = 2.0 * Math.Atan2(Math.Sqrt(num8), Math.Sqrt(1.0 - num8));
            return 6376500.0 * num9;
        }
    }

    public class NendyData
    {
        public string Name { get; set; }
        public double Distance { get; set; }
    }


    public static class ParkrunLocations
    {
        public static async Task<List<ParkrunLocation>> Get()
        {
            //var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            //{
            //    HasHeaderRecord = false,
            //};

            //var assembly = Assembly.GetExecutingAssembly();
            //using var stream = assembly.GetManifestResourceStream("FF.DataEntry.uk-parkruns.csv");
            //using var reader = new StreamReader(stream);
            //using var csvReader = new CsvHelper.CsvReader(reader, config);

            //var locations = csvReader.GetRecords<ParkrunLocation>();
            //return locations.Where(location => !location.Name.ToLower().Contains(" junior")).ToList();

            var parkrunData = await ParkrunLocationJson.Get().ConfigureAwait(false);
            var ret = parkrunData?.Events?.Features
                .Where(feature => feature.Properties.SeriesId == 1)
                .Select(feature => new ParkrunLocation
                {
                    Name = feature.Properties.EventShortName,
                    Latitude = feature.Geometry.Lattitude,
                    Longitude = feature.Geometry.Logitude,
                })
                .ToList();

            return ret ?? [];
        }

        private static readonly Lazy<List<ParkrunLocation>> lazy = new Lazy<List<ParkrunLocation>>(() => Get().Result);

        public static List<ParkrunLocation> Instance { get { return lazy.Value; } }


        public static double GetDistanceFrom(string sourceParkrunName, string destinationParkrunName)
            => GetDistanceFrom(sourceParkrunName)[destinationParkrunName];

        public static OrderedDictionary<string, double> GetDistanceFrom(string parkrunName)
        {
            if (!AllDistances.ContainsKey(parkrunName))
            {
                var sourceParkrun = Instance.Find(pr => pr.Name == parkrunName);
                if (sourceParkrun != null)
                {
                    var sortedDistances = Instance
                        .ToDictionary(k => k.Name, v => sourceParkrun.GetDistanceTo(v))
                        .OrderBy(pair => pair.Value); // Sorting by distance

                    AllDistances[parkrunName] = new OrderedDictionary<string, double>(sortedDistances);
                }
            }

            return new OrderedDictionary<string, double>(AllDistances[parkrunName]);
        }

        private static Dictionary<string, OrderedDictionary<string, double>> AllDistances = [];
    }
}