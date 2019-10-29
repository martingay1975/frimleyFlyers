define(['cell'], function(Cell) {

	var NavigationColumn;

	NavigationColumn = function(color) {
		
		this.addHeading = function (headingRows) {

			var headingCell1 = new Cell();
			headingCell1.text = "";
			headingCell1.rowSpan = headingRows.length;
			headingCell1.backgroundColor = color;

			headingRows[0].addCell(headingCell1);
		};

		this.addData = function (person, rowsArray, competitorCount) {

			var dataCell = new Cell();
			dataCell.rowSpan = competitorCount * rowsArray.length;
			dataCell.text = null;
			dataCell.backgroundColor = color;
			dataCell.isNavigation = true;

			rowsArray[0].addCell(dataCell);
		};
	};

	return NavigationColumn;

});