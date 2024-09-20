import { Outlet, Route, Routes } from "react-router-dom";
import HomePage from "./pages/HomePage";
import RegisterPage from "./pages/user/RegisterPage";
import { LoginPage } from "./pages/user/LoginPage";
import { ProtectedRoute } from "./components/ProtectedRoute";
import { SecretPage } from "./pages/SecretPage";
import { AuthProvider, useAuth } from "./hooks/useAuth";
import Layout from "./components/layout/Layout";
import SettingsPage from "./pages/user/SettingsPage";
import NotFoundPage from "./pages/NotFoundPage";
import { usePreferences } from "./hooks/usePreferences";
import ProjectsPage from "./pages/projects/ProjectsPage";

export default function App() {
	const { preferences } = usePreferences();

	const { authToken, user, fetchUserData } = useAuth();

	return (
		<div
			id='app'
			className={
				"min-h-screen flex flex-col items-center text-foreground bg-background " +
				preferences?.theme
			}
		>
			<Layout>
				<Routes>
					<Route
						path='/'
						element={<HomePage />}
					/>
					<Route
						path='/login'
						element={<LoginPage />}
					/>
					<Route
						path='/register'
						element={<RegisterPage />}
					/>
					<Route element={<ProtectedRoute children={<Outlet />} />}>
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
							path='/secret'
							element={<SecretPage />}
						/>
						<Route
							path='/projects'
							element={
								<ProjectsPage
									authToken={authToken}
									user={user}
									fetchUserData={fetchUserData}
								/>
							}
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
