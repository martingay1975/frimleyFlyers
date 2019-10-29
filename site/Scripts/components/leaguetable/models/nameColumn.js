define(['cell'], function(Cell) {

	"use strict";

	var NameColumn;

	NameColumn = function(color) {
		
		this.addHeading = function (headingRows) {

			var headingCell1 = new Cell();
			headingCell1.text = "Name";
			headingCell1.rowSpan = headingRows.length;
			headingCell1.backgroundColor = color;

			headingRows[0].addCell(headingCell1);
		};

		this.addData = function(person, rowsArray) {
			var nameCell = new Cell();
			nameCell.text = person.name;
			nameCell.backgroundColor = color;
			nameCell.personId = person.id;
			nameCell.appendCss("competitor-name");
			rowsArray[0].addCell(nameCell);
		};

	};

	return NameColumn;
});