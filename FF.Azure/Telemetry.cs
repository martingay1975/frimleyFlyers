using System.Collections.Generic;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace FF.Azure
{
    internal class Telemetry
    {
        public void AddSome()
        {
            var telemetryConfiguration = TelemetryConfiguration.CreateDefault();
            telemetryConfiguration.InstrumentationKey = "88bc27f3-783b-41f5-9bba-71519058a707";     // MG's account on Highlight
            var telemetryClient = new TelemetryClient(telemetryConfiguration);

            var r = new Dictionary<string, string> { { "key1", "val1" }, { "key2", "val2" } };
            telemetryClient.TrackTrace("Some string", SeverityLevel.Information);
        }
    }
}
