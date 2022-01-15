/*global define*/
define(['knockout', 'siteViewModel', 'raceData', 'text!./pbList.html', 'bindingHandlerToggle'],
	function (ko, siteViewModel, RaceData, componentTemplate) {

	"use strict";

	var PbListComponentViewModel;

	PbListComponentViewModel = function (params) {

		if (!(this instanceof PbListComponentViewModel)) {
			throw "Must invoke the function PbListComponentViewModel with the new operator";
		}

		var self = this;

		self.seasonYear = parseInt(params.seasonYear || "2022", 10);
		siteViewModel.pageTitleObservable("Compertitors " + self.seasonYear);
		self.raceData = new RaceData(this.seasonYear, true);
		self.competitors = ko.observableArray();
		self.raceData.initializedPromise.then(function() {
			var allCompetitors = self.raceData.competitors;
			allCompetitors.sort(function(compA, compB) {
				return compA.name < compB.name ? -1 : 1;
			});

			allCompetitors.forEach(function (competitor) {
				var seasonResults = {};
				seasonResults.name = competitor.name;
				var pbListForSesaon = competitor.seasons[0];
				seasonResults.time5km = pbListForSesaon.getTime(0);
				seasonResults.time10km = pbListForSesaon.getTime(1);
				seasonResults.time10miles = pbListForSesaon.getTime(4);
				seasonResults.timeHalf = pbListForSesaon.getTime(2);
				seasonResults.parkrunLink = 'https://www.parkrun.org.uk/parkrunner/' + competitor.parkrunAthleteId + '/all/'

				self.competitors.push(seasonResults);
			});
	
			this.isCollapsed = ko.observable(false);
	
		});
	};

	return { viewModel: PbListComponentViewModel, template: componentTemplate };

});
