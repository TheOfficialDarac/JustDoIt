import { useNavigate } from "react-router-dom";
import { useAuth } from "../hooks/useAuth";
import { useEffect } from "react";

interface Props {
	children: React.ReactNode;
}

export const ProtectedRoute = ({ children }: Props) => {
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
