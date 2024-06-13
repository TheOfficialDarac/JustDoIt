import { useState } from "react";
import {
  Avatar,
  Button,
  Dropdown,
  DropdownItem,
  DropdownMenu,
  DropdownTrigger,
  Input,
  Link,
  Navbar,
  NavbarBrand,
  NavbarContent,
  NavbarItem,
  NavbarMenuToggle,
  NavbarMenu,
  NavbarMenuItem,
} from "@nextui-org/react";
import DummyLogo from "../assets/DummyLogo";
import LoginModal from "./LoginModal";

interface Props {
  name: string;
}

function Header({ name }: Props) {
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  const menuItems = [
    "Profile",
    "Dashboard",
    "Activity",
    "Analytics",
    "System",
    "Deployments",
    "My Settings",
    "Team Settings",
    "Help & Feedback",
    "Log Out",
  ];

  return (
    <>
      <Navbar
        onMenuOpenChange={setIsMenuOpen}
        position="sticky"
        isBordered
        shouldHideOnScroll={false}
      >
        <NavbarContent>
          <NavbarMenuToggle
            aria-label={isMenuOpen ? "Close menu" : "Open menu"}
            className="sm:hidden"
          />
          <NavbarBrand>
            <DummyLogo />
            <p className="font-bold text-inherit">Just Do It</p>
          </NavbarBrand>
        </NavbarContent>

        <NavbarContent className="hidden sm:flex gap-4" justify="center">
          <NavbarItem>
            <Link color="foreground" href="/">
              Features
            </Link>
          </NavbarItem>
          <NavbarItem>
            <Link color="foreground" href="/">
              Customers
            </Link>
          </NavbarItem>
          <NavbarItem>
            <Link color="foreground" href="/">
              {name}
            </Link>
          </NavbarItem>
        </NavbarContent>
        <NavbarContent as="div" className="items-center" justify="end">
          <Input
            classNames={{
              base: "max-w-full sm:max-w-[10rem] h-10",
              mainWrapper: "h-full",
              input: "text-small",
              inputWrapper:
                "h-full font-normal text-default-500 bg-default-400/20 dark:bg-default-500/20",
            }}
            placeholder="Type to search..."
            size="sm"
            startContent={
              <svg
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                strokeWidth={1.5}
                stroke="currentColor"
                className="size-6"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  d="m21 21-5.197-5.197m0 0A7.5 7.5 0 1 0 5.196 5.196a7.5 7.5 0 0 0 10.607 10.607Z"
                />
              </svg>
            }
            type="search"
          />
          {/* is user signed up checker */}
          {true ? (
            <>
              <NavbarItem className="hidden lg:flex">
                {/* <Link href={}>Login</Link> */}
                <LoginModal />
              </NavbarItem>
              <NavbarItem>
                <Button
                  as={Link}
                  color="primary"
                  href="/register"
                  variant="flat"
                >
                  Sign Up
                </Button>
              </NavbarItem>
            </>
          ) : (
            <Dropdown placement="bottom-end">
              <DropdownTrigger>
                <Avatar
                  isBordered
                  as="button"
                  className="transition-transform"
                  color="secondary"
                  name="Jason Hughes"
                  size="sm"
                  src="https://i.pravatar.cc/150?u=a042581f4e29026704d"
                />
              </DropdownTrigger>
              <DropdownMenu aria-label="Profile Actions" variant="flat">
                <DropdownItem key="profile" className="h-14 gap-2">
                  <p className="font-semibold">Signed in as</p>
                  <p className="font-semibold">zoey@example.com</p>
                </DropdownItem>
                <DropdownItem key="settings">My Settings</DropdownItem>
                <DropdownItem key="team_settings">Team Settings</DropdownItem>
                <DropdownItem key="analytics">Analytics</DropdownItem>
                <DropdownItem key="system">System</DropdownItem>
                <DropdownItem key="configurations">Configurations</DropdownItem>
                <DropdownItem key="help_and_feedback">
                  Help & Feedback
                </DropdownItem>
                <DropdownItem key="logout" color="danger">
                  Log Out
                </DropdownItem>
              </DropdownMenu>
            </Dropdown>
          )}
        </NavbarContent>

        <NavbarMenu>
          {menuItems.map((item, index) => (
            <NavbarMenuItem key={`${item}-${index}`}>
              <Link
                color={
                  index === 2
                    ? "primary"
                    : index === menuItems.length - 1
                    ? "danger"
                    : "foreground"
                }
                className="w-full"
                href="#"
                size="lg"
              >
                {item}
              </Link>
            </NavbarMenuItem>
          ))}
        </NavbarMenu>
      </Navbar>
    </>
  );
}

export default Header;
