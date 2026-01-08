using FF.DataEntry.Api;
using FF.DataEntry.Dto;

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
                createAthlete(7, "Alasdair Nuttall", "1106175", "220582"),
				//createAthlete(8, "Sarah Erskine", "4751449", "569008"),
				//createAthlete(9, "Alex Halfacre", "7880452", "912484"),
				//createAthlete(10, "Richard Boese", "19591034", "37186"),
				createAthlete(11, "Sarah Campbell-Foster", "", "75812"),
				//createAthlete(12, "Phil Jelly", "1661722", "3893"),
				//createAthlete(13, "Louise Parker", "", "426881"),
				//createAthlete(14, "Nicholas Yewings", "", "107892"),
				//createAthlete(15, "Jim Laidlaw", "379140", "1013712"),
				//createAthlete(16, "Lee Marshall", "", "76667"),
				//createAthlete(17, "Helen Hart", "", "94048"),
                createAthlete(18, "Darren Stone", "5460853", "523541"),
				//createAthlete(19, "Alfie Boese", "", "83588"),
				//createAthlete(20, "Emma Malcolm", "1717915", "344643"),
				//createAthlete(21, "Elinor Boese", "", "125634"),
				createAthlete(22, "Adrian Keane-Munday", "", "2272640"),
                createAthlete(23, "Oli Peddle", "13197801", "953039"),
                createAthlete(24, "Lewis Whatley", "", "911642"),
				//createAthlete(24, "Dave Bartlett", "", "2087676"),
				createAthlete(25, "Tom Churchill", "1125412", "91676"),
                //createAthlete(26, "Rich Howden", "1750767", "414051"),
                createAthlete(27, "Ben Gay", "", "430946", "Leamington"),
				//createAthlete(28, "Julia Boese", "", "293582"),
				createAthlete(29, "Alan Bush", "19641672", "1520236"),
                createAthlete(30, "Andy Poulter", "", "2306222"),
                //createAthlete(31, "Zoe Stone", "", "449398"),
				//createAthlete(32, "Bruno Silva", "", "3201689"),
				//createAthlete(33, "Derek Peddle", "", "3174383"),
				createAthlete(34, "Susan Rodrigues", "", "3414380"),
                createAthlete(35, "Ashton Peddle", "", "676392"),
                createAthlete(36, "Karen Peddle", "", "116038"),
                createAthlete(37, "Kirstie Stone", "", "3506857"),
                //createAthlete(38, "Em Howden", "", "121641"),
                //createAthlete(39, "Charmaine Long", "", "2914203"),
                //createAthlete(40, "Sam Benson", "", "3435693"),
                createAthlete(41, "Paul Williams", "", "41533"),
                //createAthlete(42, "Richard Fyvie", "", "75992"),
                createAthlete(43, "Steve Page", "", "1255112"),
                createAthlete(44, "Christine Scally", "", "77879"),
                //createAthlete(45, "Chelsea Knight", "", "145938"),
                createAthlete(46, "Louise McIntosh", "", "47519"),
                //createAthlete(47, "Gareth Hopkins", "", "3583073"),
                createAthlete(48, "Jodie Raynsford", "", "491932"),
                //createAthlete(49, "Hannah Williams", "", "780142"),
                createAthlete(50, "Fiona Keane-Munday", "", "4607674"),
                //createAthlete(51, "Emily Benson", "", "4022877"),
                createAthlete(52, "Rebecca Williams", "", "10354407"),  // old profile: 59915
                //createAthlete(53, "Jess Raynsford", "", "3912467"),
                //createAthlete(54, "Mary Williams", "", "780136"),
                createAthlete(55, "Simon Harvey", "", "76882"),
                createAthlete(56, "Susan Harvey", "", "77851"),
                createAthlete(57, "Jo Longmuir", "", "74484"),
                createAthlete(58, "Paul Bass", "", "1250667"),
                createAthlete(59, "Lucy Bass", "", "724150"),
                //createAthlete(60, "Hayley Bush", "", "5005026"),
                //createAthlete(61, "Adam Pett", "", "9059902"),
                createAthlete(62, "Karen Phillips", "", "3549873"),
                createAthlete(63, "Wendy Ockrim", "", "1473870"),
                createAthlete(64, "Harvey Ockrim", "", "1472445"),
                createAthlete(65, "Kev Knight", "", "4968220"),
                createAthlete(66, "Matthew Knight", "", "4988701"),
                createAthlete(67, "Jen Knight", "", "4988676"),
                createAthlete(68, "Charlotte Knight", "", "4988692"),
                createAthlete(69, "Leonie Harvey", "", "77854", "Rother Valley"),
                //createAthlete(70, "Richard Jackson", "", "848919"),
                createAthlete(71, "Phoebe Harvey", "", "77852"),
                createAthlete(72, "Rufus Frew", "", "734089"),
            };

            Athlete createAthlete(int id, string name, string stravaId, string parkrunId, string homeParkrun = ParkrunLocation.FRIMLEYLODGE_EVENTNAME)
            {
                return new Athlete { Name = name, StravaId = stravaId, ParkrunId = parkrunId, HomePakrunName = homeParkrun };
            }
        }

        public Task InitAsync(string path)
        {
            return PopulateWithParkrunListAsync(path);
        }

        public static async Task<Time> GetTimeAsync(RaceDistance raceDistance, TimeSpan backup5kmTime, TimeSpan? timeSpan = null)
        {
            if ((!timeSpan.HasValue || timeSpan == TimeSpan.Zero) && backup5kmTime != TimeSpan.Zero)
            {
                timeSpan = await RaceTimePredictor.GetPredictor(raceDistance, backup5kmTime);
            }

            Time time = new Time();
            time.SetTime(timeSpan == null ? TimeSpan.Zero : timeSpan.Value);
            return time;
        }

        public Athlete FindAthleteByName(string name) =>
            this.Athletes.Single(athlete => athlete.Name == name);


        public async Task PopulateWithParkrunListAsync(string basePath, IReadOnlyList<string>? athleteNamesToInclude = null, bool getFromParkrunSite = false)
        {
            if (string.IsNullOrWhiteSpace(basePath))
            {
                throw new ArgumentNullException(nameof(basePath));
            }

            basePath = Path.Combine(basePath, "Athletes");
            IList<Athlete> athletes;
            if (athleteNamesToInclude == null || athleteNamesToInclude.Count == 0)
            {
                // Get all athletes in the Athlete Manager collection
                athletes = this.Athletes;
            }
            else
            {
                athletes = this.Athletes.Where(athlete => athleteNamesToInclude.Contains(athlete.Name)).ToList();
            }

            // now we have a list of athletes - fill the parkrunlist portion of the athlete. If overwrite == true then this will involve going to the parkrun site to get the latest data.
            List<Task> athleteTasks = athletes
                .Select(athlete => athlete.PopulateParkrunListAsync(basePath, getFromParkrunSite))
                .ToList();

            await Task.WhenAll(athleteTasks);
        }
    }
}
