using FF.DataEntry.Dto;
using System.Text.Json;

namespace FF.DataEntry.Utils
{
    public class AthletesManager
    {
		public List<Athlete> Athletes { get; private set; }

		public AthletesManager()
		{
			this.Athletes = new List<Athlete>
			{
				createAthlete(1, "Martin Gay", "995184", "414364"),
				createAthlete(2, "James Ball", "7396134", "248342"),
				createAthlete(3, "Chris Peddle", "7597223", "399793"),
				createAthlete(4, "David Peddle", "3927231", "79200"),
				createAthlete(5, "Duncan Ball", "12896333", "56739"),
				createAthlete(6, "Bob Turner", "381428", "289659"),
				//createAthlete(7, "Alasdair Nuttall", "1106175", "220582"),
				//createAthlete(8, "Sarah Erskine", "4751449", "569008"),
				//createAthlete(9, "Alex Halfacre", "7880452", "912484"),
				//createAthlete(10, "Richard Boese", "19591034", "37186"),
				createAthlete(11, "Sarah Campbell-Foster", "", "75812"),
				//createAthlete(12, "Phil Jelly", "1661722", "3893"),
				createAthlete(13, "Louise Parker", "", "426881"),
				//createAthlete(14, "Nicholas Yewings", "", "107892"),
				createAthlete(15, "Jim Laidlaw", "379140", "1013712"),
				//createAthlete(16, "Lee Marshall", "", "76667"),
				//createAthlete(17, "Helen Hart", "", "94048"),
				createAthlete(18, "Darren Stone", "5460853", "523541"),
				//createAthlete(19, "Alfie Boese", "", "83588"),
				//createAthlete(20, "Emma Malcolm", "1717915", "344643"),
				//createAthlete(21, "Elinor Boese", "", "125634"),
				createAthlete(22, "Adrian Keane-Munday", "", "2272640"),
				createAthlete(23, "Oli Peddle", "13197801", "953039"),
				//createAthlete(24, "Dave Bartlett", "", "2087676"),
				createAthlete(25, "Tom Churchill", "1125412", "91676"),
				createAthlete(26, "Rich Howden", "1750767", "414051"),
				createAthlete(27, "Ben Gay", "", "430946"),
				//createAthlete(28, "Julia Boese", "", "293582"),
				createAthlete(29, "Alan Bush", "19641672", "1520236"),
				createAthlete(30, "Andy Poulter", "", "2306222"),
				//createAthlete(31, "Zoe Stone", "", "449398"),
				//createAthlete(32, "Bruno Silva", "", "3201689"),
				//createAthlete(33, "Derek Peddle", "", "3174383"),
				createAthlete(34, "Susan Rodrigues", "", "3414380"),
				//createAthlete(35, "Ashton Peddle", "", "676392"),
				createAthlete(36, "Karen Peddle", "", "116038"),
				createAthlete(37, "Kirstie Stone", "", "3506857"),
				createAthlete(38, "Em Howden", "", "3506857"),
				//createAthlete(39, "Charmaine Long", "", "2914203")
				createAthlete(40, "Sam Benson", "", "3435693"),
				createAthlete(41, "Paul Williams", "", "41533"),
				createAthlete(42, "Richard Fyvie", "", "75992"),
				createAthlete(43, "Steve Page", "", "1255112"),
				createAthlete(44, "Christine Scally", "", "77879"),
				createAthlete(45, "Chelsea Knight", "", "145938"),
				createAthlete(46, "Louise McIntosh", "", "47519"),
				createAthlete(47, "Gareth Hopkins", "", "3583073"),
				createAthlete(48, "Jodie Raynesford", "", "491932"),
				createAthlete(49, "Hannah Williams", "", "780142"),
				createAthlete(50, "Fiona Keane-Munday", "", "4607674"),
				createAthlete(51, "Emily Benson", "", "4022877"),

			};

			Athlete createAthlete(int id, string name, string stravaId, string parkrunId)
			{
				return new Athlete { Name = name, StravaId = stravaId, ParkrunId = parkrunId };
			}
		}

		public static async Task<Time> GetTimeAsync(RaceDistance raceDistance, TimeSpan backup5kmTime, TimeSpan? timeSpan = null)
		{
			if ((!timeSpan.HasValue || timeSpan == TimeSpan.Zero) && backup5kmTime != TimeSpan.Zero)
			{
				timeSpan = await RaceTimePredictor.GetPredictor(raceDistance, backup5kmTime);
			}

			var time = new Time();
			time.SetTime(timeSpan == null ? TimeSpan.Zero : timeSpan.Value);
			return time;
		}

		public TimeSpan GetQuickestParkrunLastYear(string name, int year)
		{
			var athlete = FindAthleteByName(name);
			return GetQuickestParkrunLastYear(athlete, year);
		}

		public TimeSpan GetQuickestParkrunLastYear(Athlete athlete, int year)
        {
			try
			{
				var startDate = new DateTime(year - 1, 1, 1);
				var endDate = new DateTime(year - 1, 12, 31);
				var inSeasonForAthlete = GetParkrunInDate(athlete.ParkrunRunList, startDate, endDate);
				var selected = inSeasonForAthlete.Select(parkrunRun => parkrunRun.RaceTime).Min();
				return selected;
			}
			catch (Exception ex)
            {
				Console.WriteLine($"{athlete.Name} does not have a parkrun time");
				return TimeSpan.Zero;
            }
		}

		public TimeSpan GetQuickestParkrun(string name, DateTime startDate, DateTime endDate)
        {
			var athlete = FindAthleteByName(name);
			var inSeasonForAthlete = GetParkrunInDate(athlete.ParkrunRunList, startDate, endDate);
			var selected = inSeasonForAthlete.Select(parkrunRun => parkrunRun.RaceTime).Min();
			return selected;
		}

		public IEnumerable<ParkrunRun> GetParkrunInDate(IEnumerable<ParkrunRun> parkrunRunList, DateTime startTime, DateTime endTime)
        {
			return parkrunRunList.Where(parkrunRun => parkrunRun.Date >= startTime && parkrunRun.Date <= endTime);
		}

		public ParkrunRun GetFrimleyLodgeQuickest(string name, DateTime startDate, DateTime endDate)
        {
			if (startDate == null || endDate == null)
            {
				throw new ArgumentNullException(nameof(startDate));
            }

			var athlete = FindAthleteByName(name);
			var inSeasonForAthlete = GetParkrunInDate(athlete.ParkrunRunList, startDate, endDate);
			return inSeasonForAthlete
				.Where(parkrunRun => parkrunRun.Event == "Frimley Lodge")
				.OrderBy(parkrunRun => parkrunRun.RaceTime)
				.First();
		}

		public ParkrunRun GetTouristQuickest(string name, DateTime startDate, DateTime endDate)
        {
			if (startDate == null || endDate == null)
			{
				throw new ArgumentNullException(nameof(startDate));
			}

			var athlete = FindAthleteByName(name);
			var inSeasonForAthlete = GetParkrunInDate(athlete.ParkrunRunList, startDate, endDate);

			return inSeasonForAthlete
				.Where(parkrunRun => parkrunRun.Event != "Frimley Lodge")
				.OrderBy(parkrunRun => parkrunRun.RaceTime)
				.First();
		}

		public Athlete FindAthleteByName(string name)
        {
			return this.Athletes.SingleOrDefault(athlete => athlete.Name == name);
        }

		public async Task PopulateWithParkrunList(string athletesPath, bool overwrite = false)
        {
			if (string.IsNullOrWhiteSpace(athletesPath))
            {
				throw new ArgumentNullException(nameof(athletesPath));
            }

			athletesPath = Path.Combine(athletesPath, "Athletes");

			foreach (var athlete in this.Athletes)
            {
                var athletePath = Path.Combine(athletesPath, athlete.Name + ".json");
				if (File.Exists(athletePath) && !overwrite)
				{
					using (var stream = File.OpenRead(athletePath))
					{
						var loadedAthlete = await JsonSerializer.DeserializeAsync<Athlete>(stream, JsonSerializerDefaultOptions.Options);
						athlete.ParkrunRunList = loadedAthlete?.ParkrunRunList ?? throw new InvalidOperationException();
					}
				}
				else
				{
					// Do some scraping
					if (!string.IsNullOrEmpty(athlete.ParkrunId))
					{
						var parkrunWebsite = new ParkrunWebsite();
						athlete.ParkrunRunList = await parkrunWebsite.GetAllAsync(athlete.ParkrunId);

						// save results locally so don't need to scrape again.... soon anyway.
						using (var stream = File.OpenWrite(athletePath))
						{
							await JsonSerializer.SerializeAsync(stream, athlete, JsonSerializerDefaultOptions.Options);
						}
					}
				}
            }
        }
	}
}
