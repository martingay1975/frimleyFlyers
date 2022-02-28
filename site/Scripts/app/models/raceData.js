// season data
define([
	"jquery",
	"timeSpan",
	"race",
	"competitorsData",
	"siteOptions",
], function ($, TimeSpan, Race, competitorsData, siteOptions) {
	"use strict";

	var RaceData = function (seasonYear) {
		if (!seasonYear) {
			throw "must provide a season year";
		}

		var init;
		var addRacesAsync;
		var calculateRaceResults;

		/*******  Public  *********/
		this.races = [];
		this.seasonYear = seasonYear;
		this.initializedPromise = null;
		this.takeNBestScores = null;
		this.pointsScheme = null;
		this.pointsPbBonus = null;
		this.competitors = [];

		this.getRace = function (raceId) {
			var raceIndex;
			var race;

			for (raceIndex = 0; raceIndex < this.races.length; raceIndex += 1) {
				race = this.races[raceIndex];
				if (race.id === raceId) {
					return race;
				}
			}

			throw "Unable to find race with id: " + raceId;
		};

		/*******  Private  *********/

		addRacesAsync = function () {
			var self = this;

			var isOutOfDate = siteOptions.isOutOfDate();
			var url = "/res/json/raceData" + self.seasonYear + ".json";

			// This will fail silently if the JSON is not valid
			var getRaceDataPromise = $.ajax({
				url: url,
				dataType: "json",
				cache: false,
			});

			getRaceDataPromise.fail(function (err, err2, err3, err4) {
				debugger;
				window.alert(
					"getRaceDataPromise failed. " +
						err3 +
						" " +
						url +
						". Try using err.responseText.substring(startPos, endPos)"
				);
			});

			var completedPromise = getRaceDataPromise.then(function (
				racesDataJson
			) {
				self.takeNBestScores = racesDataJson.takeNBestScores;
				self.pointsScheme = racesDataJson.pointsScheme;
				self.pointsPbBonus = racesDataJson.pointsPbBonus;
				self.entryCost = racesDataJson.entryCost;

				self.leagueTableColumnsToShow =
					racesDataJson.leagueTableColumnsToShow || [];
				var personCounter = 1;
				racesDataJson.records.forEach(
					function (recordJson) {
						var person = competitorsData.getCompetitor(
							recordJson.name
						);

						if (!person) {
							throw (
								"Unable to find person '" +
								recordJson.name +
								"' in '" +
								url +
								"'"
							);
						}

						var fiveKm = new TimeSpan(
							recordJson.fiveKm.hour,
							recordJson.fiveKm.minute,
							recordJson.fiveKm.second
						);

						var tenKm = null;
						if (recordJson.tenKm) {
							tenKm = new TimeSpan(
								recordJson.tenKm.hour,
								recordJson.tenKm.minute,
								recordJson.tenKm.second
							);
						}

						var halfMarathon = null;
						if (recordJson.halfMarathon) {
							halfMarathon = new TimeSpan(
								recordJson.halfMarathon.hour,
								recordJson.halfMarathon.minute,
								recordJson.halfMarathon.second
							);
						}

						var twohalfKm = null;
						if (recordJson.twohalfKm) {
							twohalfKm = new TimeSpan(
								recordJson.twohalfKm.hour,
								recordJson.twohalfKm.minute,
								recordJson.twohalfKm.second
							);
						}

						var tenMiles = null;
						if (recordJson.tenMiles) {
							tenMiles = new TimeSpan(
								recordJson.tenMiles.hour,
								recordJson.tenMiles.minute,
								recordJson.tenMiles.second
							);
						}

						person.setRecords(
							self.seasonYear,
							twohalfKm,
							fiveKm,
							tenKm,
							halfMarathon,
							tenMiles
						);

						personCounter++;

						self.competitors.push(person);
					},
					function (failure) {
						console.log(failure);
					}
				);

				// Map the JSON onto the RACE class

				// Add each of the races
				racesDataJson.races.forEach(function (raceDataJson) {
					var race = new Race(
						seasonYear,
						self.pointsScheme,
						self.pointsPbBonus,
						raceDataJson.label,
						raceDataJson.distance,
						raceDataJson.website,
						raceDataJson.icon
					);

					// Add each of the events that constitute the race
					raceDataJson.events.forEach(function (raceEventsJson) {
						var event = race.addRaceEvent(
							new Date(
								raceEventsJson.date[0],
								raceEventsJson.date[1],
								raceEventsJson.date[2]
							),
							raceEventsJson.distance
						);

						// Add each of the competitors results
						if (raceEventsJson.results) {
							raceEventsJson.results.forEach(function (
								raceEventResultJson
							) {
								event.addCompetitorResult(
									raceEventResultJson.name,
									new TimeSpan(
										raceEventResultJson.time.hour,
										raceEventResultJson.time.minute,
										raceEventResultJson.time.second
									),
									raceEventResultJson.notes
								);
							});
						}
					});

					self.races.push(race);
				});
			});

			return completedPromise;
		};

		calculateRaceResults = function () {
			this.races.forEach(function (race) {
				race.generatePositions();
				race.assignPoints();
			});
		};

		init = function () {
			var self = this;
			var addRacesPromise = addRacesAsync.call(this);

			addRacesPromise.fail(function (err) {
				alert("addRacesAsync failed");
			});

			var addRaceRacesProcessedPromise = addRacesPromise.then(
				function () {
					try {
						calculateRaceResults.call(self);
						competitorsData.calculateCompetitorsTotalPoints(
							self.races,
							self.takeNBestScores
						);
						competitorsData.sortByPoints();
					} catch (e) {
						console.log(e);
					}
				}
			);

			return addRaceRacesProcessedPromise;
		};

		this.initializedPromise = init.call(this);
	};

	return RaceData;
});
