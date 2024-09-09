import React from "react";
import ReactDOM from "react-dom/client";

// 1. import `NextUIProvider` component
import { NextUIProvider } from "@nextui-org/react";

import "./index.css";
import App from "./App.tsx";
import { BrowserRouter } from "react-router-dom";
import { PreferenceProvider } from "./hooks/usePreferences.tsx";

ReactDOM.createRoot(document.getElementById("root")!).render(
	<React.StrictMode>
		<PreferenceProvider>
			<NextUIProvider>
				<BrowserRouter>
					<App />
				</BrowserRouter>
			</NextUIProvider>
		</PreferenceProvider>
	</React.StrictMode>
);
