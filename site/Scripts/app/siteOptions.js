/*global define*/
define(["jquery", "environment"], function($, environment) {

	"use strict";

	var SiteOptions = function() {

		var self = this;
		var DATE_LAST_UPDATED_KEY = "dateLastUpdated";
		var init;

		this.dateLastUpdated = null;

		this.isOutOfDate = function () {
			var localStorageDateLastUpdated = localStorage.getItem(DATE_LAST_UPDATED_KEY);
			var hasChanged = self.dateLastUpdated != localStorageDateLastUpdated;
			localStorage.setItem(DATE_LAST_UPDATED_KEY, self.dateLastUpdated);
			return hasChanged;
		};

		init = function () {
			self.dateLastUpdated = environment.siteOptions.dateLastUpdated;
		};

		init();
	};

	return new SiteOptions();
});

