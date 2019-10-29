/*global define*/
define(['knockout'], function (ko) {

//    <button data-bind="toggle: isHidden">Show / Hide</button>
//      <!-- instead of -->
//    <button data-bind="click: function(){isHidden(!isHidden());}">Show / Hide</button>

    "use strict";

    ko.bindingHandlers.toggle = {
        init: function (element, valueAccessor) {
            var value = valueAccessor();
            ko.applyBindingsToNode(element, {
                click: function () {
                    value(!value());
                }
            });
        }
    };
});