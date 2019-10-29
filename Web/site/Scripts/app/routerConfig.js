/*global require*/
define(function() {

	"use strict";

	// set the parameters from the values passed in the url to object parameters used in the components.	
	var setParams = function (request, vals) {
		return {
			filter: vals.filter,
			title: decodeURIComponent(vals.title || ""),
			componentName: vals.subOptionName || "home",
			expanded: vals.expanded !== undefined ? (vals.expanded === "true") : true	// if no 'expanded' value defined then assume expanded = true. Convert the string to a boolean
		};
	};

	var setRunnerParams = function(request, vals) {
		return {
			competitorId: parseInt(vals.competitorId, 10) || null,
			title: decodeURIComponent(vals.title || "")
		};
	};

	var setRaceParams = function(request, vals) {
		return {
			raceId: parseInt(vals.raceId, 10) || null
		};
	};

	// A hard coded sets of routes have been defined which is in the return statement
	// { } = non-optional variable
	// : : = optional variable
	var routerConfig = {

		routes: [
			// default empty hash
			{
				id: "nothing",
				url: "",
				params: {
				},
				rules: {
					normalize_: setParams
				}
			},
			{
				id: "default",
				url: "/:subOptionName:",
				params: {
				},
				rules: {
					subOptionName: ["home", "endure24", "ffchampionship", "ffchampionship2017", "ffchampionship2018", "fftrophy2018", "club", "runs", "pbList"],
					normalize_: setParams
				}
			},
			{
				id: "runner",
				url: "/runner/:competitorId:",
				params: {
					componentName: "runner"
				},
				rules: {
					normalize_: setRunnerParams
				}
			},
			{
				id: "race",
				url: "/race/:raceId:",
				params: {
					componentName: "race"
				},
				rules: {
					normalize_: setRaceParams
				}
			}
		]
	};

	return routerConfig;

});