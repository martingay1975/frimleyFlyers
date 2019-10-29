define(['cell'], function(Cell) {

	"use strict";

	var PointsColumn = function (color) {

		this.addHeading = function (headingRows) {

			var headingCell1 = new Cell();
			headingCell1.text = "Total";
			headingCell1.backgroundColor = color;
			headingCell1.rowSpan = headingRows.length;
			headingRows[0].addCell(headingCell1);
		};

		this.addData = function (person, rowsArray) {
			var nameCell = new Cell();
			nameCell.text = person.points;
			nameCell.backgroundColor = color;
			nameCell.css = " bigger";
			rowsArray[0].addCell(nameCell);
		};
	};

	return PointsColumn;
});