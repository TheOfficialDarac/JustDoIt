import { useAuth } from "../../hooks/useAuth";

const Logout = () => {
  const { logout } = useAuth();

  const handleLogout = () => {
    logout();
  };

  return (
    <>
      <button type="button" onClick={handleLogout}>
        LOG OUT
      </button>
    </>
  );
};

export default Logout;
