define(function () {

	var ResponsiveTableCell = function(heading, value) {
		this.heading = heading;
		this.value = value;
	};

	/**
	 * Array of responsiveTableCell
	 * @param {any} row
	 */
	var ResponsiveTableRow = function () {

		var self = this;

		this.addCell = function(heading, value) {
			self.row.push(new ResponsiveTableCell(heading, value));
		};

		this.row = [];
	};

	return ResponsiveTableRow;
})