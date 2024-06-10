import Header from "./Header";
import Footer from "./Footer";
import Testing from "../Testing";

function Layout() {
  return (
    <>
      <Header name="header" />
      {/* <main className="p-4 bg-amber-100 flex-1">
        <div>Here is content</div>
      </main> */}
      <Testing />
      <Footer name="footer" />
    </>
  );
}

export default Layout;
