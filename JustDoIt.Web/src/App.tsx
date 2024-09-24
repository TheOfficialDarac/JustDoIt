import { Outlet, Route, Routes } from "react-router-dom";
import HomePage from "./pages/HomePage";
import { ProtectedRoute } from "./components/ProtectedRoute";
import { SecretPage } from "./pages/SecretPage";
import { useAuth } from "./hooks/useAuth";
import Layout from "./components/layout/Layout";
import NotFoundPage from "./pages/NotFoundPage";
import ProjectsPage from "./pages/projects/ProjectsPage";
import TasksPage from "./pages/projects/TasksPage";
import { LoginPage } from "./pages/User/LoginPage";
import RegisterPage from "./pages/User/RegisterPage";
import SettingsPage from "./pages/User/SettingsPage";

export default function App() {
  const { authToken, user, fetchUserData } = useAuth();

  return (
    <div className="min-h-screen flex flex-col items-center">
      <Layout>
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
          <Route element={<ProtectedRoute children={<Outlet />} />}>
            <Route
              path="/settings"
              element={
                <SettingsPage
                  authToken={authToken}
                  user={user}
                  fetchUserData={fetchUserData}
                />
              }
            />
            <Route path="/secret" element={<SecretPage />} />
            <Route
              path="/projects"
              element={<ProjectsPage authToken={authToken} user={user} />}
            />
            <Route
              path="/tasks/:projectId"
              element={<TasksPage authToken={authToken} />}
            />
          </Route>

          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </Layout>
    </div>
  );
}
