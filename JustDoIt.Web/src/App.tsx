import { Route, Routes } from "react-router-dom";
import HomePage from "./pages/HomePage";
import RegisterPage from "./pages/RegisterPage";
import { LoginPage } from "./pages/LoginPage";
import { ProtectedRoute } from "./components/ProtectedRoute";
import { SecretPage } from "./pages/SecretPage";
import { AuthProvider } from "./hooks/useAuth";
import Layout from "./components/layout/Layout";
import SettingsPage from "./pages/SettingsPage";
import NotFoundPage from "./pages/NotFoundPage";
import { usePreferences } from "./hooks/usePreferences";
import { useEffect, useState } from "react";
import ProjectsPage from "./pages/ProjectsPage";

export default function App() {
  const { preferences } = usePreferences();
  // console.log(preferences.theme);
  const [classList, setClassList] = useState<string | undefined>(
    "min-h-screen flex flex-col items-center text-foreground bg-background "
  );

  useEffect(() => {
    setClassList(
      "min-h-screen flex flex-col items-center text-foreground bg-background " +
        preferences?.theme
    );
  }, [preferences]);
  return (
    <div id="app" className={classList}>
      <AuthProvider>
        <Layout>
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            <Route
              path="/secret"
              element={
                <ProtectedRoute>
                  <SecretPage />
                </ProtectedRoute>
              }
            />
            <Route
              path="/settings"
              element={
                <ProtectedRoute>
                  <SettingsPage />
                </ProtectedRoute>
              }
            />
            <Route
              path="/projects"
              element={
                <ProtectedRoute>
                  <ProjectsPage />
                </ProtectedRoute>
              }
            />
            <Route path="*" element={<NotFoundPage />} />
          </Routes>
        </Layout>
      </AuthProvider>
    </div>
  );
}
