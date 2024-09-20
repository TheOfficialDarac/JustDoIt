// import React from "react";
import ReactDOM from "react-dom/client";

// 1. import `NextUIProvider` component
import { NextUIProvider } from "@nextui-org/react";

import "./index.css";
import App from "./App.tsx";
import { BrowserRouter } from "react-router-dom";
import { PreferenceProvider } from "./hooks/usePreferences.tsx";
import { AuthProvider } from "./hooks/useAuth.tsx";

ReactDOM.createRoot(document.getElementById("root")!).render(
	// <React.StrictMode>
	<PreferenceProvider>
		<NextUIProvider>
			<BrowserRouter>
				<AuthProvider>
					<App />
				</AuthProvider>
			</BrowserRouter>
		</NextUIProvider>
	</PreferenceProvider>
	// </React.StrictMode>
);
