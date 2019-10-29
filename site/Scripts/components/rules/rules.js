/*global define*/
define(['knockout', 'stringUtil', 'race'], function (ko, stringUtil, Race) {

	"use strict";

	var RulesComponentViewModel, getPointsString;

	getPointsString = function (pointsScheme) {
		var pos = "", index, result = "";
		for (index = 0; index < pointsScheme.length; index++) {

			var positionVal = index + 1;
			if (positionVal % 10 === 1 && positionVal !== 11) {
				pos = positionVal + "st";
			} else if (positionVal % 10 === 2 && positionVal !== 12) {
				pos = positionVal + "nd";
			} else if (positionVal % 10 === 3 && positionVal !== 13) {
				pos = positionVal + "rd";
			} else {
				pos = positionVal + "th";
			}

			result += stringUtil.format("{0}-{1}pts, ", pos, pointsScheme[index]);
		}

		return result;
	}

	RulesComponentViewModel = function (params) {

		if (!(this instanceof RulesComponentViewModel)) {
			throw "Must invoke the function RulesComponentViewModel with the new operator";
		}

		var self = this;

		this.isCollapsed = ko.observable(false);
		this.pointsSchemeObservable = params.pointsSchemeObservable;
		this.pointsPbBonusObservable = params.pointsPbBonusObservable;
		this.takeNBestScoresObservable = params.takeNBestScoresObservable;
		this.entryCostObservable = params.entryCostObservable;
		this.pointsSchemeStringComputed = ko.pureComputed(function() {
			return getPointsString(self.pointsSchemeObservable());
		}, this);
	};
	

	return RulesComponentViewModel;

});
