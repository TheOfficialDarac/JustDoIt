import { ReactNode, useEffect, useState } from "react";
import { useAuth } from "../../hooks/useAuth";
import { Card, CardBody, CardHeader, Image } from "@nextui-org/react";

interface UserInProject {
  userId: string;
  projectId: number;
  isVerified: boolean;
  token: string;
  projectRole: string;
}
interface Project {
  id: string;
  title: string;
  adminId: string;
  pictureUrl: string;
  description: string;
}

const ProjectsSidebar = () => {
  const { user } = useAuth();
  const [projects, setProjects] = useState<Project[] | null>(null);
  const [display, setDisplay] = useState<ReactNode>(null);

  useEffect(() => {
    const getProjects = async () => {
      if (user) {
        try {
          const response = await fetch(
            "/api/user/projects?" +
              new URLSearchParams({ userId: user.id }).toString(),
            {
              method: "GET",
              mode: "cors",
              headers: {
                "Content-Type": "application/json",
              },
            }
          );

          if (response.ok) {
            const data = await response.json();
            console.log(data);
            setProjects(data);
          } else {
            console.error("Failed to fetch projects");
          }
        } catch (error) {
          console.error("Error fetching projects:", error);
        }
      }
    };

    getProjects();
  }, [user]);

  useEffect(() => {
    if (projects) {
      setDisplay(
        <div className="flex gap-4 flex-col max-w-[300px] p-5 box-border">
          {projects.map((project, index) => (
            <>
              <Card
                className="py-4 rounded-lg box-border hover:border-2 hover:border-grey-300"
                key={index}
              >
                <CardHeader className="pb-0 pt-2 px-4 flex-col items-start">
                  <p className="text-tiny uppercase font-bold">Tag? myRole?</p>
                  <h4 className="font-bold text-large">{project?.title}</h4>
                  <small className="text-default-500">
                    {project?.description}
                  </small>
                </CardHeader>
                <CardBody className="overflow-visible py-2">
                  <Image
                    alt="Card background"
                    className="object-cover rounded-xl"
                    src={
                      project?.pictureUrl ||
                      "https://nextui.org/images/hero-card-complete.jpeg"
                    }
                    width={270}
                  />
                </CardBody>
              </Card>
            </>
          ))}
        </div>
      );
    }
  }, [projects]);

  return <>{display}</>;
};

export default ProjectsSidebar;
