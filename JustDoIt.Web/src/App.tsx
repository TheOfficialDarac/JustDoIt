import { Outlet, Route, Routes } from "react-router-dom";
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

export default function App() {
	const { preferences } = usePreferences();

	return (
		<div
			id='app'
			className={
				"min-h-screen flex flex-col items-center text-foreground bg-background " +
				preferences?.theme
			}
		>
			<AuthProvider>
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
								element={<SettingsPage />}
							/>
							<Route
								path='/secret'
								element={<SecretPage />}
							/>
						</Route>

						<Route
							path='*'
							element={<NotFoundPage />}
						/>
					</Routes>
				</Layout>
			</AuthProvider>
		</div>
	);
}
