var gulp = require("gulp");
var sass = require("gulp-sass");
var debug = require("gulp-debug");
var watch = require("gulp-watch");
var childProcess = require("child_process");

var homePath = "C:\\Users\\Slop\\";
var workPath = "C:\\Users\\mgay\\";

var env = {
	sitePath: homePath + "Google Drive\\Work\\Code\\Web\\FrimleyFlyers",
	webServerPath: homePath + "Google Drive\\Work\\Code\\Web\\WebDataEntry.Owin\\bin\\Debug\\\WebDataEntry.Owin.exe"
};

var sassConfig = {
	inputDirectory: "Content/main.scss",
	outputDirectory: "Content",
	options: {
		outputStyle: "expanded"
	}
};

var buildcss = function() {
	return gulp
		.src(sassConfig.inputDirectory)
		.pipe(sass(sassConfig.options).on("error", sass.logError))
		.pipe(gulp.dest(sassConfig.outputDirectory));
}

gulp.task("build-css", buildcss);

gulp.task("watch", function () {
	watch("Scripts/**/**/*.scss")
		.on("change", function (file) {
			buildcss();
			console.log(`Compiled Sass ${file}`);
		});
});

gulp.task("run-webserver", function() {
	console.log("running " + env.webServerPath);
	var options = {detached: true, stdio: 'ignore'};
	var webServer = childProcess.spawn('cmd.exe', ["/c", env.webServerPath], options,
			(error, stdout, stderr) => {
				if (error) {
					console.error(`exec error: ${error}`);
					return;
				}
				console.log(`stdout: ${stdout}`);
				console.log(`stderr: ${stderr}`);
			});
});