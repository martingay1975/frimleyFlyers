define([
	"raceEventResultEntry",
	"competitorsData",
	"jquery-dateFormat",
	"raceDistance",
], function (RaceEventResultEntry, competitorsData, dateFormat, raceDistance) {
	"use strict";

	//var self.pointsScheme = [21, 17, 14, 12, 10, 8, 6, 4, 2, 1],
	//	self.pointsPbBonus = 5;

	var RaceEvent = function (seasonYear, date, distance) {
		var self = this;
		this.date = date;
		this.dateString = dateFormat.format.date(date, "dd MMM");
		this.distance = distance;
		this.results = []; // array of raceEventResultEntry
		this.seasonYear = seasonYear;

		this.getResultEntry = function (name) {
			return RaceEventResultEntry.findFromName(self.results, name);
		};

		this.addCompetitorResult = function (name, time, notes) {
			self.results.push(
				new RaceEventResultEntry(
					self.seasonYear,
					name,
					time,
					notes,
					self.distance
				)
			);
		};
	};

	var Race = function (
		seasonYear,
		pointsScheme,
		pointsPbBonus,
		label,
		distance,
		website,
		image
	) {
		var self = this,
			getCompetitorBestEvent,
			placeInResults;

		/******* Public **********/
		this.label = label;
		this.website = website;
		this.image = "/images/" + image;
		this.distance = distance; // raceDistance enum
		this.distanceString = raceDistance.getName(distance);
		this.raceEvents = []; // array of raceEvent
		this.positions = []; // array of ordered raceEventResultEntry.
		this.id = Race.getNextId(); // incrementing unique integer starting at 1.
		this.seasonYear = seasonYear;
		this.pointsScheme = pointsScheme;
		this.pointsPbBonus = pointsPbBonus;

		this.addRaceEvent = function (date) {
			var raceEvent = new RaceEvent(seasonYear, date, self.distance);
			self.raceEvents.push(raceEvent);
			return raceEvent;
		};

		this.findInPositions = function (name) {
			var raceEventResultEntry = RaceEventResultEntry.findFromName(
				self.positions,
				name
			);
			return raceEventResultEntry;
		};

		this.generatePositions = function () {
			var competitorBestEvent,
				orderedBestEvents = [];

			// loop around each person
			competitorsData
				.getCompetitors(seasonYear)
				.forEach(function (person) {
					competitorBestEvent = getCompetitorBestEvent(person.name);

					if (competitorBestEvent != null) {
						// competitor actually competed!

						// now place in correct place in the array.
						placeInResults(orderedBestEvents, competitorBestEvent);
					}

					// if the person did not compete then they do not figure in the results.
				});

			self.positions = orderedBestEvents;
		};

		this.assignPoints = function () {
			var index;

			for (index = 0; index < self.positions.length; index += 1) {
				if (index < self.pointsScheme.length) {
					self.positions[index].points = self.pointsScheme[index];
					self.positions[index].position = index + 1;

					var checkPreviousIndex = index - 1;
					if (
						checkPreviousIndex > -1 &&
						self.positions[index].percentageFromPb ===
							self.positions[checkPreviousIndex].percentageFromPb
					) {
						// there are equal %s from pb - so share the points.
						var averagePoints =
							(self.pointsScheme[index] +
								self.pointsScheme[checkPreviousIndex]) /
							2;
						self.positions[index].points = averagePoints;
						self.positions[checkPreviousIndex].points =
							averagePoints;

						if (self.positions[checkPreviousIndex].isBetterThanPb) {
							self.positions[checkPreviousIndex].points +=
								self.pointsPbBonus;
						}
					}
				}

				// add PB points (even if you are outside the normal scoring points)
				if (self.positions[index].isBetterThanPb) {
					self.positions[index].points += self.pointsPbBonus;
				}
			}
		};

		/******* Private **********/
		placeInResults = function (array, raceEventResultEntry) {
			var index,
				currentRaceEventResultEntry,
				indexAtInsert = -1;

			if (!raceEventResultEntry || !array) {
				throw "Invalid null parameter";
			}

			// loop around all the existing positions. Put the lowest % at the top, all the way to the furthest from PB at the bottom.
			for (index = 0; index < array.length; index += 1) {
				currentRaceEventResultEntry = array[index];
				if (
					raceEventResultEntry.percentageFromPb <
					currentRaceEventResultEntry.percentageFromPb
				) {
					indexAtInsert = index;
					break;
				}
			}

			if (indexAtInsert >= 0) {
				array.splice(indexAtInsert, 0, raceEventResultEntry);
			} else {
				// doesn't beat any existing entries, so add to the end.
				array.push(raceEventResultEntry);
			}
		};

		getCompetitorBestEvent = function (name) {
			var raceEventResultEntry,
				bestEvent = null;

			self.raceEvents.forEach(function (raceEvent) {
				// get the persons result from this event
				raceEventResultEntry = raceEvent.getResultEntry(name);
				if (raceEventResultEntry) {
					if (bestEvent) {
						// if we already have a best event to compare against, then compare
						if (
							raceEventResultEntry.percentageFromPb <
							bestEvent.percentageFromPb
						) {
							bestEvent = raceEventResultEntry;
						}
					} else {
						// if we don't have a best event yet (because this is the first on the list) then make this the best event.
						bestEvent = raceEventResultEntry;
					}
				}
			});

			// finally mark this event entry for the person as the best in the series.
			if (bestEvent) {
				bestEvent.isBest = true;
			}

			return bestEvent;
		};
	};

	Race.counter = 0;
	Race.getNextId = function () {
		Race.counter += 1;
		return Race.counter;
	};

	return Race;
});
