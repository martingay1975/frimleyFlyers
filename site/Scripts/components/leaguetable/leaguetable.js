/*global define*/
define(["jquery", "jquery-color", "knockout", "router", "siteViewModel", "stringUtil", "nameColumn", "pbColumn", "pointsColumn", "raceColumn",
		"navigationColumn", "row", "raceData", "race", "paletteCycle", "raceDatabase"],

	function ($, jqueryColor, ko, router, siteViewModel, stringUtil, NameColumn, pbColumn, PointsColumn, RaceColumn,
		NavigationColumn, Row, RaceData, Race, PaletteCycle, raceDatabase) {

		"use strict";

		var RacesToDetail = function () {

			var self = this;

			var showIndex = function(raceId) {
				return self.races().indexOf(raceId);
			};

			var isShow = function(raceId) {
				return showIndex(raceId) > -1;
			};

			var add = function (raceId) {
				if (!isShow(raceId)) {
					self.races.push(raceId);
				}
			};

			var remove = function (raceId) {
				self.races.remove(raceId);
			};

			// public functions.
			this.races = ko.observableArray([]);
			
			this.toggle = function (raceId) {
				if (isShow(raceId)) {
					remove(raceId);
				} else {
					add(raceId);
				}
			};
		};

		var options = {
			showNavigation: false,
			racesToDetail: new RacesToDetail()
		};

		var LeagueTableComponentViewModel = function (params) {

			this.pointsSchemeObservable = ko.observable([]);
			this.pointsPbBonusObservable = ko.observable(0);
			this.takeNBestScoresObservable = ko.observable(0);
			this.entryCostObservable = ko.observable(0);

			var columnPaletteCycle = new PaletteCycle([
				$.Color("rgba(176,196,222,0.5)"),
				$.Color("rgba(245,245,245,0.5)")
			]);

			var preColumns = [
				new NameColumn(columnPaletteCycle.getNextColor()),
				new PointsColumn(columnPaletteCycle.getNextColor())
			];

			var self = this;

			if (!(this instanceof LeagueTableComponentViewModel)) {
				throw "Must invoke the function LeagueTableComponentViewModel with the new operator";
			}

			try {
				this.seasonYear = params.seasonYear;
				//var results = "Results " + params.seasonYear;
				//siteViewModel.pageTitleObservable(results);
			} catch (e) {
				alert("errr");
			} 

			this.toggleRaceDetails = function (cell) {
				options.racesToDetail.toggle(cell.raceId);
			};

			this.gotoRaceClick = function (cell, evt) {
				router.setUrl("race/" + cell.raceId);
			};

			this.gotoPersonClick = function (cell, evt) {
				router.setUrl("runner/" + cell.personId);
			};

			this.showRacesRange = function (lowLimit, upLimit) {

				var rowIteration = function (rowArray) {
					var visible;
					rowArray.forEach(function (row) {
						row.cells.forEach(function (cell) {
							if (cell.raceId) {
								visible = cell.raceId >= lowLimit && cell.raceId <= upLimit;
								cell.visible(visible);
							}
						});
					});
				};

				rowIteration(self.tableRows);
				rowIteration(self.headingRows);
			};

			this.headingRows = [];
			this.headingRows.push(new Row()); // array of Cell
			this.headingRows.push(new Row()); // array of Cell
			this.headingRows.push(new Row()); // array of Cell
			this.tableRows = []; // array of Row.
			this.initialized = ko.observable(false);
			
			var init = function () {

				self.initialized(false);
				var getSeasonPromise = raceDatabase.getSeasonAsync(self.seasonYear, preColumns, columnPaletteCycle, self.headingRows, self.tableRows, options);
				getSeasonPromise.done(function (raceData) {
					self.pointsSchemeObservable(raceData.pointsScheme);
					self.pointsPbBonusObservable(raceData.pointsPbBonus);
					self.takeNBestScoresObservable(raceData.takeNBestScores);
					self.entryCostObservable(raceData.entryCost);
					self.initialized(true);
				});
				getSeasonPromise.fail(function(err) {
					window.alert("Failed to call raceDatabase.getSeasonAsync");
					console.error(JSON.stringify(err));
				});

			};
			
			init.call(this);
		};

		return LeagueTableComponentViewModel;
	});
