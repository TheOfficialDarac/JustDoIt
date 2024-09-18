import { User as NextUser } from "@nextui-org/react";
import { ReactNode, useEffect, useState, useCallback } from "react";

interface Props {
  project: Project;
}

interface Project {
  id: string;
  title: string;
  adminId: string;
  pictureUrl: string;
  description: string;
}

interface UserInProject {
  userId: string;
  projectId: number;
  isVerified: boolean;
  token: string;
  projectRole: string;
}

interface User {
  userName: string;
  email: string;
  firstName: string;
  lastName: string;
  pictureUrl: string;
  id: string;
}

interface ProjectUser extends User {
  projectId: number;
  projectRole: string;
}

export const ProjectMembers = ({ project }: Props) => {
  // const [usersInProject, setUsersInProject] = useState<UserInProject[]>([]);
  // const [users, setUsers] = useState<User[]>([]);
  const [displayMembers, setDisplayMembers] = useState<ReactNode>(null);

  const fetchProjectData = useCallback(async () => {
    const membersFetched = await getProjectMembers();
    const usersFetched = await getUserInfo();
    if (membersFetched && usersFetched) {
      bindMembersAndUsers(membersFetched, usersFetched);
    }
  }, [project?.id]);

  useEffect(() => {
    if (project?.id) {
      fetchProjectData();
    }
  }, [project?.id, fetchProjectData]);

  const bindMembersAndUsers = (
    usersInProject: UserInProject[],
    users: User[]
  ) => {
    const result = usersInProject.map((up) => {
      const user = users.find((user) => user.id === up.userId);
      if (user) {
        return {
          ...user,
          projectId: up.projectId,
          projectRole: up.projectRole,
        };
      }
      return null;
    });
    // .filter((usr): usr is ProjectUser => usr !== null);

    setDisplayMembers(
      <>
        {result.map((usr, index) => (
          <div key={index}>
            <NextUser
              description={`${usr.firstName} ${usr.lastName}`}
              name={usr.projectRole}
              avatarProps={{
                src: usr?.pictureUrl,
              }}
              className="w-[200px]"
            />
          </div>
        ))}
      </>
    );
  };

  const getUserInfo = async (): Promise<User[]> => {
    try {
      const response = await fetch(
        `/api/Project/users?${new URLSearchParams({
          projectID: project?.id,
        }).toString()}`,
        { method: "GET", headers: { "Content-Type": "application/json" } }
      );
      if (response.ok) {
        return await response.json();
      }
      console.error("Failed to fetch users");
    } catch (error) {
      console.error("Error fetching users:", error);
    }
    return [];
  };

  const getProjectMembers = async (): Promise<UserInProject[]> => {
    try {
      const response = await fetch(
        `/api/user/user_projects?${new URLSearchParams({
          projectID: project?.id,
        }).toString()}`,
        { method: "GET", headers: { "Content-Type": "application/json" } }
      );
      if (response.ok) {
        return await response.json();
      }
      console.error("Failed to fetch project members");
    } catch (error) {
      console.error("Error fetching project members:", error);
    }
    return [];
  };

  return <div className="grid grid-cols-5 gap-4">{displayMembers}</div>;
};
