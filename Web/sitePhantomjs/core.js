"use strict";

(function () {

	var init = function (page) {
		page.onError = function (msg, trace) {
			console.log(msg);
			trace.forEach(function (item) {
				console.log('  ', item.file, ':', item.line);
			});
		};

		page.onConsoleMessage = function (msg) {
			console.log(msg);
		};
	}

	var convertToText = function(obj) {

		try {
			//create an array that will later be joined into a string.
			var string = [];

			//is object
			//    Both arrays and objects seem to return "object"
			//    when typeof(obj) is applied to them. So instead
			//    I am checking to see if they have the property
			//    join, which normal objects don't have but
			//    arrays do.
			if (obj == undefined) {
				return String(obj);
			} else if (typeof (obj) == "object" && (obj.join == undefined)) {
				for (var prop in obj) {
					if (obj.hasOwnProperty(prop)) {
						string.push(prop + ": " + convertToText(obj[prop]));
					}
				};
				return "{" + string.join(",") + "}";

				//is array
			} else if (typeof (obj) == "object" && !(obj.join == undefined)) {
				for (prop in obj) {
					string.push(convertToText(obj[prop]));
				}
				return "[" + string.join(",") + "]";

				//is function
			} else if (typeof (obj) == "function") {
				string.push(obj.toString());

				//all other values can be done with JSON.stringify
			} else {
				string.push(JSON.stringify(obj));
			}

			return string.join(",");
		} catch (e) {
			console.log("Error: " + JSON.stringify(e));
		}
	}

	/**
	 * Wait until the test condition is true or a timeout occurs. Useful for waiting
	 * on a server response or for a ui change (fadeIn, etc.) to occur.
	 *
	 * @param testFx javascript condition that evaluates to a boolean,
	 * it can be passed in as a string (e.g.: "1 == 1" or "$('#bar').is(':visible')" or
	 * as a callback function.
	 * @param onReady what to do when testFx condition is fulfilled,
	 * it can be passed in as a string (e.g.: "1 == 1" or "$('#bar').is(':visible')" or
	 * as a callback function.
	 * @param timeOutMillis the max amount of time to wait. If not specified, 3 sec is used.
	 */
	var waitFor = function (testFx, onReady, onTimeout, timeOutMillis) {
		var maxtimeOutMillis = timeOutMillis ? timeOutMillis : 5001, //< Default Max Timeout is 3s
			start = new Date().getTime(),
			condition = false,
			interval = setInterval(function () {
				if ((new Date().getTime() - start < maxtimeOutMillis) && !condition) {
					// If not time-out yet and condition not yet fulfilled
					condition = (typeof (testFx) === "string" ? eval(testFx) : testFx()); //< defensive code
				} else {
					if (!condition) {
						// If condition still not fulfilled (timeout but condition is 'false')
						console.log("'waitFor()' timeout");
						onTimeout ? onTimeout() : phantom.exit(1);
					} else {
						clearInterval(interval); //< Stop this interval
						// Condition fulfilled (timeout and/or condition is 'true')
						typeof (onReady) === "string" ? eval(onReady) : onReady(); //< Do what it's supposed to do once the condition is fulfilled
					}
				}
			}, 1000); //< repeat check every 1000ms
	}

	/**
	 * Starts a page request, passing the url, and handlers
	 * @param {any} url
	 * @param {any} hasPageLoadedCallback
	 * @param {any} pageLoadedCallback
	 */
	var start = function (url, waitForHandlers, helper) {

		var loadUrl = url + "/" + waitForHandlers.urlPathPortion;
		console.log("Loading " + loadUrl);
		page.open(loadUrl, function (status) {

			if (status !== "success") {
				console.log("Page could not be found. Try starting the web Catastrophic failire.");
			} else {
				waitFor(
					/*testFx*/ function () { return page.evaluate(waitForHandlers.hasPageLoadedCallback); },
					/*onReady*/ function () {
						try {
							var fs = require('fs');
							
							// wrap up the object in to a string so it can be transported in the callback in page.evaulate.
							console.log("onReady");
							var helperInstanceAsString = "var helperInstance = " + convertToText(helper);
							
							// get the html to write
							var result = page.evaluate(waitForHandlers.pageLoadedCallback, page, helperInstanceAsString); /* passing 'page' makes it the first parameter in evaluate callback */
							
							// write the html to file.
							var outputPath = "../site/output/" + waitForHandlers.outputPath;
							fs.write(outputPath, result);
							console.log("Saving to:" + outputPath);

						} finally {
							page.close();
							phantom.exit();
						}
					},
					/* onTimeout */
					function () {
						console.log("Timed out!");
						phantom.exit();
					},
					/*timeOutMillis*/
					10000
				);
			}
		});
	};

	module.exports = {
		init: init,
		waitFor: waitFor,
		start: start
	};

}());

