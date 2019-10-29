// cd C:\Users\mgay\Google Drive\Work\Code\Web\FrimleyFlyers\sitePhantomjs
// phantomjs.exe runner.js

// I've used this as inspiration: https://github.com/knockout/knockout/blob/master/spec/runner.phantom.js
// Modules: https://soledadpenades.com/2013/11/01/modules-in-phantomjs/

// The main thing I learnt was that just poking the page was not good enough. I needed to have a time to wait
// till the page fully loaded with all async calls completed. This is the testFx part.
// Once I have deemed the page fully loaded, the onReady function is called.

"use strict";

var system = require("system");
var page = require("webpage").create();

var core = require("./core.js");
var Helper = require("./helper.js");

var IndexPageHandlers = require("./pageHandlers/index.js");
var FFChampionshipPageHandlers = require("./pageHandlers/ffchampionship.js");
var FFChampionship2017PageHandlers = require("./pageHandlers/ffchampionship2017.js");
var FFChampionship2018PageHandlers = require("./pageHandlers/ffchampionship2018.js");
var FFTrophy2018PageHandlers = require("./pageHandlers/fftrophy2018.js");
var Endure24PageHandlers = require("./pageHandlers/endure24.js");
var PbListPageHandlers = require("./pageHandlers/pbList.js");
var RunsPageHandlers = require("./pageHandlers/runs.js");

(function () {
	console.log("Start");

	core.init(page);

	var handlers = {
		index: new IndexPageHandlers(),
		ffchampionship: new FFChampionshipPageHandlers(),
		ffchampionship2017: new FFChampionship2017PageHandlers(),
		ffchampionship2018: new FFChampionship2018PageHandlers(),
		fftrophy2018: new FFTrophy2018PageHandlers(),
		endure24: new Endure24PageHandlers(),
		pbList: new PbListPageHandlers(),
		runs: new RunsPageHandlers()
	};

	var helper1 = new Helper();
	if (system.args.length > 1) {
		var handlerProperty = system.args[1];
		core.start("http://localhost:53002", handlers[handlerProperty], helper1);
	} else {
		console.log("Must pass first parameter which denotes which page to build");
	}

}());