import React, { useState, useEffect, useRef } from "react";

export const TimerComponent = () => {
	console.log("Running Timer Component");

	const [timer, setTimer] = useState(0);
	const [startTime, setStartTime] = useState(Date.now());
	const timerHandle = useRef(0);

	useEffect(() => {
		console.log("Mounting Timer Component");
		timerHandle.current = setInterval(() => {
			console.log("callback called - startTime: " + startTime);
			const now = Date.now();
			setTimer(Math.floor((now - startTime) / 1000));
		}, 1000);

		return () => {
			console.log("Unmounting Timer Component " + timerHandle.current);
			clearTimeout(timerHandle.current);
		};
	}, [startTime]);

	return (
		<>
			<p>Start Time {startTime}</p>
			<p>Handle {timerHandle.current}</p>
			<span>Timer: </span> <span>{timer}secs</span>
			<button
				onClick={() => setStartTime((prevValue) => prevValue - 10000)}
			>
				Skip 10 seconds
			</button>
		</>
	);
};
