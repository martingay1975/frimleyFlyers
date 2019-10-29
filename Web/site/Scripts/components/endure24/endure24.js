define(["siteViewModel", "timeSpan", "responsiveTableRow"], function (siteViewModel, TimeSpan, ResponsiveTableRow) {

	"use strict";

	var endure24ViewModelComponent = function () {
		siteViewModel.pageTitleObservable("Endure 24");

		var buildReponsiveTableRow = function (name, lapTimesArray) {

			var row = new ResponsiveTableRow();
			row.addCell("Name", name);
			for (var lapCount = 0; lapCount < lapTimesArray.length; lapCount++) {
				var lapString = lapCount + 1;
				row.addCell("Lap " + lapString, lapTimesArray[lapCount] ? lapTimesArray[lapCount].toString(): "");
			}
			return row;
		};

		var lapTimes2017TeamA = [
			buildReponsiveTableRow("Anthony Madden / Kelly Murfin", [new TimeSpan(0, 40, 14), new TimeSpan(0, 40,12), new TimeSpan(1,27,33 ), new TimeSpan(0, 56, 58)]),
			buildReponsiveTableRow("Alex Halfacre", [new TimeSpan(0, 31,42), new TimeSpan(0, 31,3), new TimeSpan(0, 40,5), new TimeSpan(0, 44,32)]),
			buildReponsiveTableRow("Louise Parker", [new TimeSpan(0, 52,36), new TimeSpan(0, 53,18)]),
			buildReponsiveTableRow("Richard / Alfie Boese", [new TimeSpan(0, 42, 35), new TimeSpan(0, 39, 50), new TimeSpan(0, 52, 40), new TimeSpan(1,5,28), new TimeSpan(0,46,49)]),
			buildReponsiveTableRow("Duncan Ball", [new TimeSpan(0, 45, 57), new TimeSpan(0,48, 42 ), new TimeSpan(0, 55,36), new TimeSpan(0, 49,47)]),
			buildReponsiveTableRow("Sarah Campbell-Foster", [new TimeSpan(0, 50,14), new TimeSpan(0, 55,7), new TimeSpan(1, 0,26), new TimeSpan(0, 57, 33)]),
			buildReponsiveTableRow("Martin Gay", [new TimeSpan(0, 50, 26), new TimeSpan(0, 53, 7), new TimeSpan(0, 50, 42)]),
			buildReponsiveTableRow("Lee Marshall", [new TimeSpan(0, 46, 20), new TimeSpan(0, 52, 5), new TimeSpan(0,47, 38)])
		];

		var lapTimes2017TeamB = [
			buildReponsiveTableRow("Darren Stone", [new TimeSpan(0, 38, 42), new TimeSpan(0, 39, 24), new TimeSpan(0, 42, 23), new TimeSpan(0, 41, 52), new TimeSpan(0, 57, 36)]),
			buildReponsiveTableRow("Chris Peddle", [new TimeSpan(0, 40, 20), new TimeSpan(0, 38, 32), new TimeSpan(0, 43, 8), new TimeSpan(0, 44, 11)]),
			buildReponsiveTableRow("Adrian Keane-Munday", [new TimeSpan(0, 47, 32), new TimeSpan(0, 48, 10), new TimeSpan(0, 54,27), new TimeSpan(0, 54, 23)]),
			buildReponsiveTableRow("David Peddle", [new TimeSpan(0, 39, 53), new TimeSpan(0, 41, 13), new TimeSpan(0, 47, 46), new TimeSpan(0, 49, 17)]),
			buildReponsiveTableRow("Andy Poulter", [new TimeSpan(0, 39, 32), new TimeSpan(0, 40, 50), new TimeSpan(0, 44, 25), new TimeSpan(0, 49, 44)]),
			buildReponsiveTableRow("Oli Peddle", [new TimeSpan(0, 40, 33), new TimeSpan(0, 39, 15), new TimeSpan(1, 24, 9)]),
			buildReponsiveTableRow("James Ball", [new TimeSpan(0, 40, 1), new TimeSpan(1, 17, 7), new TimeSpan(0, 44, 15)]),
			buildReponsiveTableRow("Emma Malcolm", [new TimeSpan(0, 57, 47), new TimeSpan(1,0,19), new TimeSpan(1, 2, 30)])
		];

		var lapTimes2016TeamA = [
			buildReponsiveTableRow("Chris Peddle", [new TimeSpan(0, 38, 42), new TimeSpan(0, 38, 26), new TimeSpan(0, 41, 11), new TimeSpan(0, 43, 6)]),
			buildReponsiveTableRow("Alasdair Nuttall", [new TimeSpan(0, 39, 17), new TimeSpan(0, 38, 20), new TimeSpan(0, 39, 45), new TimeSpan(0, 40, 59), new TimeSpan(0, 44, 22)]),
			buildReponsiveTableRow("James Ball", [new TimeSpan(0, 39, 3), new TimeSpan(0, 39, 1), new TimeSpan(0, 43, 56), new TimeSpan(0, 40, 43), new TimeSpan(0, 41, 37)]),
			buildReponsiveTableRow("Roni Norcross", [new TimeSpan(0, 52, 16), new TimeSpan(0, 54, 49), new TimeSpan(0, 57, 57)]),
			buildReponsiveTableRow("Alex Halfacre", [new TimeSpan(1, 24, 16), new TimeSpan(0, 32, 54), new TimeSpan(0, 45, 22), new TimeSpan(0, 39, 37), new TimeSpan(0, 37, 15)]),
			buildReponsiveTableRow("Martin Gay", [new TimeSpan(0, 43, 55), new TimeSpan(0, 44, 25), new TimeSpan(0, 47, 35), new TimeSpan(0, 47, 17)]),
			buildReponsiveTableRow("Emma Malcolm", [new TimeSpan(1, 8, 43), new TimeSpan(1, 10,11), new TimeSpan(1,18,14 ), new TimeSpan(0, 58, 55)])
		];

		var lapTimes2016TeamB = [
			buildReponsiveTableRow("Louise Parker", [new TimeSpan(0, 50, 50), new TimeSpan(0, 49, 51), new TimeSpan(0, 52, 14), new TimeSpan(0, 55, 1)]),
			buildReponsiveTableRow("Nicholas/Ethan Yewings", [new TimeSpan(0, 41, 28), new TimeSpan(0, 42, 0), new TimeSpan(0, 48, 8), new TimeSpan(0, 34, 12)]),
			buildReponsiveTableRow("David Peddle", [new TimeSpan(0, 41, 33), new TimeSpan(0, 41, 48), new TimeSpan(0, 56, 0), new TimeSpan(0, 49, 44)]),
			buildReponsiveTableRow("Kelly Murfin", [new TimeSpan(0, 52, 43), new TimeSpan(0, 52, 48)]),
			buildReponsiveTableRow("Darren Stone", [new TimeSpan(0, 38, 12), new TimeSpan(0, 40, 47), new TimeSpan(0, 42, 40)]),
			buildReponsiveTableRow("Phil Dunston", [new TimeSpan(0, 45, 28), new TimeSpan(0, 44, 35), new TimeSpan(0, 49, 24)]),
			buildReponsiveTableRow("Duncan Ball", [new TimeSpan(0, 44, 28), new TimeSpan(0, 51, 18), new TimeSpan(0, 50, 51)]),
			buildReponsiveTableRow("Sarah Campbell-Foster", [new TimeSpan(0, 52, 31), new TimeSpan(0, 59, 46), new TimeSpan(0, 59, 8), new TimeSpan(0, 58, 46)])
		];

		var lapTimes2015TeamA = [
			buildReponsiveTableRow("Alex Halfacre", [new TimeSpan(0, 34, 20), new TimeSpan(0, 33, 57), new TimeSpan(0, 39, 55), new TimeSpan(0, 44, 21), new TimeSpan(0, 38, 40)]),
			buildReponsiveTableRow("Nicholas Yewings", [new TimeSpan(0, 40, 35), new TimeSpan(0, 40, 55), new TimeSpan(0, 44, 54), new TimeSpan(0, 47, 54), new TimeSpan(0, 44, 27)]),
			buildReponsiveTableRow("David Peddle", [new TimeSpan(0, 50, 32), new TimeSpan(0, 41, 14), new TimeSpan(0, 42, 0), new TimeSpan(0, 48, 48), new TimeSpan(0, 43, 44)]),
			buildReponsiveTableRow("Alasdair Nuttall", [new TimeSpan(0, 39, 30), new TimeSpan(0, 39, 32), new TimeSpan(0, 41, 22), new TimeSpan(0, 43, 43), new TimeSpan(0, 42,43)]),
			buildReponsiveTableRow("Duncan Ball", [new TimeSpan(0, 45, 20), new TimeSpan(0, 44, 0), new TimeSpan(0, 54,0), new TimeSpan(0, 51, 0)]),
			buildReponsiveTableRow("Bob Turner", [new TimeSpan(0, 43, 40), new TimeSpan(0, 44, 0), new TimeSpan(0, 46, 40), new TimeSpan(0, 44, 40)]),
			buildReponsiveTableRow("Kelly Murfin", [new TimeSpan(0, 51, 14), new TimeSpan(1, 0, 0), new TimeSpan(1, 3, 30)])
		];

		var lapTimes2015TeamB = [
			buildReponsiveTableRow("Phil Jelly", [new TimeSpan(0, 34, 0), new TimeSpan(0, 34, 0), new TimeSpan(0, 38, 0), new TimeSpan(0, 38, 0), null]),
			buildReponsiveTableRow("Martin Gay", [new TimeSpan(0, 42, 51), new TimeSpan(0, 42, 51), new TimeSpan(0, 45, 22), new TimeSpan(0, 54, 26), new TimeSpan(0, 58, 31)]),
			buildReponsiveTableRow("Chris Peddle", [new TimeSpan(0, 42, 51), new TimeSpan(0, 42, 51), new TimeSpan(0, 45, 22), new TimeSpan(0, 54, 26), new TimeSpan(0, 58, 31)]),
			buildReponsiveTableRow("Louise Parker", [new TimeSpan(0, 51,50), new TimeSpan(0, 51, 31), new TimeSpan(0, 55,28), new TimeSpan(1,0,0), new TimeSpan(0, 57, 13)]),
			buildReponsiveTableRow("James Ball", [new TimeSpan(0, 36, 20), new TimeSpan(0, 36, 53), new TimeSpan(0, 40, 32), new TimeSpan(0, 39, 23), new TimeSpan(0, 38, 50)]),
			buildReponsiveTableRow("Phil Dunston", [new TimeSpan(0, 37, 33), new TimeSpan(0, 36,8), new TimeSpan(0, 39,26), new TimeSpan(0, 39,37)]),
			buildReponsiveTableRow("Emma Malcolm", [new TimeSpan(0, 52,0), new TimeSpan(0, 54, 40), new TimeSpan(1, 4,0)])
		];

		
		this.lapTimes2017TeamA = lapTimes2017TeamA;
		this.lapTimes2017TeamB = lapTimes2017TeamB;
		this.lapTimes2016TeamA = lapTimes2016TeamA;
		this.lapTimes2016TeamB = lapTimes2016TeamB;
		this.lapTimes2015TeamA = lapTimes2015TeamA;
		this.lapTimes2015TeamB = lapTimes2015TeamB;
	};

	return endure24ViewModelComponent;
});

