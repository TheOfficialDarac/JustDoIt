import { ReactNode, useEffect, useState } from "react";
import { useAuth } from "../hooks/useAuth";
import {
  Accordion,
  AccordionItem,
  Button,
  Card,
  CardBody,
  CardFooter,
  CardHeader,
  Divider,
  Image,
  User,
} from "@nextui-org/react";

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

const ProjectsPage = () => {
  const { user } = useAuth();
  const [projects, setProjects] = useState<Project[] | null>(null);
  const [display, setDisplay] = useState<ReactNode>(null);

  const [selected, setSelected] = useState<Project | null>(null);

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

  const setSelection = (project: Project) => {
    setSelected(project);
  };

  useEffect(() => {
    if (projects) {
      setDisplay(
        <div className="box-border flex gap-4 flex-col min-w-[300px] p-5 max-sm:max-w-full">
          {projects.map((project, index) => (
            <>
              <Card
                isPressable
                onPress={() => setSelection(project)}
                key={index}
                className="box-border py-4 rounded-lg hover:border-2 hover:border-grey-300"
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

  return (
    <>
      <div className="box-border flex flex-row max-sm:flex-col">
        {display}
        {selected ? (
          <div className="flex-1 m-3 p-2 box-border flex flex-col">
            <Card className="w-100 p-6" key="selected">
              <CardHeader className="flex gap-3">
                <Image
                  alt="nextui logo"
                  height={40}
                  radius="sm"
                  src="https://avatars.githubusercontent.com/u/86160567?s=200&v=4"
                  width={40}
                />
                <div className="flex flex-col">
                  <p className="text-md">TITLE: {selected?.title}</p>
                  <p className="text-small text-default-500">MY ROLE: admin</p>
                </div>
              </CardHeader>
              <Divider />
              <CardBody className="my-3">
                <p>DESCRIPTION: {selected?.description}</p>
              </CardBody>
              <Divider />
              <CardFooter className="justify-start items-start">
                <Accordion fullWidth className="h-100">
                  <AccordionItem
                    key="members"
                    aria-label="Members"
                    title="Members"
                  >
                    <div className="grid-cols-5 gap-4">
                      <User
                        name="Jane Doe"
                        description="TITLE"
                        avatarProps={{
                          src: "https://i.pravatar.cc/150?u=a04258114e29026702d",
                        }}
                        className="w-[200px]"
                      />
                      <User
                        name="Jane Doe"
                        description="TITLE"
                        avatarProps={{
                          src: "https://i.pravatar.cc/150?u=a04258114e29026702d",
                        }}
                        className="w-[200px]"
                      />
                      <User
                        name="Jane Doe"
                        description="TITLE"
                        avatarProps={{
                          src: "https://i.pravatar.cc/150?u=a04258114e29026702d",
                        }}
                        className="w-[200px]"
                      />
                      <User
                        name="Jane Doe"
                        description="TITLE"
                        avatarProps={{
                          src: "https://i.pravatar.cc/150?u=a04258114e29026702d",
                        }}
                        className="w-[200px]"
                      />
                      <User
                        name="Jane Doe"
                        description="TITLE"
                        avatarProps={{
                          src: "https://i.pravatar.cc/150?u=a04258114e29026702d",
                        }}
                        className="w-[200px]"
                      />
                      <User
                        name="Jane Doe"
                        description="TITLE"
                        avatarProps={{
                          src: "https://i.pravatar.cc/150?u=a04258114e29026702d",
                        }}
                        className="w-[200px]"
                      />
                      <User
                        name="Jane Doe"
                        description="TITLE"
                        avatarProps={{
                          src: "https://i.pravatar.cc/150?u=a04258114e29026702d",
                        }}
                        className="w-[200px]"
                      />
                    </div>
                  </AccordionItem>
                </Accordion>
              </CardFooter>
            </Card>
          </div>
        ) : null}
      </div>
    </>
  );
};

export default ProjectsPage;
