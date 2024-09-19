import { Listbox, ListboxItem } from "@nextui-org/react";
// import AppLogo from "../../assets/icons/AppLogo";
import { TwitterLogo } from "../../assets/icons/TwitterLogo";
import { InstagramLogo } from "../../assets/icons/InstagramLogo";
import FacebookLogo from "../../assets/icons/FacebookLogo";
import GithubLogo from "../../assets/icons/GithubLogo";
import RedditLogo from "../../assets/icons/RedditLogo";

const Footer = () => {
	const iconStyle =
		"text-base w-8 h-8 object-cover inline-block cursor-pointer";
	return (
		<>
			<footer className='max-w-screen-xl w-full mt-4 pt-4'>
				<div className='flex gap-10'>
					<div className='w-full px-1 py-2 text-center'>
						<Listbox
							aria-label='Listbox Variants'
							variant={"light"}
						>
							<ListboxItem key='services'>
								<strong>Services</strong>
							</ListboxItem>
							<ListboxItem key='branding'>Branding</ListboxItem>
						</Listbox>
					</div>
					<div className='w-full px-1 py-2 text-center'>
						<Listbox
							aria-label='Listbox Variants'
							variant={"light"}
						>
							<ListboxItem key='support'>
								<strong>Support</strong>
							</ListboxItem>
							<ListboxItem key='pricing'>Pricing</ListboxItem>
							<ListboxItem key='service_status'>Service status</ListboxItem>
						</Listbox>
					</div>
					<div className='w-full px-1 py-2 text-center'>
						<Listbox
							aria-label='Listbox Variants'
							variant={"light"}
						>
							<ListboxItem key='about'>
								<strong>About</strong>
							</ListboxItem>
							<ListboxItem key='story'>Our story</ListboxItem>
							<ListboxItem key='news'>Latest News</ListboxItem>
							{/* <ListboxItem key='terms'>Terms</ListboxItem> */}
						</Listbox>
					</div>
				</div>
				{/* <div className='px-4 py-1 mb-2 flex flex-row items-center justify-center'>
					<AppLogo /> <strong>Just Do It</strong>
				</div> */}
				<div className='px-4 py-1 mb-2 flex flex-row justify-around mx-16'>
					<TwitterLogo className={iconStyle} />
					<InstagramLogo className={iconStyle} />
					<FacebookLogo className={iconStyle} />
					<GithubLogo className={iconStyle} />
					<RedditLogo className={iconStyle} />
				</div>
			</footer>
		</>
	);
};

export default Footer;
