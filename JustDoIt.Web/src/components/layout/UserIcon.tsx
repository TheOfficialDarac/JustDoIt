import {
	Avatar,
	Dropdown,
	DropdownItem,
	DropdownMenu,
	DropdownSection,
	DropdownTrigger,
} from "@nextui-org/react";
import { useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { User } from "../../helpers/Types";

interface Props {
	user: User;
	authToken: string;
	logout: () => void;
}

export default function UserIcon({ user, logout, authToken }: Props) {
	const navigate = useNavigate();
	const [image, setImage] = useState<string>("");

	useEffect(() => {
		const getPicture = async () => {
			if (user?.pictureUrl) {
				const url: string = `/api/v1/attachments/get-file?filepath=${user?.pictureUrl}`;
				fetch(url, {
					method: "GET",
				}).then(async (response) => {
					response.blob().then((blob) => {
						const file = new File([blob], `${user?.id}.jpg`, {
							type: blob.type,
						});
						setImage(() => URL.createObjectURL(file));
					});
				});
			}
		};
		getPicture();
	}, [user]);

	return (
		<>
			{!authToken ? (
				<Dropdown placement='bottom-end'>
					<DropdownTrigger>
						<Avatar
							isBordered
							as='button'
							className='transition-transform'
							size='sm'
							icon={
								<svg
									xmlns='http://www.w3.org/2000/svg'
									fill='none'
									viewBox='0 0 24 24'
									strokeWidth='1.5'
									stroke='currentColor'
									className='size-6'
								>
									<path
										strokeLinecap='round'
										strokeLinejoin='round'
										d='M9.879 7.519c1.171-1.025 3.071-1.025 4.242 0 1.172 1.025 1.172 2.687 0 3.712-.203.179-.43.326-.67.442-.745.361-1.45.999-1.45 1.827v.75M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Zm-9 5.25h.008v.008H12v-.008Z'
									/>
								</svg>
							}
						/>
					</DropdownTrigger>
					<DropdownMenu
						aria-label='Profile Actions'
						variant='flat'
						onAction={(key) => {
							navigate(`/${key}`);
						}}
					>
						<DropdownItem
							key='profile'
							className='h-8 gap-2'
						>
							<p className='font-semibold'>You're not logged in</p>
						</DropdownItem>
						<DropdownItem key='login'>Login</DropdownItem>
						<DropdownItem key='register'>Register</DropdownItem>
					</DropdownMenu>
				</Dropdown>
			) : (
				<Dropdown placement='bottom-end'>
					<DropdownTrigger>
						<Avatar
							showFallback
							isBordered
							as='button'
							className='transition-transform'
							color='secondary'
							size='sm'
							src={image}
						/>
					</DropdownTrigger>
					<DropdownMenu
						aria-label='Profile Actions'
						variant='flat'
						onAction={(key) => {
							if (key !== "logout") navigate(`/${key}`, { replace: false });
							else logout();
						}}
					>
						<DropdownSection showDivider>
							<DropdownItem
								key='profile-info'
								className='h-14 gap-2'
							>
								<p className='font-semibold'>Signed in as</p>
								<p className='font-semibold'>{user?.userName}</p>
							</DropdownItem>
							{/* <DropdownItem key='projects'>My Projects</DropdownItem> */}
							{/* <DropdownItem key='settings'>My Settings</DropdownItem> */}
							<DropdownItem key='profile'>Profile</DropdownItem>
							<DropdownItem key='display-settings'>
								Display Settings
							</DropdownItem>
						</DropdownSection>
						<DropdownSection>
							<DropdownItem
								key='logout'
								color='danger'
							>
								Log out
							</DropdownItem>
						</DropdownSection>
					</DropdownMenu>
				</Dropdown>
			)}
		</>
	);
}
