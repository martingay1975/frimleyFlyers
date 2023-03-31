import React from "react";
import { Helmet } from "react-helmet";

export const HeadTagComponent: React.FC = () => {
	return (
		<Helmet>
			<meta charSet="UTF-8" />
			<meta
				name="viewport"
				content="width=device-width, initial-scale=1.0"
			/>

			<meta name="description" content="Frimley Flyers Running Group" />
			<meta name="author" content="Martin Gay" />

			<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
			<script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>

			<link
				rel="stylesheet"
				type="text/css"
				href="content/bootstrap.css"
			/>
			<link
				rel="stylesheet"
				type="text/css"
				href="content/bootstrap-theme.css"
			/>
			<link rel="stylesheet" type="text/css" href="content/main.css" />

			<title>Frimley Flyers</title>
		</Helmet>
	);
};
