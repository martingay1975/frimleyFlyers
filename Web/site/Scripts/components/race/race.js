/*global define*/
define(["text!./race.html", "raceData"], function (componentTemplate, raceData) {

	"use strict";

	var RaceComponentViewModel;
	RaceComponentViewModel = function (params) {

		var init, self = this;

		if (!(this instanceof RaceComponentViewModel)) {
			throw "Must invoke the function RaceComponentViewModel with the new operator";
		}

		this.raceId = params.raceId;
		this.race = null;

		init = function() {

			if (!this.raceId) {
				return;
			}

			//this.race = raceData.getRace(this.raceId);
			 
		};

		init.call(this);

	};

	return { viewModel: RaceComponentViewModel, template: componentTemplate };

});
