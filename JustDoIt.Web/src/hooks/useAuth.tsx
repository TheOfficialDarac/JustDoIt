import { createContext, useContext, useMemo, ReactNode } from "react";
import { useNavigate } from "react-router-dom";
// import { useLocalStorage } from "./useLocalStorage";
import { useSessionStorage } from "./useSessionStorage";

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
  const [user, setUser] = useSessionStorage("user", null);
  const navigate = useNavigate();

  const getUserObject = async (email: string): Promise<boolean> => {
    await fetch(
      "/api/user/all" + "?" + new URLSearchParams({ email: email }).toString(),
      {
        method: "GET",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
        },
      }
    )
      .then(async (response) => {
        // handle success or error from the server
        if (response.ok) {
          return response.json().then((data) => {
            setUser(data[0]);
            return true;
          });
        } else return null;
      })
      .catch((error) => {
        console.log(error);
      });
    return false;
  };

  // call this function when you want to authenticate the user
  const login = async (email: string) => {
    const res = await getUserObject(JSON.parse(email).email);
    if (res) {
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
