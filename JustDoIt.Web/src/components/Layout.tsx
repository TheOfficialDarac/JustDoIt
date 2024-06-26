// import Login from "../pages/Login";
// import Testing from "../LoginModal";

import Footer from "./Footer";
import Header from "./Header";

function Layout({ children }: any) {
  return (
    <>
      <Header name="header" />
      <main className="max-w-screen-xl w-full flex-1">{children}</main>
      <Footer name="footer" />
    </>
  );
}

export default Layout;
