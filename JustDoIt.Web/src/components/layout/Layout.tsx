import { User } from "../../helpers/Types";
import Footer from "./Footer";
import Header from "./Header";

interface Props {
	children: React.ReactNode;
	user: User;
	authToken: string;
	logout: () => void;
}

function Layout({ children, user, authToken, logout }: Props) {
	return (
		<>
			<Header
				user={user}
				authToken={authToken}
				logout={logout}
			/>
			<main className='max-w-screen-xl w-full flex-1 m-2 p-3'>{children}</main>
			<Footer />
		</>
	);
}

export default Layout;
