define(['timeSpan', 'person'], function(TimeSpan, Person) {

		"use strict";

		var CompetitorsData = function() {

			var addCompetitors,
				init,
				numericSortDescending,
				self = this;

			// publics
			this.competitors = [];

			this.calculateCompetitorsTotalPoints = function(races, takeNBestScores) {

				self.competitors.forEach(function (person) {
					person.points = 0;
					var competitorPointsList = [];
					races.forEach(function(race) {
						var raceEventResultEntry = race.findInPositions(person.name);
						if (raceEventResultEntry) {
							competitorPointsList.push(raceEventResultEntry.points);
						} else {
							competitorPointsList.push(0);
						}
					});

					// we have a list of all the points for a competitor. Now sort and pick the best 'takeNBestScores'
					competitorPointsList.sort(numericSortDescending);

					for (var competitorPointsListIndex = 0; competitorPointsListIndex < takeNBestScores; competitorPointsListIndex++) {
						if (competitorPointsList[competitorPointsListIndex]) {
					        person.points += competitorPointsList[competitorPointsListIndex];
					    }
					}
				});
			};

			this.sortByName = function() {
				self.competitors.sort(function(leftPerson, rightPerson) {
					if (leftPerson.name > rightPerson.name) {
						return 1;
					} else {
						return -1;
					}
				});
			};

			this.sortByPoints = function() {
				self.competitors.sort(function(leftPerson, rightPerson) {
					if (leftPerson.points === rightPerson.points) {
						return 0;
					} else if (leftPerson.points > rightPerson.points) {
						return -1;
					} 

					return 1;
				});
			};

			this.first = function (callback) {
				var personIndex, person;
				for (personIndex = 0; personIndex < this.competitors.length; personIndex += 1) {
					person = this.competitors[personIndex];
					var result = callback(person);
					if (result) {
						return person;
					}
				}

				return null;
			};

			this.getCompetitors = function (seasonYear) {
				var competitorsForSeason = [];
				self.competitors.forEach(function (competitor) {
					if (competitor.isInSeason(seasonYear)) {
						competitorsForSeason.push(competitor);
					}
				});

				return competitorsForSeason;
			};

			this.getCompetitor = function(name) {
				var compareFn = function(person) {
					return person.name === name;
				};

				return this.first(compareFn);
			};

			// privates
			numericSortDescending = function (leftPerson, rightPerson) {
				if (leftPerson === rightPerson) {
					return 0;
				} else if (leftPerson > rightPerson) {
					return -1;
				}

				return 1;
			};

			addCompetitors = function() {
				var martingay = new Person(1, "Martin Gay", "995184", "414364");
				var jamesball = new Person(2, "James Ball", "7396134", "248342");
				var chrispeddle = new Person(3, "Chris Peddle", "7597223", "399793");
				var davidpeddle = new Person(4, "David Peddle", "3927231", "79200");
				var duncanball = new Person(5, "Duncan Ball", "12896333", "56739");
				var bobturner = new Person(6, "Bob Turner", "381428", "289659");
				var alasdairnuttall = new Person(7, "Alasdair Nuttall", "1106175", "220582");
				var sarahErskine = new Person(8, "Sarah Erskine", "4751449", "569008");
				var alexHalfacre = new Person(9, "Alex Halfacre", "7880452", "912484");
				var richardBoese = new Person(10, "Richard Boese", "19591034", "37186");
				var sarahCampbellFoster = new Person(11, "Sarah Campbell-Foster", "", "75812");
				var philJelly = new Person(12, "Phil Jelly", "1661722", "3893");
				var louiseParker = new Person(13, "Louise Parker", "", "426881");
				var nicholasYewings = new Person(14, "Nicholas Yewings", "", "107892");
				var jimLaidlaw = new Person(15, "Jim Laidlaw", "379140", "1013712");
				var leeMarshall = new Person(16, "Lee Marshall", "", "76667");
				var helenHart = new Person(17, "Helen Hart", "", "94048");
				var darrenStone = new Person(18, "Darren Stone", "5460853", "523541");
				var alfredBoese = new Person(19, "Alfred Boese", "", "83588");
				var emmaMalcolm = new Person(20, "Emma Malcolm", "1717915", "344643");
				var elinorBoese = new Person(21, "Elinor Boese", "", "125634");
				var adrianKeaneMunday = new Person(22, "Adrian Keane-Munday", "", "2272640");
				var oliPeddle = new Person(23, "Oli Peddle", "13197801", "953039");
				var daveBartlett = new Person(24, "Dave Bartlett", "", "2087676");
				var tomChurchill = new Person(25, "Tom Churchill", "1125412", "91676");
				var richardHowden = new Person(26, "Rich Howden", "1750767", "414051");
				var benGay = new Person(27, "Ben Gay", "", "430946");
				var juliaBoese = new Person(28, "Julia Boese", "", "293582");
				var alanBush = new Person(29, "Alan Bush", "19641672", "1520236");
				var andyPoulter = new Person(30, "Andy Poulter", "", "2306222");
				var zoeStone = new Person(31, "Zoe Stone", "", "449398");
				var brunoSilva = new Person(32, "Bruno Silva", "", "3201689");
				var derekPeddle = new Person(33, "Derek Peddle", "", "3174383");
				var susanRodrigues = new Person(34, "Susan Rodrigues", "", "3414380");
				var ashtonPeddle = new Person(35, "Ashton Peddle", "", "676392");
				var karenPeddle = new Person(36, "Karen Peddle", "", "116038");
				var kirstieStone = new Person(37, "Kirstie Stone", "", "3506857");
				var emHowden = new Person(38, "Em Howden", "", "3506857");
				var charmaineLong = new Person(39, "Charmaine Long", "", "2914203");
				var jodieRaynsford = new Person(40, "Jodie Raynsford", "", "491932");
				var paulWilliams = new Person(41, "Paul Williams", "", "41533");
				var joLongmuir = new Person(42, "Jo Longmuir", "", "74484");
				var alexLongmuir = new Person(43, "Alex Longmuir", "", "75028");
				var petaRevell = new Person(44, "Peta Revell", "", "186523");
				var louiseMcIntosh = new Person(45, "Louise McIntosh", "", "47519");
				var fionaKeaneMunday = new Person(46, "Fiona Keane-Munday", "", "4607674");
				var lewisWhatley = new Person(47, "Lewis Whatley", "", "911642");
				var chelseaKnight = new Person(48, "Chelsea Knight", "", "145938");
				var christineScally = new Person(49, "Christine Scally", "", "77879");
				var richardFyvie = new Person(50, "Richard Fyvie", "", "75992");
				var samBenson = new Person(51, "Sam Benson", "", "3435693");
				var garethHopkins = new Person(52, "Gareth Hopkins", "", "3583073");
				var hayleyPhillipsHart = new Person(53, "Hayley Phillips-Hart", "", "2262112");
				var maryWilliams = new Person(54, "Mary Williams", "", "780136");
				var hannahWilliams = new Person(55, "Hannah Williams", "", "780142");
				var karenPhillips = new Person(56, "Karen Phillips", "", "");
				var jessRaynsford = new Person(57, "Jess Raynsford", "", "3912467");
				var stevePage = new Person(58, "Steve Page", "", "1255112");
				var emilyBenson = new Person(59, "Emily Benson", "", "4022877");
				var rebeccaWilliams = new Person(60, "Rebecca Williams", "", "59915");
				var simonHarvey  = new Person(61, "Simon Harvey", "", "76882");

				self.competitors.push(martingay,
					jamesball,
					chrispeddle,
					davidpeddle,
					duncanball,
					bobturner,
					alasdairnuttall,
					alexHalfacre,
					sarahCampbellFoster,
					louiseParker,
					jimLaidlaw,
					leeMarshall,
					darrenStone,
					daveBartlett,
					tomChurchill,
					richardHowden,
					emmaMalcolm,
					nicholasYewings,
					philJelly,
					richardBoese,
					alanBush,
					adrianKeaneMunday,
					andyPoulter,
					helenHart,
					oliPeddle,
					juliaBoese,
					alfredBoese,
					elinorBoese,
					benGay,
					zoeStone,
					brunoSilva,
					derekPeddle,
					susanRodrigues,
					ashtonPeddle,
					karenPeddle,
					kirstieStone,
					emHowden,
					charmaineLong,
					jodieRaynsford,
					paulWilliams,
					joLongmuir,
					alexLongmuir,
					petaRevell,
					louiseMcIntosh,
					fionaKeaneMunday,
					lewisWhatley,
					chelseaKnight,
					christineScally,
					richardFyvie,
					samBenson,
					garethHopkins,
					hayleyPhillipsHart,
					maryWilliams,
					hannahWilliams,
					karenPhillips,
					jessRaynsford,
					stevePage,
					emilyBenson,
					rebeccaWilliams,
					simonHarvey);

				self.sortByName();
			};

			init = function() {
				addCompetitors();
			};

			init();
		};

		return new CompetitorsData();
	});