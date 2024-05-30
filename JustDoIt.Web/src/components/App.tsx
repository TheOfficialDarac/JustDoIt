import Header from "./Header";
import Footer from "./Footer";

function App() {
  return (
    <>
      <Header name="Header loading works" />

      <main className="container px-atuo mx-auto py-3 bg-slate-600">
        <p className="">
          Lorem ipsum dolor sit amet, consectetur adipisicing elit. Adipisci,
          voluptatum?
        </p>
      </main>

      <Footer name="Footer loading works" />
    </>
  );
}

export default App;
