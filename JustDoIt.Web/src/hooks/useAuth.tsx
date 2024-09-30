import {
	createContext,
	useContext,
	useMemo,
	ReactNode,
	useState,
	useEffect,
	useCallback,
} from "react";
import { useNavigate } from "react-router-dom";
import { parseJwt, UserResponse } from "../helpers/Types";

interface AuthContextType {
	user: UserResponse;
	authToken: string;
	login: (token: string, rememberme: boolean) => Promise<void>;
	logout: () => void;
	fetchUserData: () => void;
}

const AuthContext = createContext<AuthContextType | null>(null);

interface Props {
	children: ReactNode;
}

export const AuthProvider = ({ children }: Props) => {
	const [authToken, setAuthToken] = useState<string>("");
	const navigate = useNavigate();

	const setUserInitial = useCallback(async () => {
		await fetch("/api/v1/auth/data", {
			headers: {
				"Content-Type": "application/json",
				Authorization: `Bearer ${authToken}`,
			},
		}).then(async (response) => {
			const json = await response.json();
			return json;
		});
	}, [authToken]);
	const [user, setUser] = useState(setUserInitial());

	const fetchUserData = useCallback(async () => {
		await fetch("/api/v1/auth/data", {
			headers: {
				"Content-Type": "application/json",
				Authorization: `Bearer ${authToken}`,
			},
		}).then(async (response) => {
			const json = await response.json();
			setUser(() => json.data);
		});
	}, [authToken]);

	useEffect(() => {
		const localVal = localStorage.getItem("auth_token");
		const sessionVal = sessionStorage.getItem("auth_token");

		if (localVal != null) {
			setAuthToken(() => localVal);
		} else if (sessionVal != null) {
			setAuthToken(() => sessionVal);
		}
	}, []);

	useEffect(() => {
		fetchUserData();
	}, [authToken, fetchUserData]);

	// call this function when you want to authenticate the user
	const login = useCallback(
		async (token: string, rememberme: boolean): Promise<void> => {
			try {
				if (rememberme) {
					localStorage.setItem("auth_token", token);
				} else {
					sessionStorage.setItem("auth_token", token);
				}
			} catch (err) {
				console.log(err);
				return;
			}

			setAuthToken(() => token);
			navigate("/", { replace: false });
		},
		[navigate]
	);

	// call this function to sign out logged in user
	const logout = useCallback((): void => {
		localStorage.removeItem("auth_token");
		sessionStorage.removeItem("auth_token");
		setAuthToken(() => "");
		setUser(() => null);
		navigate("/", { replace: true });
	}, []);

	const value = useMemo(
		() => ({
			user,
			fetchUserData,
			authToken,
			login,
			logout,
		}),
		[user, fetchUserData, authToken, login, logout]
	);

	return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
	const context = useContext(AuthContext);
	if (!context) {
		throw new Error('"useAuth" must be used within an AuthProvider component.');
	}
	return context;
};
