const pug = require("pug");
const fs = require("fs");

class Template {

	public processTemplate = (filepath: string, data: any): string => {
		const options = {
			filename: "./",
			basedir: "./input",
			debug: true,
			cache: false
		};

		const compileFunction = pug.compileFile(`input\\${filepath}`, options);
		const output = compileFunction(data);
		return output;
	}

	/**
	 * From a given template string and data, compile and return the filled in template.
	 */
	public processTemplateString = (templateString: string, data: any): string => {

		const options = {
			filename: "./",
			basedir: "./input",
			debug: true,
			cache: false
		};

		const compileFunction = pug.compile(templateString, options);
		const output = compileFunction(data);
		return output;
	}

	public generateOutputFile = (filepath: string, data: any): void => {
		const output = this.processTemplate(filepath, data);
		fs.writeFile(`output\\${filepath}`, output);
	}

	/**
	 * TEST
	 */
	public processPUG = (filepath: string, data: any): void => {
		// get the skin
		const skinTemplate = this.getSkinTemplate();

		// get and compile the target file. e.g. index.html
		const bodyContent = this.processTemplate(filepath, data);

		// now replace the placeholder in the skin with the body
		var fullDocumentContent = this.processTemplateString(skinTemplate, { "bodyContent": bodyContent });

		//output documnt to file.
		fs.writeFile(`output\\${filepath}`, fullDocumentContent);
	}


	public processPUG = (filepath: string, data: any): void => {
		// get the skin
		const skinTemplate = this.getSkinTemplate();

		// get and compile the target file. e.g. index.html
		const bodyContent = this.processTemplate(filepath, data);

		// now replace the placeholder in the skin with the body
		var fullDocumentContent = skinTemplate.replace("#{bodyContent}", bodyContent);

		// now compile the skin with the already compiled body
		fullDocumentContent = this.processTemplateString(fullDocumentContent, null);

		//output documnt to file.
		fs.writeFile(`output\\${filepath}`, fullDocumentContent);
	}

	public getSkinTemplate = (): string => {
		var contents = fs.readFileSync("input\\skin.html", "utf8");
		return contents;
	}

	public process = (filePath: string, data: any): void => {
	
		var content = fs.readFileSync("input\\skin.html", "utf8");
		var keys = Object.keys(data);
		const open_tag = '<?';
		const close_tag = '/?>';

		var rendered = content.toString();

		for (let i = 0; i < keys.length; i++) {
			var token = open_tag + keys[i] + close_tag;
			rendered = rendered.replace(token, data[keys[i]]);
		}

		return rendered;
	}
}

export = Template;