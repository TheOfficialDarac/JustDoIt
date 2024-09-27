import { useCallback, useEffect, useState } from "react";
import ProjectsSidebar from "../../components/projects/ProjectsSidebar";
import SelectedProject from "../../components/projects/SelectedProject";
import { ProjectResponse, UserResponse } from "../../types/Types";
import LoadingSpinner from "../../components/layout/LoadingSpinner";
import { useDisclosure } from "@nextui-org/react";

interface Props {
  user: UserResponse;
  authToken: string;
}
function ProjectsPage({ user, authToken }: Props) {
  const [projects, setProjects] = useState<ProjectResponse[]>([]);

  const [selectedProject, setSelectedProject] = useState<ProjectResponse>(null);
  const { onOpen, isOpen, onClose } = useDisclosure();

  const fetchProjects = useCallback(() => {
    onOpen();
    fetch("/api/v1/projects/user", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${authToken}`,
      },
    })
      .then(async (response) => {
        if (response.ok) {
          const json = await response.json();
          setProjects(() => json.data);
        }
      })
      .catch((error) => {
        console.error(error);
      })
      .finally(onClose);
  }, [authToken, onClose, onOpen]);

  useEffect(() => {
    fetchProjects();
  }, [fetchProjects]);

  useEffect(() => {
    setSelectedProject(() => projects[0]);
  }, [projects]);

  return (
    <>
      {/* <div className="border m-2 p-2 ">{user?.userName}</div> */}
      <div className="flex gap-3 border p-2 items-stretch">
        <ProjectsSidebar
          projects={projects}
          setSelectedProject={setSelectedProject}
          authToken={authToken}
          fetchProjects={fetchProjects}
        />
        {projects.length > 0 ? (
          <SelectedProject
            project={selectedProject}
            authToken={authToken}
            fetchProjects={fetchProjects}
          />
        ) : (
          <div>Create a project to get started.</div>
        )}
        <LoadingSpinner isOpen={isOpen} onClose={onClose} onOpen={onOpen} />
      </div>
    </>
  );
}

export default ProjectsPage;
