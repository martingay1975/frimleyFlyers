/*global require*/
define(["knockout", "router"], function(ko, router) {

	"use strict";

	var NavigationComponentViewModel = function () {

		this.activeComponent = ko.pureComputed(function () {
			var componentName = router.currentRouteObservable().componentName;
			return componentName;
		}, this);
	};

	return NavigationComponentViewModel;
});
