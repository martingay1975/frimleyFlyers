/*global define*/
define(['jquery', 'jquery-color', 'stringUtil', 'nameColumn', 'pointsColumn', 'raceColumn', 'row', 'competitorsData', 'paletteCycle', 'text!./mini-leaguetable.html'],
	function ($, jqueryColor, stringUtil, NameColumn, PointsColumn, RaceColumn, Row, competitorsData, PaletteCycle, componentTemplate) {

	"use strict";

	var MiniLeagueTableComponentViewModel,

	MiniLeagueTableComponentViewModel = function (params) {

		var columnPaletteCycle = new PaletteCycle([$.Color("rgba(176,196,222,0.5)"),
												   $.Color("rgba(245,245,245,0.5)")]),

			columns = [new NameColumn(columnPaletteCycle.getNextColor()),
					   new PointsColumn(columnPaletteCycle.getNextColor())],
			init;

		if (!(this instanceof MiniLeagueTableComponentViewModel)) {
			throw "Must invoke the function MiniLeagueTableComponentViewModel with the new operator";
		}

		var self = this,
			rows;

		this.tableRows = []; // array of Row.

		init = function () {

			// add all the rows
			competitorsData.getCompetitors("2017").forEach(function (person) {

				rows = [new Row()];

				// put in the default columns at the start
				columns.forEach(function (column) {
					column.addData(person, rows);
				});

				// add the rows to the table (used as the viewModel)
				self.tableRows = self.tableRows.concat(rows);
			});
		};

		init();
	};

	return { viewModel: MiniLeagueTableComponentViewModel, template: componentTemplate };

});
