define(['stringUtil', 'cell'], function(stringUtil, Cell) {

	"use strict";

	var RaceColumn;

	RaceColumn = function (race, backgroundColor) {

		// a race has n columns, where n = number of events + 1 for the points and + 1 for % off PB
		var getBackgroundColor;
		var createFirstRowHeading;
		var createLastRowHeading;
		var createToggleRowHeading;
		var DEFAULT_POST_COLUMNS = [new Cell().setText("PB"), new Cell().setText("%"), new Cell().setText("Pts").appendCss("points")];

		createFirstRowHeading = function(numOfColumns) {
			var topRowCell;
			topRowCell = new Cell();
			topRowCell.text = race.label + " (" + race.distanceString + ")";
			topRowCell.setFullColSpan(numOfColumns);
			topRowCell.backgroundColor = backgroundColor;
			topRowCell.setRaceId(race.id);
			topRowCell.raceHeading = true;

			return topRowCell;
		};

		createToggleRowHeading = function (numOfColumns) {
			var toggleCell = new Cell();
			toggleCell.setFullColSpan(numOfColumns);
			toggleCell.backgroundColor = backgroundColor;
			toggleCell.setRaceId(race.id);
			toggleCell.isToggle = true;

			return toggleCell;
		};

		createLastRowHeading = function (row) {

			race.raceEvents.forEach(function (raceEvent) {
				var secondRowCell = new Cell().setText(raceEvent.dateString).setBackgroundColor(backgroundColor).setRaceId(race.id).appendCss('racetime');
				row.addCell(secondRowCell);
			});

			DEFAULT_POST_COLUMNS.forEach(function (cell) {
				cell.backgroundColor = backgroundColor;
				cell.setRaceId(race.id);
				row.addCell(cell);
			});
		};

		this.addRaceHeading = function (headingRows) {

			var numOfColumns = race.raceEvents.length + DEFAULT_POST_COLUMNS.length;
				
			headingRows[0].addCell(createFirstRowHeading(numOfColumns));
			headingRows[1].addCell(createToggleRowHeading(numOfColumns));
			createLastRowHeading(headingRows[2]);
		};

		this.addRaceData = function(seasonYear, person, rows) {

			var raceResult;
			var	eventResult;
			var	pbCell;
			var	raceTimeCell;
			var	pcCell;
			var	pointsCell;
			var	backgroundClr;
			var	overrideBackgroundColor;

			// loop around each of the events in the race series.
			
			//console.time("iterate raceEvents");
			race.raceEvents.forEach(function (raceEvent) {
				eventResult = raceEvent.getResultEntry(person.name);

				raceTimeCell = new Cell();

				if (eventResult && eventResult.isBest && race.raceEvents.length > 1) {
					backgroundClr = backgroundColor.blend("#DDDDDD");
				} else {
					backgroundClr = backgroundColor;
				}

				raceTimeCell.text = eventResult ? stringUtil.format("{0}", eventResult.time.toString()) : "-";
				raceTimeCell.backgroundColor = backgroundClr;
				raceTimeCell.appendCss('racetime');
				raceTimeCell.setRaceId(race.id);
				rows[0].addCell(raceTimeCell);
			});

			raceResult = race.findInPositions(person.name);

			overrideBackgroundColor = getBackgroundColor(raceResult);

			pbCell = new Cell();
			pbCell.text = person.getSeason(seasonYear).getTime(race.distance) === null ? "-" : person.getSeason(seasonYear).getTime(race.distance);
			pbCell.backgroundColor = backgroundClr;
			pbCell.setRaceId(race.id);
			rows[0].addCell(pbCell);

			pcCell = new Cell();
			pcCell.text = raceResult ? raceResult.percentageFromPb.toFixed(2) + "%" : "-";
			pcCell.backgroundColor = backgroundClr;
			pcCell.setRaceId(race.id);
			rows[0].addCell(pcCell);

			pointsCell = new Cell();
			pointsCell.text = raceResult ? raceResult.points : "-";
			pointsCell.backgroundColor = overrideBackgroundColor;
			pointsCell.setRaceId(race.id);
			pointsCell.appendCss("points");
			rows[0].addCell(pointsCell);
		};

		getBackgroundColor = function(raceResult) {

			if (!raceResult) {
				return backgroundColor;
			}

			switch (raceResult.position) {

			case 1:
				return $.Color("#FFD700"); // Gold
			case 2:
				return $.Color("#C0C0C0"); // Silver
			case 3:
				return $.Color("#A67D3D"); // Bronze
			default:
				return backgroundColor;
			}
		};
	};

	RaceColumn.showDetailColumns = function(isShow, raceId, headings, rows) {

		var cellVisibleOnOrOff, setColspanToOne;

		// Summary: private function which decides if the cell should be made invisible.
		cellVisibleOnOrOff = function(cell) {
			// only interrogate cells that have the correct raceId
			if (cell.raceId === raceId) {

				// below the top heading
				if (!cell.hasCss("points")) {
					cell.visible(isShow);
				}
			}
		};

		setColspanToOne = function(cell) {
			if (cell.raceId === raceId) {
				isShow ? cell.revertToFullColSpan() : cell.colSpan(1);
			}
		};

		// turn off heading cells?
		headings[0].cells.forEach(setColspanToOne);
		headings[1].cells.forEach(setColspanToOne);
		headings[2].cells.forEach(cellVisibleOnOrOff);

		// loop around all competitor rows
		rows.forEach(function(row) {
			// loop around all cells in a row.
			row.cells.forEach(cellVisibleOnOrOff);
		});
	};

	return RaceColumn;
});