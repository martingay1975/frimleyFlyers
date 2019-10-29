define(['knockout'], function (ko) {

	"use strict";

	var Cell = function () {

		this.fullColSpan = 0;
		this.colSpan = ko.observable();
		this.rowSpan = null;
		this.text = " ";
		this.css = ko.observable("");
		this.backgroundColor = null;
		this.raceId = null;
		this.personId = null;

		//this.visible = ko.observable(true);
		this.visible = ko.pureComputed({
			read: function() {
				return !(this.hasCss("invisibleCell"));
			},
			write: function (value) {
				this.removeCss(value ? "invisibleCell" : "visibleCell");
				this.appendCss(value ? "visibleCell" : "invisibleCell");
			},
			owner: this
		});

		this.raceHeading = false;
		this.isToggle = false;

	};

	Cell.prototype = {
		revertToFullColSpan: function() {
			this.colSpan(this.fullColSpan);
		},

		setFullColSpan: function(colspan) {
			this.fullColSpan = colspan;
			this.colSpan(colspan);
		},

		setText: function(text) {
			this.text = text;
			return this;
		},

		setBackgroundColor: function(color) {
			this.backgroundColor = color;
			return this;
		},

		setRaceId: function(raceId) {
			this.raceId = raceId;
			this.appendCss('race race' + +raceId);
			return this;
		},

		// Summary: Does className exist in the css property?
		hasCss: function(className) {
			var csses,
				css,
				cssIndex;

			// shortcut
			if (this.css().indexOf(className) === -1)
				return false;

			csses = this.css().split(" ");
			for (cssIndex = 0; cssIndex < csses.length; cssIndex += 1) {
				css = csses[cssIndex];
				if (css === className) {
					return true;
				}
			}

			return false;
		},

		removeCss: function(className) {
			var csses = this.css().split(" "),
				css,
				newcsses = [],
				cssIndex = 0;

			for (cssIndex = 0; cssIndex < csses.length; cssIndex += 1) {
				css = csses[cssIndex];
				if (css !== className) {
					newcsses.push(css);
				}
			}

			this.css(newcsses.join(" "));
		},

		appendCss: function (className) {
			var current;
			if (!this.hasCss(className)) {
				current = this.css();
				if (current !== "") {
					current += " ";
				}

				this.css(current + className);
			}

			return this;
		}
	};

	return Cell;
});