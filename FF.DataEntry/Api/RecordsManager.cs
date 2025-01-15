using FF.DataEntry.Dto;
using FF.DataEntry.Utils;

namespace FF.DataEntry.Api
{
    public class RecordsManager
    {
        public RecordsManager(List<Record> records)
        {
            Records = records;
        }
        public List<Record> Records { get; }

        public async Task PopulateWithAthletesAsync(AthletesManager athletesManager, int year)
        {
            this.Records.Clear();
            foreach (var athlete in athletesManager.Athletes)
            {
                var record = new Record(athlete.Name);

                TimeSpan fastestParkrun5km = TimeSpan.Zero;
                for (var yearToLook = year; yearToLook > 2010; yearToLook--)
                {
                    fastestParkrun5km = athletesManager.GetQuickestParkrunInYear(athlete, year)?.RaceTime ?? TimeSpan.Zero;
                    if (fastestParkrun5km != TimeSpan.Zero)
                    {
                        break;
                    }
                }

                record.FiveKm.SetTime(fastestParkrun5km);
                //record.TenKm = await AthletesManager.GetTimeAsync(RaceDistance.TenKm, fastestParkrun5km);
                //record.TenMiles = await AthletesManager.GetTimeAsync(RaceDistance.TenMiles, fastestParkrun5km);
                //record.HalfMarathon = await AthletesManager.GetTimeAsync(RaceDistance.HalfMarathon, fastestParkrun5km);

                this.Records.Add(record);
            }
        }

        public void ResetAllTimes()
        {
            foreach (var record in Records)
            {
                record.FiveKm.SetTime(TimeSpan.Zero);
                record.TenKm.SetTime(TimeSpan.Zero);
                record.TenMiles.SetTime(TimeSpan.Zero);
                record.HalfMarathon.SetTime(TimeSpan.Zero);
            }
        }
    }
}
