"use strict";
var fs = require("fs");
var Template = require("./template");
var Application = (function () {
    function Application() {
        this.main = function () {
            var template = new Template();
            template.process("index.html", { name: "Martin" });
        };
    }
    return Application;
}());
module.exports = Application;
//# sourceMappingURL=application.js.map