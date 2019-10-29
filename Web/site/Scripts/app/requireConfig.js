/*global environment*/
var require = {
	baseUrl: "Scripts",
	urlArgs: "v=" + gbl_environment.siteOptions.version,
	paths: {
		// third-party
		crossroads: "ThirdParty/crossroads/dist/crossroads",
		hasher: "ThirdParty/hasher/dist/js/hasher",
		jquery: "ThirdParty/jquery/dist/jquery",
		"jquery-color" : "ThirdParty/jquery-color/jquery.color",
		"jquery-dateFormat": "ThirdParty/jquery-dateFormat/dateFormat",
		knockout: "ThirdParty/knockout/dist/knockout-3.4.2.debug",
		"knockout-projections": "ThirdParty/knockout-projections/dist/knockout-projections",
		"knockout-mapping": "ThirdParty/knockout-mapping/knockout.mapping",
		signals: "ThirdParty/js-signals/dist/signals",
		text: "ThirdParty/requirejs-text/text",

		// app
		router: "app/router", // Routing / url hash
		routerConfig: "app/routerConfig", // Config for the routing,
		siteOptions: "app/siteOptions", // Representation of siteOptions.js
		siteViewModel: "app/siteViewModel", // The root object, allow easy access of objects
		stringUtil: "app/stringUtil", // String utility class
		urlBuilder: "app/urlBuilder", // Url building utility class
		environment: "app/environment",	// Environment with siteOptions
		googleAnalytics: "app/googleAnalytics", // Google Analytics api initiator
		polyfill: "app/polyfill",	// Polyfills for ES6 functions

		// app/bindingHandlers
		bindingHandlersFormatDate: "app/bindingHandlers/formatDate", // format date binding handler
		bindingHandlerToggle: "app/bindingHandlers/toggle", // toggles the value of a ko observable boolean.
		attrUndefined: "app/bindingHandlers/attrUndefined",

		raceDatabase: "app/models/raceDatabase",
		raceData: "app/models/raceData",
		person: "app/models/person",
		race: "app/models/race",
		timeSpan: "app/models/timeSpan",
		cell: "app/models/cell",
		row: "app/models/row",
		raceDistance: "app/models/raceDistance",
		competitorsData: "app/models/competitorsData",
		paletteCycle: "app/models/paletteCycle",

		raceEventResultEntry: "app/models/raceEventResultEntry",
		nameColumn: "components/leagueTable/models/nameColumn",
		pbColumn: "components/leagueTable/models/pbColumn",
		pointsColumn: "components/leagueTable/models/pointsColumn",
		raceColumn: "components/leagueTable/models/raceColumn",
		navigationColumn: "components/leagueTable/models/navigationColumn",

		responsiveTableRow: "components/responsiveTable/responsiveTableRow"
	},
	shim : {
		"environment": { exports: "gbl_environment" }	// needs the path in environment as well.
	}
};
