import { Link } from "react-router-dom";
import { ProjectResponse, ProjectRoleResponse } from "../../types/Types";
import ProjectMembers from "./ProjectMembers";
import { useEffect, useState } from "react";

interface Props {
  project: ProjectResponse;
  authToken: string;
}

export default function SelectedProject({
  project,
  authToken,
}: Readonly<Props>) {
  const [userRole, setUserRole] = useState<ProjectRoleResponse>(null);

  useEffect(() => {
    const getRole = async () => {
      await fetch(`/api/v1/projects/user-role?ProjectId=${project.id}`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${authToken}`,
        },
      })
        .then(async (response) => {
          if (response.ok) {
            const json = await response.json();
            if (json.result.isSuccess) {
              setUserRole(() => json.data);
            } else {
              console.warn("user has no role");
            }
          }
        })
        .catch((error) => console.error(error));
    };
    getRole();
  }, [authToken, project.id]);

  return (
    <div className="p-3 border w-full flex flex-col">
      <div className="border">
        <h3>{project?.title}</h3>
        <p>{project?.description}</p>
      </div>
      <div>
        <Link to={`/tasks/${project?.id}`}>See tasks?</Link>
      </div>
      <div>
        {userRole?.roleName == "ADMIN" && <h3>I CAN DO ANYTHING</h3>}
        can edit members if admin & send invite
        <ProjectMembers projectId={project?.id} />
      </div>
    </div>
  );
}
