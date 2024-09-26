import { useNavigate } from "react-router-dom";
import { useEffect } from "react";

interface Props {
	children: React.ReactNode;
	authToken: string;
}

export const ProtectedRoute = ({ children, authToken }: Props) => {
	const navigate = useNavigate();
	useEffect(() => {
		// console.log(authToken);
		if (!authToken) {
			navigate("/");
			return;
		}
	}, [authToken, navigate]);
	return <>{children}</>;
};
