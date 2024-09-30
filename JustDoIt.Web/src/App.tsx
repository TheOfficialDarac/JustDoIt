import { Outlet, Route, Routes } from "react-router-dom";
import HomePage from "./pages/HomePage";
import { ProtectedRoute } from "./components/ProtectedRoute";
import { SecretPage } from "./pages/SecretPage";
import { useAuth } from "./hooks/useAuth";
import Layout from "./components/layout/Layout";
import NotFoundPage from "./pages/NotFoundPage";
import ProjectsPage from "./pages/projects/ProjectsPage";
import TasksPage from "./pages/tasks/TasksPage";
import SettingsPage from "./pages/user/SettingsPage";
import RegisterPage from "./pages/user/RegisterPage";
import { LoginPage } from "./pages/user/LoginPage";
import UserSettings from "./components/settings/UserSettings";
import DisplaySettings from "./components/settings/DisplaySettings";
import ProjectPage from "./pages/projects/ProjectPage";

export default function App() {
	const { authToken, user, fetchUserData, logout } = useAuth();

	return (
		<div className='min-h-screen flex flex-col items-center'>
			<Layout
				user={user}
				authToken={authToken}
				logout={logout}
			>
				<Routes>
					<Route
						path='/'
						element={<HomePage />}
					/>
					<Route
						path='/display-settings'
						element={<DisplaySettings />}
					/>
					<Route
						path='/login'
						element={<LoginPage />}
					/>
					<Route
						path='/register'
						element={<RegisterPage />}
					/>
					<Route
						element={
							<ProtectedRoute
								children={<Outlet />}
								authToken={authToken}
							/>
						}
					>
						<Route
							path='/settings'
							element={
								<SettingsPage
									authToken={authToken}
									user={user}
									fetchUserData={fetchUserData}
								/>
							}
						/>
						<Route
							path='/profile'
							element={
								<UserSettings
									user={user}
									authToken={authToken}
									fetchUserData={fetchUserData}
								/>
							}
						/>

						<Route
							path='/secret'
							element={<SecretPage />}
						/>

						<Route
							path='/projects'
							element={<ProjectsPage authToken={authToken} />}
						/>
						<Route
							path='/projects/:projectId'
							element={<ProjectPage authToken={authToken} />}
						/>
						<Route
							path='/tasks/:projectId'
							element={<TasksPage authToken={authToken} />}
						/>
					</Route>

					<Route
						path='*'
						element={<NotFoundPage />}
					/>
				</Routes>
			</Layout>
		</div>
	);
}
