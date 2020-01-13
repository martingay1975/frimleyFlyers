"use strict";

(function () {
	var ffchampionshipPageHandlers = function () {

		/**
		 * When the DOM sees #homeContent then consider the page loaded
		 * @returns {} 
		 */
		this.hasPageLoadedCallback = function () {
			return document.body.querySelector('#leagueTableEnd') !== null;
		};

		this.pageLoadedCallback = function (page, helperObjAsString) {

			// var helperInstance =
			eval(helperObjAsString);

			// I think running in the context of the page (dom).... not in the phantomJS context, or in this objects context
			//var newContent = context.helper.removeRequireInjectedScripts(context.page.content);
			var newContent = helperInstance.removeRequireInjectedScripts(page.content);

			return newContent;
		};

		this.outputPath = "ffchampionship2019.html";
		this.urlPathPortion = "#ffchampionship2019";
	};

	module.exports = ffchampionshipPageHandlers;
}());