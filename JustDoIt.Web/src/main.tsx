import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";

// 1. import `NextUIProvider` component
import { NextUIProvider } from "@nextui-org/react";
import Layout from "./components/Layout.tsx";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <NextUIProvider>
      <Layout />
    </NextUIProvider>
  </React.StrictMode>
);
