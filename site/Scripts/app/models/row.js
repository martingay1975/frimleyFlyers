define(['cell'], function(Cell) {

	"use strict";

	var Row = function() {
		this.cells = []; // array of cells.

		this.addCell = function(cell) {
			if (!cell) {
				cell = new Cell();
			}

			this.cells.push(cell);
		};
	};

	return Row;
});