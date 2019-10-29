define(function() {

	"use strict";

	var PaletteCycle = function (colorArray) {

		var self = this;

		var currentIndex = 0;

		this.getNextColor = function () {
			currentIndex += 1;
			if (currentIndex >= colorArray.length) {
				currentIndex = 0;
			}

			return self.getCurrentColor();
		};

		this.getCurrentColor = function () {
			return colorArray[currentIndex];
		};
	};

	return PaletteCycle;
});