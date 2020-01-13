define(['cell', 'raceDistance'], function (Cell, raceDistance) {

	"use strict";

	var addHeading, addData, formatPbString;

	formatPbString = function(person, seasonYear, raceDistance) {
		var distancePb = person.getSeason(seasonYear).getTime(raceDistance);
		distancePb === null ? "-" : distancePb.toString();
		return distancePb;
	};

	addData = function(seasonYear, person, rowsArray) {
		var topCell, secondCell, thirdCell, fourthCell;
		topCell = new Cell().setText("5km: " + formatPbString(person, seasonYear, raceDistance.fiveKm));
		secondCell = new Cell().setText("10km: " + formatPbString(person, seasonYear, raceDistance.tenKm));
		thirdCell = new Cell().setText("10 Mile: " + formatPbString(person, seasonYear, raceDistance.tenMile));
		fourthCell = new Cell().setText("Half M: " + formatPbString(person, seasonYear, raceDistance.halfMarathon));
		rowsArray[0].addCell(topCell);
		rowsArray[1].addCell(secondCell);
		rowsArray[2].addCell(thirdCell);
		rowsArray[3].addCell(fourthCell);
	};

	addHeading = function (headingRows) {

		var headingCell1 = new Cell();
		headingCell1.text = "PB";

		headingRows[0].addCell(headingCell1);
		headingRows[1].addCell();
	};

	return {
		addHeading: addHeading,
		addData: addData
	};
});