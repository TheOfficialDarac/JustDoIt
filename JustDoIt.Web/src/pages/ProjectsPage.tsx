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
} from "@nextui-org/react";
import { ProjectMembers } from "../components/ProjectMembers";
import { useNavigate } from "react-router-dom";

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

  let navigate = useNavigate();

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
            // console.log(data);
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
        <>
          <div
            className="box-border flex gap-4 flex-col min-w-[300px] p-5 max-sm:max-w-full"
            key="projects"
          >
            <div className="p-5 flex">
              <Button
                type="button"
                onClick={() => {
                  navigate("/projects/create");
                }}
                className="flex-1"
              >
                Create Project
              </Button>
            </div>
            {projects.map((project, index) => (
              <Card
                isPressable
                onPress={() => setSelected(project)}
                key={index}
                className="box-border py-4 rounded-lg hover:border-gray-300 hover:border-[2px] border-[2px] border-transparent"
                // style={{
                //   borderWidth: "2px",
                //   borderStyle: "solid",
                //   borderColor: "transparent",
                // }} // Add default border to maintain size
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
            ))}
          </div>
        </>
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
                <div className="flex p-2 m-2 gap-1 flex-col">
                  <Button
                    type="button"
                    onClick={() => {
                      navigate("/project/members/add/" + selected?.id);
                    }}
                  >
                    Members
                  </Button>

                  <Button
                    type="button"
                    onClick={() => {
                      navigate("/tasks/" + selected?.id);
                    }}
                    className="ml-auto"
                  >
                    See Tasks
                  </Button>
                  <Button
                    type="button"
                    onClick={() => {
                      navigate("/project/edit/" + selected?.id);
                    }}
                  >
                    Edit Project
                  </Button>
                </div>
              </CardHeader>
              <Divider />
              <CardBody className="my-3">
                <p>DESCRIPTION: {selected?.description}</p>
              </CardBody>
              <Divider />
              <CardFooter className="justify-start items-start">
                <Accordion fullWidth className="h-100" key="accordion">
                  <AccordionItem
                    key="members"
                    aria-label="Members"
                    title="Members"
                  >
                    <ProjectMembers project={selected} />
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
