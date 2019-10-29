var request = require("request");
var fs = require("fs");
var cheerio = require("cheerio"); // Fast, flexible, and lean implementation of core jQuery designed
var deferred = require("deferred");
var moment = require("moment"); // date/time management
//var RateLimiter = require("request-rate-limiter");
//var limiter = new RateLimiter(60);

//var cookie = "__gads=ID=be44e08bdf670644:T=1516091527:S=ALNI_MahAw0VDRqo_0maF5iq2w3asZSPEQ; __utmz=61341485.1516885268.2.2.utmcsr=frimleyflyers.co.uk|utmccn=(referral)|utmcmd=referral|utmcct=/runs.html; __utmc=61341485; cookiesDisclosureCount=10; __utma=61341485.1337246131.1516091499.1518437598.1518442513.5";
var cookie = "__gads=ID=b6578ecb0adb2cca:T=1539277031:S=ALNI_MZQ2bNkEeiraEDsCK9R_bNjBP4vnQ; __utmz=61341485.1546114485.71.20.utmcsr=resultsemail|utmccn=systememail|utmcmd=email; __utma=61341485.127158723.1477694959.1546117188.1546161158.73; __utmc=61341485; __utmt=1; cookiesDisclosureCount=26; __utmb=61341485.2.10.1546161158"
var WebObject = function() {

	var getHttpGetOptions = function (uri, withEncodingHeader) {

		var uriParts = uri.split("/");
		var host = uriParts[2];

		var requestOptions = {
			uri: uri,
			method: "GET",
			gzip: withEncodingHeader,
			headers: {
				"Accept": "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8",
				//"Accept-Language": "en-GB,en-US;q=0.9,en;q=0.8",
				"Accept-Language": "en-US,en;q=0.9",
				"Cache-Control": "no-cache",
				"Connection": "keep-alive",
				"Cookie": cookie,
				"Host": host,
				"Pragma": "no-cache",
				//"Proxy-Connection": "keep-alive",
				"Upgrade-Insecure-Requests": "1",
				//"User-Agent": "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36"
				"User-Agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36"

			}
		};

		if (withEncodingHeader) {
			requestOptions.headers["Accept-Encoding"] = "gzip, deflate";
		}

		return requestOptions;
	};

	this.requestAsync = function(url, withEncoding) {
		var d = deferred();
		var httpRequestHeader = getHttpGetOptions(url, withEncoding);

		//limiter.request(function(err1, backoff) {
		//	if (err1) {
		//		d.reject(new Error("limiter failed or overflowed. " + err1));
		//	}

			request(httpRequestHeader,
				function (err, resp) {
					if (err) {
						d.reject(new Error("Unable to fetch '" + url + "', reason: " + err));
						return;
					}

					if (resp.statusCode !== 200) {
						d.reject(new Error("Unable to fetch '" + url + "', code: " + resp.statusCode));
						return;
					}

					d.resolve(resp);
				});
		//});
		
		return d.promise();
	};
};
var web = new WebObject();

// represents a reult entry for an athlete.
var ResultEntry = function () {
	this.location = "";
	this.date = null;
	this.time = null;
};

// Parse the HTML page with the athlete's parkrun results and add to the allResultsArray array.
var processAthleteLocationHistory = function (response, allResultsArray) {

	var resultsRowIndex, resultRow, resultColumn, resultColumnsIndex, allResultsArrayEntry,
		$, indexCaptions, cap, location, titleText, atIndex,
		KEY_PHRASE = " at ";

	var uri = response.request.href;
	var body = response.body;
	$ = cheerio.load(body);

	titleText = $("h2").text();
	
	var titleParts = $("title").text().split(' ');
	var resultEntry;
	titleParts.splice(0, 2);
	titleParts.splice(titleParts.length - 1, 1);
	location = titleParts.join(' ');

	var errorString;
	cap = $("table caption");
	if (cap.length < 3) {

		errorString = "\r\n" + "Error in page: " + uri;
		errorString += "\r\nrequest";
		errorString += "\r\n" + JSON.stringify(response.request.headers);
		errorString += "\r\n" + "response";
		errorString += "\r\n" + JSON.stringify(response.headers);
		addToErrorLog(errorString);
		throw "Could not find third table caption in " + body;
	} else {
		errorString = "\r\n" + "Success in page: " + uri;
		errorString += "\r\nrequest";
		errorString += "\r\n" + JSON.stringify(response.request.headers);
		errorString += "\r\n" + "response";
		errorString += "\r\n" + JSON.stringify(response.headers);
		addToErrorLog(errorString);
	}

	//for (indexCaptions = 0; indexCaptions < cap.length; indexCaptions += 1) {
		//var tableCaption = cap[indexCaptions];
	var tableCaption = cap[2];	// The third caption is "All Results"... but different for different countries/languages.
		var captionText = tableCaption.children[0].data;
		// if (captionText === "All Results") {
			var table = tableCaption.parent;

			// find all the result rows for this specific parkrun
			var resultRows = $("tbody tr", table);
			for (resultsRowIndex = 0; resultsRowIndex < resultRows.length; resultsRowIndex += 1) {
				resultRow = resultRows[resultsRowIndex];
				resultEntry = new ResultEntry();
				resultEntry.location = location;
				// loop around all the columns in the results for this parkrun
				for (resultColumnsIndex = 0; resultColumnsIndex < resultRow.children.length; resultColumnsIndex += 1) {
					resultColumn = resultRow.children[resultColumnsIndex];
					switch (resultColumnsIndex) {
						case 0:// date
							var dateText = $("a", resultColumn).text();
							var dateParsed = moment.utc(dateText, "DD/MM/YYYY");
							resultEntry.date = dateParsed;
							break;
						case 3:// time
							var timeText = $(resultColumn).text();
							var timeParsed = moment.utc(timeText, "m:ss");
							resultEntry.time = {
								hour: 0,
								minute: timeParsed.get("minute"),
								second: timeParsed.get("second")
							};
							break;
					}
				}

				allResultsArray.push(resultEntry);
			}
		//}
	//}
};

// The athletes specific parkrun's history.
var getAthleteLocationHistoryAsync = function (uri, allResultsArray) {
	var promise = web.requestAsync(uri, true);
	var returnPromise = promise.then(function (response) {
		try {
			processAthleteLocationHistory(response, allResultsArray);
			console.log("Success: " + uri);
		} catch (e) {
			// keep trying until we get there
			console.log("Try again: " + uri);
			getAthleteLocationHistoryAsync(uri, allResultsArray);
		}
	});

	return returnPromise;
};

// block for a specfic amount of time
var waitABit =function(timeoutMS) {
	var waitTill = new Date(new Date().getTime() + timeoutMS);
	while(waitTill > new Date()){}
}

// sort the resultEntry array by date (fastest first)
var sortByTimeDesc = function (resultEntryA, resultEntryB) {

	var timeA = (resultEntryA.time.minute * 60) + resultEntryA.time.second;
	var timeB = (resultEntryB.time.minute * 60) + resultEntryB.time.second;

	if (timeA > timeB) {
		return 1;
	}

	if (timeA < timeB) {
		return -1;
	}

	return 0;
};

// sort the resultEntry array by date (earliest first)
var sortByDateAsc = function (resultEntryA, resultEntryB) {
	if (resultEntryA.date < resultEntryB.date) {
		return -1;
	}

	if (resultEntryA.date > resultEntryB.date) {
		return 1;
	}

	return 0;
};

// The athlete's history page has been obtained. Now loop around each of the different parkruns the athlete has run in
var athleteHistoryCallback = function (response) {
	var anchor;
	var index;
	var links = [];
	var athleteLocationHistoryPromise = [];

	// loop around the Event Summaries and collect all the 'All' links for the various parkruns this athlete has run in
	var $ = cheerio.load(response.body);
	var anchors = $("#results").find("a");
	for (index = 0; index < anchors.length; index++) {
		anchor = anchors[index];
		if (anchor.attribs.href) {
			var href = anchor.attribs.href;
			href = href.replace("/athletehistory?athleteNumber", "/athletehistory/?athleteNumber");
			if (href.indexOf('/athletehistory/?athleteNumber') > -1) {
				links.push(href);
			}
		}
	}

	// loop around all the parkrun history links for the athlete and call getAthleteLocationHistoryAsync. Add to the allResultsArray.
	var linksIndex;
	var link;
	var allResultsArray = [];
	var allLocationsPromiseArray = [];
	for (linksIndex = 0; linksIndex < links.length; linksIndex += 1) {
		link = links[linksIndex];
		athleteLocationHistoryPromise = getAthleteLocationHistoryAsync(link, allResultsArray);
		allLocationsPromiseArray.push(athleteLocationHistoryPromise);
	}

	// see https://www.npmjs.com/package/deferred#processing-collections
	// map collects all the promises and resolves once all have been completed.
	var allDone = deferred.map(allLocationsPromiseArray);

	// Groups into an object, where the member name is the key. The value is then the array entries pertaining to that key.
	var groupBy = function (xs, key) {
		return xs.reduce(function (rv, x) {
			(rv[x[key]] = rv[x[key]] || []).push(x);
			return rv;
		}, {});
	};

	var groupByReportLength = function (groupByResult) {

		var GroupEntry = function(name, count) {
			this.name = name;
			this.count = count;
		};

		var ret = [];
		for (key in groupByResult) {
			var groupEntry = new GroupEntry(key, groupByResult[key].length);
			ret.push(groupEntry);
		}
		return ret;
	}
	
	var newPromise = allDone.then(function (abc) {

		//allResultsArray.sort(sortByDateAsc);
		allResultsArray.sort(sortByTimeDesc);

		var t = groupBy(allResultsArray, "location");
		var groupEntries = groupByReportLength(t);

		var latestYear = allResultsArray;
		//var latestYear = allResultsArray.filter(result => result.date.year() === 2017);
		//writeResultsToFile(allResultsArray, "martingay");
		//return allResultsArray;
		return latestYear;
	});


	return newPromise;
};

var Athlete = function(athleteNumber, name) {

	var self = this;
	var writeResultsToFile;

	this.athleteNumber = athleteNumber;
	this.name = name;

	// The data for this athlete.
	this.results = [];

	// Get the athletes results and update the results member.
	this.getResultsAsync = function () {
		var athleteHistoryUrl = "http://www.parkrun.org.uk/results/athleteresultshistory/?athleteNumber=" + self.athleteNumber;
		var athleteHistoryPromise = web.requestAsync(athleteHistoryUrl, false);
		var athleteHistoryPromise1 = athleteHistoryPromise.then(athleteHistoryCallback);

		var allResultsDone = athleteHistoryPromise1.then(function (allResults) {
			self.results = allResults;
			writeResultsToFile(self.results, self.name);
			console.log("Written athlete " + self.name);
		}, function() {
			console.log("Failed on athlete " + self.name);
		});

		return allResultsDone;
	};

	// writes entries to a JSON file
	writeResultsToFile = function (results, athleteName) {
		var writeStream = fs.createWriteStream("C:\\temp\\" + athleteName + ".json");

		console.log(athleteName + "\t\t" + results.length);
		results.forEach(function (result) {
			writeStream.write(JSON.stringify(result));
			//console.log("location: " + result.location + "  date: " + result.date.format("DD/MM/YYYY") + "  time: " + result.time.minute + ":" + result.time.second);
		});
		writeStream.end();
	};

	addToErrorLog = function(message) {
		var writeStream = fs.createWriteStream("C:\\temp\\error.txt", { flags: "a" });
		writeStream.write(message + "\r\n");
		writeStream.end();
	}
};

var createAthlete = function(ignore, name, ignore, parkrunId) {
	return new Athlete(parkrunId, name);
};

var martingay = createAthlete(1, "Martin Gay", "995184", "414364");
var jamesball = createAthlete(2, "James Ball", "7396134", "248342");
var chrispeddle = createAthlete(3, "Chris Peddle", "7597223", "399793");
var davidpeddle = createAthlete(4, "David Peddle", "3927231", "79200");
var duncanball = createAthlete(5, "Duncan Ball", "12896333", "56739");
var bobturner = createAthlete(6, "Bob Turner", "381428", "289659");
var alasdairnuttall = createAthlete(7, "Alasdair Nuttall", "1106175", "220582");
var sarahErskine = createAthlete(8, "Sarah Erskine", "4751449", "569008");
var alexHalfacre = createAthlete(9, "Alex Halfacre", "7880452", "912484");
var richardBoese = createAthlete(10, "Richard Boese", "19591034", "37186");
var sarahCampbellFoster = createAthlete(11, "Sarah Campbell-Foster", "", "75812");
var philJelly = createAthlete(12, "Phil Jelly", "1661722", "3893");
var louiseParker = createAthlete(13, "Louise Parker", "", "426881");
var nicholasYewings = createAthlete(14, "Nicholas Yewings", "", "107892");
var jimLaidlaw = createAthlete(15, "Jim Laidlaw", "379140", "1013712");
var leeMarshall = createAthlete(16, "Lee Marshall", "", "76667");
var helenHart = createAthlete(17, "Helen Hart", "", "94048");
var darrenStone = createAthlete(18, "Darren Stone", "5460853", "523541");
var alfieBoese = createAthlete(19, "Alfie Boese", "", "83588");
var emmaMalcolm = createAthlete(20, "Emma Malcolm", "1717915", "344643");
var elinorBoese = createAthlete(21, "Elinor Boese", "", "125634");
var adrianKeaneMunday = createAthlete(22, "Adrian Keane-Munday", "", "2272640");
var oliPeddle = createAthlete(23, "Oli Peddle", "13197801", "953039");
var daveBartlett = createAthlete(24, "Dave Bartlett", "", "2087676");
var tomChurchill = createAthlete(25, "Tom Churchill", "1125412", "91676");
var richardHowden = createAthlete(26, "Rich Howden", "1750767", "414051");
var benGay = createAthlete(27, "Ben Gay", "", "430946");
var juliaBoese = createAthlete(28, "Julia Boese", "", "293582");
var alanBush = createAthlete(29, "Alan Bush", "19641672", "1520236");
var andyPoulter = createAthlete(30, "Andy Poulter", "", "2306222");
var zoeStone = createAthlete(31, "Zoe Stone", "", "449398");
var brunoSilva = createAthlete(32, "Bruno Silva", "", "3201689");
var derekPeddle = createAthlete(33, "Derek Peddle", "", "3174383");
var susanRodrigues = createAthlete(34, "Susan Rodrigues", "", "3414380");
var ashtonPeddle = createAthlete(35, "Ashton Peddle", "", "676392");
var karenPeddle = createAthlete(36, "Karen Peddle", "", "116038");
var kirstieStone = createAthlete(37, "Kirstie Stone", "", "3506857");
var emHowden = createAthlete(38, "Em Howden", "", "3506857");
var charmaineLong = createAthlete(39, "Charmaine Long", "", "2914203");

var athletes = new Array();
//athletes.push(richardBoese);
athletes.push(martingay,
	jamesball,
	chrispeddle,
	davidpeddle,
	duncanball,
	bobturner,
	alasdairnuttall,
	alexHalfacre,
	sarahCampbellFoster,
	louiseParker,
	jimLaidlaw,
	leeMarshall,
	darrenStone,
	daveBartlett,
	tomChurchill,
	richardHowden,
	emmaMalcolm,
	nicholasYewings,
	philJelly,
	richardBoese,
	alanBush,
	adrianKeaneMunday,
	andyPoulter,
	helenHart,
	oliPeddle,
	juliaBoese,
	alfieBoese,
	elinorBoese,
	benGay,
	zoeStone,
	brunoSilva,
	derekPeddle,
	susanRodrigues,
	ashtonPeddle,
	karenPeddle,
	kirstieStone,
	emHowden,
	charmaineLong);

// START HERE!
athletes.forEach(function (athlete) {
	athlete.getResultsAsync();
});
