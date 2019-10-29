"use strict";
var pug = require("pug");
var fs = require("fs");
var Template = (function () {
    function Template() {
        var _this = this;
        this.processTemplate = function (filepath, data) {
            var options = {
                filename: "./",
                basedir: "./input",
                debug: true,
                cache: false
            };
            var compileFunction = pug.compileFile("input\\" + filepath, options);
            var output = compileFunction(data);
            return output;
        };
        /**
         * From a given template string and data, compile and return the filled in template.
         */
        this.processTemplateString = function (templateString, data) {
            var options = {
                filename: "./",
                basedir: "./input",
                debug: true,
                cache: false
            };
            var compileFunction = pug.compile(templateString, options);
            var output = compileFunction(data);
            return output;
        };
        this.generateOutputFile = function (filepath, data) {
            var output = _this.processTemplate(filepath, data);
            fs.writeFile("output\\" + filepath, output);
        };
        /**
         * TEST
         */
        this.process = function (filepath, data) {
            // get the skin
            var skinTemplate = _this.getSkinTemplate();
            // get and compile the target file. e.g. index.html
            var bodyContent = _this.processTemplate(filepath, data);
            // now replace the placeholder in the skin with the body
            var fullDocumentContent = _this.processTemplateString(skinTemplate, { "bodyContent": bodyContent });
            //output documnt to file.
            fs.writeFile("output\\" + filepath, fullDocumentContent);
        };
        this.process3 = function (filepath, data) {
            // get the skin
            var skinTemplate = _this.getSkinTemplate();
            // get and compile the target file. e.g. index.html
            var bodyContent = _this.processTemplate(filepath, data);
            // now replace the placeholder in the skin with the body
            var fullDocumentContent = skinTemplate.replace("#{bodyContent}", bodyContent);
            // now compile the skin with the already compiled body
            fullDocumentContent = _this.processTemplateString(fullDocumentContent, null);
            //output documnt to file.
            fs.writeFile("output\\" + filepath, fullDocumentContent);
        };
        this.getSkinTemplate = function () {
            var contents = fs.readFileSync("input\\skin.html", "utf8");
            return contents;
        };
    }
    return Template;
}());
module.exports = Template;
//# sourceMappingURL=template.js.map