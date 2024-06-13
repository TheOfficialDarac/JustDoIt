import Header from "./Header";
import Footer from "./Footer";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Home from "../pages/Home";
import Register from "../pages/Register";
import Login from "../pages/Login";
// import Testing from "../LoginModal";

function Layout() {
  return (
    <>
      <Header name="header" />

      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
        </Routes>
      </BrowserRouter>

      <Footer name="footer" />
    </>
  );
}

export default Layout;
