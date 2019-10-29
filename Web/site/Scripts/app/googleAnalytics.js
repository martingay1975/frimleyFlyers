/*global window, document */
define(['jquery'], function ($) {

	"use strict";

    (function (i, s, o, g, r, a, m) {
        i.GoogleAnalyticsObject = r;
        i[r] = i[r] || function() {
            (i[r].q = i[r].q || []).push(arguments);
        };
        i[r].l = new Date();
        a = s.createElement(o);
        m = s.getElementsByTagName(o)[0];
        a.async = 1;
        a.src = g;
        m.parentNode.insertBefore(a, m);
    }) (window, document, 'script', '//www.google-analytics.com/analytics.js', 'gaTracker');

	// create a default tracker object. 
    gaTracker('create', {
    	trackingId: 'UA-50811124-1',
    	cookieDomain: 'auto',
    	version: '2.0'
    });

    var googleAnalytics = {

		/// sends the pageView's hit.
		sendPageViewHit: function (pageViewObj) {

			var mergedPageViewObj = $.extend({ hitType: "pageview" }, pageViewObj);
			gaTracker('send', mergedPageViewObj);
		},

		/// set the tracker's page - which will get sent on every subsequent hit.... until setPage is called again.
		setPage: function(page, title) {
			gaTracker("set",
			{
				"page": page,
				"title" : title
			});
		}
	};

	//var googleAnalytics = {
	//	sendPageViewHit: function (pageViewObj) { },
	//	setPage: function(page, title) { }
	//};
	return googleAnalytics;
});