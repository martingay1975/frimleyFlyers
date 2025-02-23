using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System.Globalization;
using System.Reflection;

namespace FF.DataEntry.Api
{
    public class ParkrunLocation
    {
        public ParkrunLocation() { }

        [Index(0)]
        public string Name { get; set; }
        [Index(1)]
        public string LocationName { get; set; }
        [Index(2)]
        public double Longitude { get; set; }
        [Index(3)]
        public double Lattitude { get; set; }
    }

    public class ParkrunLocationMap : ClassMap<ParkrunLocation>
    {
        public ParkrunLocationMap()
        {
            Map(pr => pr.Name).Index(0);
            Map(pr => pr.LocationName).Index(1);
            Map(pr => pr.Longitude).Index(2);
            Map(pr => pr.Lattitude).Index(3);
        }
    }

    public static class ParkrunLocations
    {
        public static IList<ParkrunLocation> Get()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };


            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("FF.DataEntry.uk-parkruns.csv");
            using var reader = new StreamReader(stream);
            using var csvReader = new CsvHelper.CsvReader(reader, config);

            var locations = csvReader.GetRecords<ParkrunLocation>();
            return locations.Where(location => !location.Name.EndsWith("juniors")).ToList();
        }
    }
}