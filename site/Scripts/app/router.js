/*global define*/
define(["jquery", "knockout", "crossroads", "hasher", "routerConfig", "googleAnalytics"], function ($, ko, crossroads, hasher, routerConfig, googleAnalytics) {

	// This module configures crossroads.js, a routing library. 
	"use strict";

	var activateCrossroads = function() {

		/* parameters are: newHash, old hash */
		var parseHash = function(newHash) {
			crossroads.parse(newHash);
		};

		crossroads.normalizeFn = crossroads.NORM_AS_OBJECT;
		hasher.initialized.add(parseHash);
		hasher.changed.add(parseHash);
		hasher.init();
	};

	var Router = function (routerConfig) {

		var self = this;
		var hashUpdated;
		var init;

		this.currentRouteObservable = ko.observable({});

		// handler for when the hash changes value. 
		hashUpdated = function (route, requestParams) {
			// merge in the parameter value in the querystring along with the route information.
			$.extend(requestParams, route.params);
			self.currentRouteObservable(requestParams);

			googleAnalytics.setPage(requestParams, 'title');
			googleAnalytics.sendPageViewHit();
		};

		this.setUrl = function(url) {
			hasher.setHash(url);
		};

		init = function () {
			// register all the routes
			ko.utils.arrayForEach(routerConfig.routes, function (route) {
				var addedRoute = crossroads.addRoute(route.url, function (requestParams) {
					hashUpdated(route, requestParams);
				});

				// append specific rules
				if (route.rules) {
					addedRoute.rules = route.rules;
				}
			});

			// add a handler which captures a route that is invalid
			crossroads.bypassed.add(function (request) {
				window.alert("bad route");
			});

			activateCrossroads();
		};

		init();
	};
	
	return new Router(routerConfig);
});
