
const Application = require("./Scripts/core/application");

try {
	// Start the app.
	const app = new Application();
	app.main();
} catch (e) {
	console.error(e);
} 
