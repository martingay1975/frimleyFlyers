"use strict";

(function () {

	var Helper = function() {

		this.removeText = function(startMarker, endMarker, content, keepEndMarker) {
			var idx = content.indexOf(startMarker);
			var newContent = content;
			var newContent1, newContent2;

			while (idx > -1) {

				var endIdx = -1;
				if (keepEndMarker) {
					endIdx = newContent.indexOf(endMarker, idx);
				} else {
					endIdx = newContent.indexOf(endMarker, idx) + endMarker.length;
				}

				newContent1 = newContent.substring(0, idx);
				newContent2 = newContent.substring(endIdx);
				newContent = newContent1 + newContent2;
				idx = newContent.indexOf(startMarker);
			}

			return newContent;
		};

		this.removeAttribute = function(attribute, content) {
			var idx = content.indexOf(attribute);
			var newContent = content;
			var newContent1 = "", newContent2 = "";
			var openingAttributeSpeechMarkIdx;
			var endingAttributeSpeechMarkIdx;
			while (idx > -1) {
				openingAttributeSpeechMarkIdx = newContent.indexOf("\"", idx);
				endingAttributeSpeechMarkIdx = newContent.indexOf("\"", openingAttributeSpeechMarkIdx + 1);
				endingAttributeSpeechMarkIdx++;
				newContent1 = newContent.substring(0, idx);
				newContent2 = newContent.substring(endingAttributeSpeechMarkIdx);
				newContent = newContent1 + newContent2;
				idx = newContent.indexOf(attribute);
			}

			return newContent;
		}

		this.logObject = function(obj) {
			console.log(JSON.stringify(obj));
		};

		this.removeRequireInjectedScripts = function(content) {
			var newContent = content;
			newContent = this.removeText("<!-- start of require injected scripts -->", "</head>", newContent, true);
			newContent = this.removeText("<!-- development-start -->", "<!-- development-end -->", newContent, false);
			newContent = this.removeAttribute("data-bind", newContent);
			newContent = this.removeText("<!-- ", "-->", newContent, false);
			return newContent;
		};
	};

	module.exports = Helper;
}());


