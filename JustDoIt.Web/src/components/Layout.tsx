import Header from "./Header";
import Footer from "./Footer";

function Layout() {
  return (
    <>
      <Header name="" />
      <main className="container mx-auto p-4 bg-amber-100 flex-1">
        <div>Here is content</div>
      </main>
      <Footer name="" />
    </>
  );
}

export default Layout;
