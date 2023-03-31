import React from "react";

export const FooterComponent: React.FC = () => {
	return (
		<footer className="whiteBkg">
			<ul className="nav navbar-nav navbar-right relative">
				<li>
					<a
						href="http://twitter.com/FrimleyFlyers"
						target="_blank"
						rel="noreferrer"
					>
						<svg viewBox="0- 0 90 90">
							<use xlinkHref="#twitter"></use>
						</svg>
					</a>
				</li>
				<li>
					<a
						href="http://www.facebook.com/FrimleyF/"
						target="_blank"
						rel="noreferrer"
					>
						<svg className="facebook" viewBox="0 0 70 70">
							<use xlinkHref="#facebook"></use>
						</svg>
					</a>
				</li>
				<li>
					<a
						href="http://www.facebook.com/FrimleyF/"
						target="_blank"
						rel="noreferrer"
					>
						<img
							alt=""
							className="icon"
							src="/Scripts/components/navigation/images/facebook.png"
						/>
					</a>
				</li>
				<li>
					<a
						href="http://www.strava.com/clubs//frimleyflyers"
						target="_blank"
						rel="noreferrer"
					>
						<img
							alt=""
							className="icon"
							src="/Scripts/components/navigation/images/strava.png"
						/>
					</a>
				</li>
			</ul>
		</footer>
	);
};
