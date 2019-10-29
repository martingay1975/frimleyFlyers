define(['timeSpan', 'competitorsData'], function (TimeSpan, competitorsData) {

	"use strict";

	var RaceEventResultEntry, calculatePercentageFromPb;

	calculatePercentageFromPb = function (seasonYear, distance) {

		var self = this;
		

		// get the persons pb
		var person = competitorsData.getCompetitor(self.name);
		if (!person) {
			throw "Unable to find person '" + self.name + "'";
		}

		var pb = person.getSeason(seasonYear).getTime(distance);
		if (!pb) {
			throw "No pb defined for this athlete for this distance";
		}

		var pbInSeconds = pb.totalSeconds();
		var eventTimeInSeconds = self.time.totalSeconds();

		var pc = ((eventTimeInSeconds / pbInSeconds) - 1) * 100;
		return pc;
	};

	RaceEventResultEntry = function (seasonYear, name, time, distance) {

		var self = this;
		if (!(time instanceof TimeSpan)) {
			throw "time must be of type TimeSpan";
		}

		self.name = name;
		self.time = time;
		self.percentageFromPb = calculatePercentageFromPb.call(self, seasonYear, distance);
		self.isBetterThanPb = self.percentageFromPb < 0;
		self.isBest = false;
		self.points = 0;
		self.position = null;
	};

	// Description: Given an array of RaceEventResultEntry, find the entry with a specific name.
	RaceEventResultEntry.findFromName = function(arrayOfRaceEventResultEntry, nameToFind) {

		var raceEventResultEntry, resultIndex;
		for (resultIndex = 0; resultIndex < arrayOfRaceEventResultEntry.length; resultIndex++) {
			raceEventResultEntry = arrayOfRaceEventResultEntry[resultIndex];
			if (raceEventResultEntry.name === nameToFind) {
				return raceEventResultEntry;
			}
		};

		return null;	// unable to find a result for the person with the name passed as a parameter.

	};

	return RaceEventResultEntry;
});