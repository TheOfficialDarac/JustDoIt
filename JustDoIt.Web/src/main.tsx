import React from "react";
import ReactDOM from "react-dom/client";

// 1. import `NextUIProvider` component
import { NextUIProvider } from "@nextui-org/react";

import "./index.css";
import App from "./App.tsx";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <NextUIProvider className="min-h-screen flex flex-col items-center">
      <App />
    </NextUIProvider>
  </React.StrictMode>
);
