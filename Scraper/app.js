
var express = require("express");
var request = require("request");
var fs = require("fs");
var cheerio = require("cheerio");
var deferred = require("deferred");

var app = express();


var getHttpGetOptions = function (uri) {
	var requestOptions = {
		uri: uri,
		method: "GET",
		headers: {
			"Accept": "text/html, application /xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8",
			"Accept-Language": "en-GB,en-US;q=0.8,en;q=0.6",
			"Cache-Control": "no-cache",
			"Host": "www.parkrun.org.uk",
			"Pragma": "no-cache",
			"Proxy-Connection": "keep-alive",
			"Upgrade-Insecure-Requests": "1",
			"User-Agent": "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.87 Safari/537.36"
		}
	};
	
	return requestOptions;
};

var deferredRequest = function (url) {
	var d = deferred();
	
	request(url, function (err, resp, body) {
		if (err) {
			d.reject(new Error("Unable to fetch '" + url + "', reason: " + err));
			return;
		}
		
		if (resp.statusCode !== 200) {
			d.reject(new Error("Unable to fetch '" + url + "', code: " + resp.statusCode));
			return;
		}
		
		d.resolve(body);
	});
	
	return d.promise();
};


var processAthleteLocationHistory = function(body, allResultsArray) {
	var resultsRowIndex, resultRow, resultColumn, resultColumnsIndex, allResultsArrayEntry, 
		$, indexCaptions, cap, location, titleText, atIndex, 
		KEY_PHRASE = " at ";
	
	$ = cheerio.load(body);
	
	titleText = $("h2").text();
	atIndex = titleText.indexOf(KEY_PHRASE);
	location = titleText.substring(atIndex + KEY_PHRASE.length);
	
	cap = $("table caption");
	for (indexCaptions = 0; indexCaptions < cap.length; indexCaptions += 1) {
		var tableCaption = cap[indexCaptions];
		var captionText = tableCaption.children[0].data;
		if (captionText === "All Results") {
			var table = tableCaption.parent;
			
			// find all the result rows for this specific parkrun
			var resultRows = $("tbody tr", table);
			for (resultsRowIndex = 0; resultsRowIndex < resultRows.length; resultsRowIndex += 1) {
				resultRow = resultRows[resultsRowIndex];
				allResultsArrayEntry = { location: location, date: "", time: "" };
				
				// loop around all the columns in the results for this parkrun
				for (resultColumnsIndex = 0; resultColumnsIndex < resultRow.children.length; resultColumnsIndex += 1) {
					resultColumn = resultRow.children[resultColumnsIndex];
					switch (resultColumnsIndex) {
						case 0:// date
							allResultsArrayEntry.date = $("a", resultColumn).text();
							break;
						case 3:// time
							allResultsArrayEntry.time = $(resultColumn).text();
							break;
					}
				}
				
				allResultsArray.push(allResultsArrayEntry);
			}
		}
	}
};

var getAthleteLocationHistoryAsync = function (uri, allResultsArray) {
	var promise = deferredRequest(getHttpGetOptions(uri));
	promise.done(function (body) {
		processAthleteLocationHistory(body, allResultsArray);
	});
};


var getStatsAsync = function(athleteNumber) {

	var athleteHistoryUrl = "http://www.parkrun.org.uk/results/athleteresultshistory/?athleteNumber=" + athleteNumber;
	
	// make first page
	request(getHttpGetOptions(athleteHistoryUrl), 
		function (error, response, body) {

			var $, anchors, index, anchor, links = [];
	   
			$ = cheerio.load(body);
			anchors = $("#results").find("a");
			for (index = 0; index < anchors.length; index++) {
				anchor = anchors[index];
				if (anchor.attribs.href) {
					var href = anchor.attribs.href;
					if (href.indexOf('/results/athletehistory?athleteNumber') > -1) {
						links.push(href);
					}
				}
			}

			var linksIndex, link, allResultsArray = [];
			for (linksIndex = 0; linksIndex < links.length; linksIndex++) {
				link = links[linksIndex];
				getAthleteLocationHistoryAsync(link, allResultsArray);
			}

			console.log("number of runs: " + allResultsArray.length);
		});
};

//app.get('/', function (req, res) {
	
	var promise = getStatsAsync(414364);

//});

//var port = 53004;
//app.listen(port, function(err) {
//	console.log('running on port ' + port);
//});