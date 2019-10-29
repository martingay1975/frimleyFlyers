/*global define*/
define(['knockout', 'router'], function (ko, router) {

	"use strict";

	var siteViewModel = {
		currentRouteObservable: router.currentRouteObservable,
		pageTitleObservable: ko.observable("Loading ...")
	};

	return siteViewModel;
});