import { Route, Routes } from "react-router-dom";
import HomePage from "./pages/HomePage";
import RegisterPage from "./pages/RegisterPage";
import { LoginPage } from "./pages/LoginPage";
import { ProtectedRoute } from "./components/ProtectedRoute";
import { Secret } from "./pages/SecretPage";
import { AuthProvider } from "./hooks/useAuth";
import Layout from "./components/Layout";

export default function App() {
  // const isUserLoggedIn = true;
  return (
    <>
      <Layout>
        <AuthProvider>
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
          </Routes>
        </AuthProvider>
      </Layout>
    </>
  );
}
