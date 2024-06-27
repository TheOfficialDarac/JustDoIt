import React from "react";
import ReactDOM from "react-dom/client";

// 1. import `NextUIProvider` component
import { NextUIProvider } from "@nextui-org/react";

import "./index.css";
import App from "./App.tsx";
import { BrowserRouter } from "react-router-dom";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <NextUIProvider className="min-h-screen flex flex-col items-center">
      <BrowserRouter>
        <App />
      </BrowserRouter>
    </NextUIProvider>
  </React.StrictMode>
);
