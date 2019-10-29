define(['stringUtil'], function (stringUtil) {

	"use strict";

	var TimeSpan = function (hour, minute, second) {
		this.hour = hour || 0;
		this.minute = minute || 0;
		this.second = second || 0;
	};

	TimeSpan.prototype.totalSeconds = function () {
		return this.second + (this.minute * 60) + (this.hour * 3600);
	};

	TimeSpan.prototype.toString = function () {
		var ret = "";
		
		if (this.hour > 0) {
			ret = this.hour + ":";
		}

		ret += stringUtil.pad(2, this.minute, '0') + "." + stringUtil.pad(2, this.second, '0');
		return ret;
	};

	return TimeSpan;
});