import { Navigate, Route, Routes } from "react-router-dom";
import HomePage from "./pages/HomePage";
import RegisterPage from "./pages/RegisterPage";
import { LoginPage } from "./pages/LoginPage";
import { ProtectedRoute } from "./components/ProtectedRoute";
import { Secret } from "./pages/SecretPage";
import { AuthProvider } from "./hooks/useAuth";
import Layout from "./components/Layout";
import { useEffect, useState } from "react";

export default function App() {
  // let isLoggedIn = false;

  // const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);

  // const toggleButton = () => {
  //   setIsLoggedIn((prev) => !prev);
  // };

  // useEffect(() => {
  //   const user = window.localStorage.getItem("user");
  //   console.log(user);
  //   toggleButton();
  // }, []);

  return (
    <>
      <AuthProvider>
        <Layout
        // isLoggedIn={isLoggedIn} toggle={toggleButton}
        >
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            <Route
              path="/secret"
              element={
                <ProtectedRoute>
                  <Secret />
                </ProtectedRoute>
              }
            />
            <Route path="*" element={<Navigate to="/" />} />
          </Routes>
        </Layout>
      </AuthProvider>
    </>
  );
}
