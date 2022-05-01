import { useState } from "react";
import { TimerComponent } from "./timerComponent";

function App() {
	const [show, setShow] = useState(false);

	const clickHandler = (value) => {
		setShow(!value);
	};

	console.log("Run Component");
	return (
		<>
			<button onClick={() => clickHandler(show)}>
				{show ? "Hide" : "Show"}{" "}
			</button>
			{show && <TimerComponent />}
		</>
	);
}

export default App;
