define(["jquery", "raceData", "raceColumn", "navigationColumn", "competitorsData", "row"], function ($, RaceData, RaceColumn, NavigationColumn, competitorsData, Row) {

	"use strict";
	var RaceDatabase = function () {

		// private
		var self = this;

		var changeRacesToShow = function (racesToShow, headingRows, tableRows) {

			var isShow;
			for (var raceId = 1; raceId < 8; raceId += 1) {
				isShow = racesToShow.indexOf(raceId) > -1;
				RaceColumn.showDetailColumns(isShow, raceId, headingRows, tableRows);
			}
		};

		// public
		this.bySeasonYear = [];

		this.getSeasonAsync = function (seasonYear, preColumns, columnPaletteCycle, headingRows, tableRows, options) {

			var cacheOrFetchPromise = self.cacheOrFetchAsync({
										cacheValue: self.bySeasonYear[seasonYear],
										fetchAsync: function() {
											var fetchPromise = self.fetchSeasonYearAsync(seasonYear, preColumns, columnPaletteCycle, headingRows, tableRows, options);
											fetchPromise.fail(function(err) {
												alert("Failed to get season: " + seasonYear);
											});
											return fetchPromise;
										}
			});

			var thenPromise = cacheOrFetchPromise.then(function (value) {
				self.bySeasonYear[seasonYear] = value;
				return value;
			}, function(err) {
				alert("failed to call cacheOrFetchAsync()");
			});

			return thenPromise;
		};

		this.fetchSeasonYearAsync = function(seasonYear, preColumns, columnPaletteCycle, headingRows, tableRows, options) {

			var raceData = new RaceData(seasonYear);
			var navigationColumn;
			var raceColumns = [];

			var initalizedThenPromise = raceData.initializedPromise.then(function() {

				// add the default columns (name, total)
				preColumns.forEach(function(preColumn) {
					preColumn.addHeading(headingRows);
				});

				// add the individual race columns
				raceData.races.forEach(function(race) {
					var raceColumn = new RaceColumn(race, columnPaletteCycle.getNextColor());
					raceColumns.push(raceColumn);
					raceColumn.addRaceHeading(headingRows);
				});

				// add a navigation column
				if (options.showNavigation) {
					navigationColumn = new NavigationColumn(columnPaletteCycle.getNextColor());
					navigationColumn.addHeading(headingRows);
				}

				var rows;

				// add all the data in the rows
				var competitors = competitorsData.getCompetitors(seasonYear);
				competitors.forEach(function(person, index) {

					// Add the data in the table
					rows = [new Row()];

					// put in the default columns at the start
					preColumns.forEach(function(column) {
						column.addData(person, rows);
					});

					// add the race column
					raceColumns.forEach(function(raceColumn) {
						raceColumn.addRaceData(seasonYear, person, rows);
					});

					// only do the first instance of this.
					if (options.showNavigation && index === 0) {
						navigationColumn.addData(person, rows, competitors.length);
					}

					// add the rows to the table (used as the viewModel)
					//tableRows.concat(rows);
					$.merge(tableRows, rows);
				});

				// react to changes in which columns to show details for.
				options.racesToDetail.races.subscribe(function(newArray) {
					changeRacesToShow(newArray, headingRows, tableRows);
				});

				changeRacesToShow(raceData.leagueTableColumnsToShow, headingRows, tableRows);

				// this only returns the race data, but not everything else like headingRows and tableRows. Need to think
				// about the layers of abstraction. Not quite right currently.
				return raceData;
			});

			return initalizedThenPromise.promise();
		};

		this.cacheOrFetchAsync = function(cacheOrFetchParam) {

			// { cacheValue - cache value, 
			//   fetchAsync - callback function }

			if (cacheOrFetchParam.cacheValue) {
				// we already have a cache value, therefore return it
				var promise = $.Deferred(function(newDeferred) {
					newDeferred.resolve(cacheOrFetchParam.cacheValue);
				});

				return promise;
			} else {
				var fetchPromise = cacheOrFetchParam.fetchAsync().promise();
				fetchPromise.fail(function(err) {
					window.alert("failed to get success promise");
				});
				return fetchPromise;
			}
		};
	}

	return new RaceDatabase();
});