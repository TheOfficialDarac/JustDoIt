import { Button } from "@nextui-org/react";
import { useAuth } from "../hooks/useAuth";

export const SecretPage = () => {
	const { logout } = useAuth();

	return (
		<div>
			<h1>This is a Secret page</h1>
			<Button
				type='button'
				onPress={logout}
			>
				Logout
			</Button>
		</div>
	);
};
