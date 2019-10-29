define(['timeSpan', 'raceDistance', 'polyfill'], function (TimeSpan, raceDistance) {

	"use strict";

	var Season = function(year) {

		// stored as an array. Position in array determined by the 'raceDistance' enum.
		var records = [new TimeSpan(), new TimeSpan(), new TimeSpan(), new TimeSpan()];
		
		this.year = year;

		this.getTime = function (distance) {
			return records[distance];
		};

		this.setRecords = function (twohalfKmTime, fiveKmTime, tenKmTime, halfMarathonTime) {
			records[raceDistance.twohalfKm] = twohalfKmTime;
			records[raceDistance.fiveKm] = fiveKmTime;
			records[raceDistance.tenKm] = tenKmTime;
			records[raceDistance.halfMarathon] = halfMarathonTime;
		};
	};

	var Person = function (id, name, stravaAthleteId, parkrunAthleteId) {
		var self = this;
		this.id = id;
		this.seasons = [];
		this.name = name;
		this.points = 0;
		this.stravaAthleteId = stravaAthleteId;
		this.parkrunAthleteId = parkrunAthleteId;
		
		this.isInSeason = function (seasonYear) {
			try {
				self.getSeason(seasonYear);
				return true;
			} catch (e) {
				return false;
			}
		};

		this.getSeason = function(seasonYear) {
			var season = self.seasons.find(function(season) {
				return season.year === seasonYear;
			});

			if (!season) {
				throw "Unable to find season: " + seasonYear + " for person: " + self.name;
			}
			return season;
		};

		this.setRecords = function (year, twohalfKmTime, fiveKmTime, tenKmTime, halfMarathonTime) {
			var newSeason = new Season(year);
			self.seasons.push(newSeason);
			newSeason.setRecords(twohalfKmTime, fiveKmTime, tenKmTime, halfMarathonTime);
		};
	};

	return Person;
});