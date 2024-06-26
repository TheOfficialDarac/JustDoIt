import { BrowserRouter, Route, Routes } from "react-router-dom";
import AuthorizeView, { AuthorizedUser } from "./components/AuthorizeView";
import LogoutLink from "./components/LogoutLink";
import Layout from "./components/Layout";
import Register from "./pages/Register";
import Home from "./pages/Home";
import Login from "./pages/Login";

export default function App() {
  // const isUserLoggedIn = true;
  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route
            path="/"
            element={
              <AuthorizeView>
                <span>
                  <LogoutLink>
                    Logout <AuthorizedUser value="email" />
                    <Layout>
                      <Home></Home>
                    </Layout>
                  </LogoutLink>
                </span>
              </AuthorizeView>
            }
          />
          <Route
            path="/login"
            element={
              <Layout>
                <Login />
              </Layout>
            }
          />
          <Route
            path="/register"
            element={
              <Layout>
                <Register />
              </Layout>
            }
          />
        </Routes>
      </BrowserRouter>
    </>
  );
}
