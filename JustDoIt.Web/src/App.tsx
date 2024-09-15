import { Route, Routes } from "react-router-dom";
import HomePage from "./pages/HomePage";
import RegisterPage from "./pages/User/RegisterPage";
import { LoginPage } from "./pages/User/LoginPage";
import { ProtectedRoute } from "./components/ProtectedRoute";
import { SecretPage } from "./pages/SecretPage";
import { AuthProvider } from "./hooks/useAuth";
import Layout from "./components/layout/Layout";
import SettingsPage from "./pages/User/SettingsPage";
import NotFoundPage from "./pages/NotFoundPage";
import { usePreferences } from "./hooks/usePreferences";
import { useEffect, useState } from "react";
import ProjectsPage from "./pages/Projects/ProjectsPage";
// import TasksPage from "./pages/TasksPage";
import ProjectTasksPage from "./pages/Projects/ProjectTasksPage";
import EditTaskPage from "./pages/Tasks/EditTaskPage";
import CreateTaskPage from "./pages/Tasks/CreateTaskPage";
import CreateProjectPage from "./pages/Projects/CreateProjectPage";
import EditProjectPage from "./pages/Projects/EditProjectPage";
import AddMembersPage from "./pages/Projects/AddMembersPage";

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
            <Route
              path="/tasks/edit/:id"
              element={
                <ProtectedRoute>
                  <EditTaskPage />
                </ProtectedRoute>
              }
            />
            <Route
              path="/tasks/:id"
              element={
                <ProtectedRoute>
                  <ProjectTasksPage />
                </ProtectedRoute>
              }
            />
            <Route
              path="/tasks/create/:id"
              element={
                <ProtectedRoute>
                  <CreateTaskPage />
                </ProtectedRoute>
              }
            />
            <Route
              path="/projects/create"
              element={
                <ProtectedRoute>
                  <CreateProjectPage />
                </ProtectedRoute>
              }
            />
            <Route
              path="/project/edit/:id"
              element={
                <ProtectedRoute>
                  <EditProjectPage />
                </ProtectedRoute>
              }
            />
            <Route
              path="/project/members/add/:id"
              element={<AddMembersPage />}
            />
            <Route path="*" element={<NotFoundPage />} />
          </Routes>
        </Layout>
      </AuthProvider>
    </div>
  );
}
