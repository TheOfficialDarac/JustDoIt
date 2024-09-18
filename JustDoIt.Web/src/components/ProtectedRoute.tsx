import { useNavigate } from "react-router-dom";
import { useAuth } from "../hooks/useAuth";
import { useEffect } from "react";

export const ProtectedRoute = ({ children }) => {
	const navigate = useNavigate();
	const { authToken } = useAuth();
	useEffect(() => {
		console.log(authToken);
		if (!authToken) {
			navigate("/");
			return;
		}
	}, [authToken, navigate]);
	// console.log("AUTH: ", authToken);
	return <>{children}</>;
};
