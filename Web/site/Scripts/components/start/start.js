/*global require*/
define(['knockout', 'text!./start.html'], function (ko, componentTemplate) {

	"use strict";

	var StartComponentViewModel;


	StartComponentViewModel = function () {
	};

	return { viewModel: StartComponentViewModel, template: componentTemplate };
});
