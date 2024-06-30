import { useAuth } from "../../hooks/useAuth";
import { useNavigate } from "react-router-dom";

const Logout = () => {
  const { logout } = useAuth();

  const navigate = useNavigate();
  const handleLogout = () => {
    logout();
    navigate("/");
  };

  return (
    <>
      <button onClick={handleLogout}>LOG OUT</button>
    </>
  );
};

export default Logout;
