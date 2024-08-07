import {
  Avatar,
  Dropdown,
  DropdownItem,
  DropdownMenu,
  DropdownSection,
  DropdownTrigger,
} from "@nextui-org/react";
import { useNavigate } from "react-router-dom";
import Logout from "./Logout";
import { useAuth } from "../../hooks/useAuth";

export default function UserIcon() {
  const navigate = useNavigate();
  const { user } = useAuth();
  // console.log(user);

  return (
    <>
      {!user ? (
        <Dropdown placement="bottom-end">
          <DropdownTrigger>
            <Avatar
              isBordered
              as="button"
              className="transition-transform"
              size="sm"
              icon={
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  fill="none"
                  viewBox="0 0 24 24"
                  strokeWidth="1.5"
                  stroke="currentColor"
                  className="size-6"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    d="M9.879 7.519c1.171-1.025 3.071-1.025 4.242 0 1.172 1.025 1.172 2.687 0 3.712-.203.179-.43.326-.67.442-.745.361-1.45.999-1.45 1.827v.75M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Zm-9 5.25h.008v.008H12v-.008Z"
                  />
                </svg>
              }
            />
          </DropdownTrigger>
          <DropdownMenu
            aria-label="Profile Actions"
            variant="flat"
            onAction={(key) => {
              navigate(`/${key}`);
            }}
          >
            <DropdownItem key="profile" className="h-8 gap-2">
              <p className="font-semibold">You're not logged in</p>
            </DropdownItem>
            <DropdownItem key="login">Login</DropdownItem>
            <DropdownItem key="register">Register</DropdownItem>
          </DropdownMenu>
        </Dropdown>
      ) : (
        <Dropdown placement="bottom-end">
          <DropdownTrigger>
            <Avatar
              isBordered
              as="button"
              className="transition-transform"
              color="secondary"
              size="sm"
              src={user?.pictureUrl}
            />
          </DropdownTrigger>
          <DropdownMenu
            aria-label="Profile Actions"
            variant="flat"
            onAction={(key) => {
              if (key !== "logout") navigate(`/${key}`);
            }}
          >
            <DropdownSection showDivider>
              <DropdownItem key="profile" className="h-14 gap-2">
                <p className="font-semibold">Signed in as</p>
                <p className="font-semibold">{user.userName}</p>
              </DropdownItem>
              <DropdownItem key="projects">My Projects</DropdownItem>
              <DropdownItem key="settings">My Settings</DropdownItem>
            </DropdownSection>
            <DropdownSection>
              <DropdownItem key="logout" color="danger">
                <Logout />
              </DropdownItem>
            </DropdownSection>
          </DropdownMenu>
        </Dropdown>
      )}
    </>
  );
}
