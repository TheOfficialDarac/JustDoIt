import { createContext, useContext, useMemo, ReactNode } from "react";
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

  // call this function when you want to authenticate the user
  const changeTheme = () => {
    console.log("theme toggle gets called");
    const root = document.querySelector("#app");
    root?.classList.toggle("dark");
    if (root?.classList.contains("dark")) {
      setPreferences({ ...preferences, theme: "dark" });
    } else {
      setPreferences({ ...preferences, theme: "light" });
    }
  };

  //   useEffect(() => {}, [preferences]);

  const value = useMemo(
    () => ({
      preferences,
      changeTheme,
    }),
    [preferences]
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
