﻿define(function() {

	"use strict";

	var raceDistance = {
		fiveKm: 0,
		tenKm : 1,
		halfMarathon: 2,
		twohalfKm: 3,
		tenMiles: 4,

		getName: function (index) {
			switch (index) 
			{
				case 0:
					return "5km";
				case 1:
					return "10km";
				case 2:
					return "Half Marathon";
				case 3:
					return "2.5km";
				case 4:
					return "10 miles";
				default:
					throw "Unsupported value";
			}
		}
	};

	return raceDistance;
});