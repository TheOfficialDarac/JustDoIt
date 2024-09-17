import { createContext, useContext, useMemo, ReactNode, useState } from "react";
import { useNavigate } from "react-router-dom";
// import { useLocalStorage } from "./useLocalStorage";
import { useSessionStorage } from "./useSessionStorage";
import { useLocalStorage } from "./useLocalStorage";

interface AuthContextType {
  user: User | null;
  login: (data: string) => Promise<void>;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType | null>(null);

interface Props {
  children: ReactNode;
}

interface User {
  userName: string;
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  pictureUrl: string;
  id: string;
}

export const AuthProvider = ({ children }: Props) => {
  const [user, setUser] = useState(null);
  // const [user, setUser] = useSessionStorage("user", null);
  const navigate = useNavigate();

  // call this function when you want to authenticate the user
  const login = async (token: string, rememberme: boolean) => {
    console.log("token: ", token);
  };

  // call this function to sign out logged in user
  const logout = () => {
    setUser(null);
    navigate("/", { replace: true });
  };

  const value = useMemo(
    () => ({
      user,
      login,
      logout,
    }),
    [user]
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};
