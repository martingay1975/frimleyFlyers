/*global define, require, window, document, navigator, environment*/
/* jslint browser: true */
define(["knockout", "siteViewModel"], function (ko, siteViewModel) {

	"use strict";

	// this is invoked from index.html and is run immediately. See the data-main attribute where the require.js gets included.
	// Registering the knockout components: These are keyed by their name, and "require" a javaScript module to initialize the component. 
	// They key, e.g. network can be used statically in the html, or can be part of the parameter of a component knockout binding.

	var createRegistrationObject = function (name, omitViewModel) {
		var path = "/scripts/components" + "/" + name + "/" + name;
		var textTemplatePath = "text!" + path + ".html";
		var registrationObject = {
			template: { require: textTemplatePath }
		};

		if (!omitViewModel) {
			var viewModelPath = path + ".js";
			registrationObject.viewModel = { require: viewModelPath };
		}

		return registrationObject;
	};

	var registerViewModelAndTemplate = function (name) {
		var component = createRegistrationObject(name, false);
		ko.components.register(name, component);
	};

	/**
	 * Registers just a template. No need for any binding with a viewModel.
	 * @param {} name 
	 * @returns {} 
	 */
	var registerTemplate = function(name) {
		var component = createRegistrationObject(name, true);
		ko.components.register(name, component);
	}

	// ==========================
	// controls

	// controls - shared (currently shared, consumed by more than one component
	registerViewModelAndTemplate("navigation");
	registerViewModelAndTemplate("ffchampionship");
	registerViewModelAndTemplate("ffchampionship2017");
	registerViewModelAndTemplate("ffchampionship2018");
	registerViewModelAndTemplate("ffchampionship2019");
	registerViewModelAndTemplate("fftrophy2018");
	registerTemplate("svg-images");
	registerViewModelAndTemplate("rules");
	registerViewModelAndTemplate("responsiveTable");

	registerViewModelAndTemplate("leaguetable");
	ko.components.register("header1", { require: "components/header/header" });
	ko.components.register("start", { require: "components/start/start" });
	ko.components.register("mini-leaguetable", { require: "components/mini-leaguetable/mini-leaguetable" });
	ko.components.register("pbList", { require: "components/pbList/pbList" });
	ko.components.register("expand-collapse-button", { require: "components/expand-collapse-button/expand-Collapse-Button" });

	registerViewModelAndTemplate("home");
	registerViewModelAndTemplate("endure24");
	registerViewModelAndTemplate("club");
	registerViewModelAndTemplate("runs");

	// ==========================
	try {
		// Start the application, index.html has a siteViewModel backing it
		ko.applyBindings(siteViewModel);
	} catch (e) {
		window.alert("There was an error initializing. " + e);
	}
});
