import {
	createContext,
	useContext,
	useMemo,
	ReactNode,
	useCallback,
	useEffect,
} from "react";
import { useLocalStorage } from "./useLocalStorage";

interface PreferenceContextType {
	preferences: Preference | null;
	changeTheme: () => void;
}

interface Preference {
	theme: string;
}

const PreferenceContext = createContext<PreferenceContextType | null>(null);

interface Props {
	children: ReactNode;
}

export const PreferenceProvider = ({ children }: Props) => {
	const [preferences, setPreferences] = useLocalStorage("preferences", null);

	useEffect(() => {
		const root = document.querySelector("#body");

		root?.classList.add(preferences?.theme);
	}, [preferences?.theme]);

	// call this function when you want to authenticate the user
	const changeTheme = useCallback(() => {
		console.log("theme toggle gets called");
		const root = document.querySelector("#body");
		root?.classList.toggle("dark");
		if (root?.classList.contains("dark")) {
			setPreferences({ ...preferences, theme: "dark" });
		} else {
			setPreferences({ ...preferences, theme: "light" });
		}
	}, [preferences, setPreferences]);

	const value = useMemo(
		() => ({
			preferences,
			changeTheme,
		}),
		[changeTheme, preferences]
	);

	return (
		<PreferenceContext.Provider value={value}>
			{children}
		</PreferenceContext.Provider>
	);
};

export const usePreferences = () => {
	const context = useContext(PreferenceContext);
	if (!context) {
		throw new Error("usePreferences must be used within an PreferenceContext");
	}
	return context;
};
