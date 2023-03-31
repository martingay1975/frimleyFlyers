import React from "react";
import "./App.css";
import { TableComponent } from "./components/table";
import { HeadTagComponent } from "./components/head-tag";
import { FooterComponent } from "./components/footer";

export const App: React.FC = () => {
	return (
		<>
			<HeadTagComponent></HeadTagComponent>

			<h2 className="row">
				<h2 className="pageTitle">Season 2023</h2>
				<div className="main-container">
					<TableComponent></TableComponent>
				</div>
			</h2>
			<FooterComponent></FooterComponent>
		</>
	);
};
