/*global define*/
define(["text!./runner.html"], function (componentTemplate) {

	"use strict";

	var RunnerComponentViewModel;

	RunnerComponentViewModel = function (params) {

		if (!(this instanceof RunnerComponentViewModel)) {
			throw "Must invoke the function RunnerComponentViewModel with the new operator";
		}

		this.competitorId = params.competitorId;
	};
	
	RunnerComponentViewModel.prototype = {
		
	};

	return { viewModel: RunnerComponentViewModel, template: componentTemplate };

});
