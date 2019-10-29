/*global define*/
define(["raceData", "text!./races-main.html"], function (raceData, componentTemplate) {

	"use strict";

	var RacesMainComponentViewModel;

	RacesMainComponentViewModel = function (params) {

		var init;

		if (!(this instanceof RacesMainComponentViewModel)) {
			throw "Must invoke the function RacesMainComponentViewModel with the new operator";
		}

		this.races = raceData.races;

		//init = function() {

			
		//};

		//init();

	};
	
	return { viewModel: RacesMainComponentViewModel, template: componentTemplate };

});
