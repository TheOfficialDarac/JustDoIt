import Layout from "./components/Layout";

export default function App() {
  const isUserLoggedIn = true;
  return <>{isUserLoggedIn ? <Layout /> : "no"}</>;
}
