/*global require*/
define(["knockout", "siteOptions", "text!./header.html"], function (ko, siteOptions, componentTemplate) {

	"use strict";

	var init = function () {
		var headerComponentViewModel = this;
		headerComponentViewModel.dateLastUpdated(siteOptions.dateLastUpdated);
	};

	var HeaderComponentViewModel = function () {
		this.dateLastUpdated = ko.observable();
		init.call(this);
	};

    return { viewModel: HeaderComponentViewModel, template: componentTemplate };
});
