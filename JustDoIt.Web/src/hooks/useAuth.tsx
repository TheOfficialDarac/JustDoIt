import { createContext, useContext, useMemo, ReactNode } from "react";
import { useNavigate } from "react-router-dom";
// import { useLocalStorage } from "./useLocalStorage";
import { useSessionStorage } from "./useSessionStorage";

interface AuthContextType {
  user: object | null;
  login: (data: string) => Promise<void>;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType | null>(null);

interface Props {
  children: ReactNode;
}

interface User {
  username: string;
  email: string;
  password: string;
  firstName: string;
  lastNAme: string;
  phone: string;
  pictureUrl: string;
}

export const AuthProvider = ({ children }: Props) => {
  const [user, setUser] = useSessionStorage("user", null);
  const navigate = useNavigate();

  const getUserObject = async (email: string): Promise<User> => {
    const url = new URL("https://localhost:7010/api/user/all");
    url.searchParams.set("email", email);
    await fetch(
      // "/api/user/all" + "?" + new URLSearchParams({ email: email }).toString()
      url
    )
      .then(async (response) => {
        // handle success or error from the server
        console.log(response);
        if (response.ok) {
          return response.json().then((data) => {
            console.log(data);
            return data;
          });
        } else return null;
      })
      .catch((error) => {
        console.log(error);
      });
  };

  // call this function when you want to authenticate the user
  const login = async (email: string) => {
    const res: User | null = await getUserObject(email);
    if (res) {
      setUser(res);
      navigate("/profile");
    }
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
