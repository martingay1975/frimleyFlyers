define(["siteViewModel"], function (siteViewModel) {

	"use strict";

	var runsViewModelComponent = function () {
		siteViewModel.pageTitleObservable("Runs");
	};

	return runsViewModelComponent;
});

