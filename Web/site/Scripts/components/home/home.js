define(["siteViewModel"], function (siteViewModel) {

	"use strict";

	var homeObject = function () {
		siteViewModel.pageTitleObservable("Home");
	};

	return homeObject;
});

