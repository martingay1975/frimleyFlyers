/*global define*/
define(["text!./runners-main.html", "competitorsData"], function (componentTemplate, competitorsData) {

	"use strict";

	var RunnersMainComponentViewModel;
	RunnersMainComponentViewModel = function (params) {

		var init, self = this;

		if (!(this instanceof RunnersMainComponentViewModel)) {
			throw "Must invoke the function RunnersMainComponentViewModel with the new operator";
		}

		this.competitorsData = competitorsData;
	};
	
	return { viewModel: RunnersMainComponentViewModel, template: componentTemplate };

});
