var Environment = function() {

	/**
	 * Synchronous call to get the siteOptions.
	 * @returns {} 
	 */
	this.readSiteOptions = function() {

		var getEnvironmentCall = $.ajax({
			url: "/res/json/siteOptions.json",
			dataType: "json", // responseJson property filled in the response, converting to a javascript object.
			cache: false, // need to ensure we do not get a browser cached response. Otherwise, it would appear as logging in as a different user (which was a cached response)
			async: false, // need to make this call synchronously as we cannot continue until the environment is set, as the rest of the site relies on this data.
			type: "GET"
		});

		var siteOptions = getEnvironmentCall.responseJSON;
		return siteOptions;
	};

	this.siteOptions = this.readSiteOptions();
};

var gbl_environment = new Environment();