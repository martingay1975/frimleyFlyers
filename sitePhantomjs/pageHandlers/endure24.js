"use strict";

(function () {
	var endure24PageHandlers = function () {

		/**
		 * When the DOM sees #homeContent then consider the page loaded
		 * @returns {} 
		 */
		this.hasPageLoadedCallback = function () {
			return document.body.querySelector('#endEndure24') !== null;
		};

		this.pageLoadedCallback = function (page, helperObjAsString) {

			// var helperInstance =
			eval(helperObjAsString);

			// I think running in the context of the page (dom).... not in the phantomJS context, or in this objects context
			//var newContent = context.helper.removeRequireInjectedScripts(context.page.content);
			var newContent = helperInstance.removeRequireInjectedScripts(page.content);

			return newContent;
		};

		this.outputPath = "endure24.html";
		this.urlPathPortion = "#endure24";
	};

	module.exports = endure24PageHandlers;
}());