const fs = require("fs");
const Template = require("./template");

class Application {

	public main = () => {
		var template = new Template();
		template.process("index.html", { name: "Martin" });
	}
}

export = Application