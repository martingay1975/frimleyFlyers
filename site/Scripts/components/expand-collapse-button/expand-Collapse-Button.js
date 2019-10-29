/*global define*/
define(['knockout', 'text!./expand-Collapse-Button.html', 'bindingHandlerToggle'], function (ko, componentTemplate) {

	"use strict";

	var ExpandCollapseComponentViewModel = function (params) {

		var self = this;

		if (params.inverse) {
			// We want to take the observable and "not" any reads or writes.
			this.toggleFlagObservable = ko.computed( {
				read: function () {
					return !params.toggleFlagObservable();
				},
				write: function(value) {
					params.toggleFlagObservable(!value);
				}
			});
		} else {
			// Use the observable directly without intervention.
			this.toggleFlagObservable = params.toggleFlagObservable;
		}

		this.dispose = function () {

			if (params.inverse) {
			    self.toggleFlagObservable.dispose();
			}
		};
	};

	return { viewModel: ExpandCollapseComponentViewModel, template: componentTemplate };
});
