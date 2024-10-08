import { useState } from "react";
import {
	Navbar,
	NavbarBrand,
	NavbarContent,
	NavbarItem,
	NavbarMenuToggle,
	NavbarMenu,
	NavbarMenuItem,
	Button,
} from "@nextui-org/react";
import DummyLogo from "../../assets/icons/AppLogo.tsx";
import UserIcon from "./UserIcon.tsx";
import { Link } from "react-router-dom";
import { User } from "../../helpers/Types.tsx";

interface Props {
	user: User;
	authToken: string;
	logout: () => void;
}
function Header({ user, authToken, logout }: Props) {
	const [isMenuOpen, setIsMenuOpen] = useState<boolean>(false);

	const menuItems = ["settings", "projects", "shutdown", "Log Out"];

	return (
		<Navbar
			onMenuOpenChange={setIsMenuOpen}
			position='sticky'
			isBordered
			shouldHideOnScroll={false}
		>
			<NavbarContent>
				<NavbarMenuToggle
					aria-label={isMenuOpen ? "Close menu" : "Open menu"}
					className='sm:hidden'
				/>
				<NavbarBrand>
					<Link
						to='/'
						color='foreground'
						className='flex items-center'
					>
						<DummyLogo />
						<p className='font-bold text-inherit'>Just Do It</p>
					</Link>
				</NavbarBrand>
			</NavbarContent>

			<NavbarContent
				className='hidden sm:flex gap-4'
				justify='center'
			>
				{authToken && (
					<NavbarItem>
						<Link
							color='foreground'
							to='/projects'
						>
							Projects
						</Link>
					</NavbarItem>
				)}
				<NavbarItem>
					<Link
						color='foreground'
						to='/'
					>
						Home
					</Link>
				</NavbarItem>
			</NavbarContent>
			<NavbarContent
				as='div'
				className='items-center'
				justify='end'
			>
				{/* <Input
						classNames={{
							base: "max-w-full sm:max-w-[10rem] h-10",
							mainWrapper: "h-full",
							input: "text-small",
							inputWrapper:
								"h-full font-normal text-default-500 bg-default-400/20 dark:bg-default-500/20",
						}}
						placeholder='Type to search...'
						size='sm'
						startContent={<MagnifyingGlas />}
						type='search'
					/> */}
				{
					<UserIcon
						user={user}
						authToken={authToken}
						logout={logout}
					/>
				}
			</NavbarContent>

			<NavbarMenu>
				{menuItems.map((item, index) => (
					<NavbarMenuItem key={`${item}-${index}`}>
						{item == "Log Out" ? (
							// <Navigate />
							<Button onClick={logout}>{item}</Button>
						) : (
							<Link
								className={
									"w-full" +
									// color={
									(index === 2
										? "primary"
										: index === menuItems.length - 1
										? "danger"
										: "foreground")
								}
								// className='w-full'
								to={"/" + item}
								// size='lg'
							>
								{item.normalize()}
							</Link>
						)}
					</NavbarMenuItem>
				))}
			</NavbarMenu>
		</Navbar>
	);
}

export default Header;
