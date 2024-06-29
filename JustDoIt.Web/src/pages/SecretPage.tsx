import { useAuth } from "../hooks/useAuth";

export const Secret = () => {
  const { logout } = useAuth();

  const handleLogout = () => {
    logout();
  };

  return (
    <div>
      <h1>This is a Secret page</h1>
      <button type="button" onClick={handleLogout}>
        Logout
      </button>
    </div>
  );
};
