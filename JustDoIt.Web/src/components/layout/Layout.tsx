import Footer from "./Footer";
import Header from "./Header";

interface Props {
  children: React.ReactNode;
}

function Layout({ children }: Props) {
  return (
    <>
      <Header />
      <main className="max-w-screen-xl w-full flex-1">{children}</main>
      <Footer name="footer" />
    </>
  );
}

export default Layout;
