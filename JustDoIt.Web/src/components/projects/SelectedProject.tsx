import { Link } from "react-router-dom";
import { ProjectResponse, ProjectRoleResponse } from "../../types/Types";
import { useCallback, useEffect, useState } from "react";
import { Button } from "@nextui-org/react";
import TasksComponent from "../test/TasksComponent";
import { Cog6ToothIcon } from "@heroicons/react/16/solid";

interface Props {
  project: ProjectResponse;
  authToken: string;
}

export default function SelectedProject({
  project,
  authToken,
}: Readonly<Props>) {
  const [userRole, setUserRole] = useState<ProjectRoleResponse>(null);

  const getRole = useCallback(async () => {
    await fetch(`/api/v1/projects/user-role?ProjectId=${project?.id}`, {
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
  }, [authToken, project?.id]);

  useEffect(() => {
    getRole();
  }, [getRole]);

  return (
    <div className="p-3 border w-full flex flex-col">
      <div className="border">
        <h3>{project?.title}</h3>
        <p>{project?.description}</p>
        <button onClick={() => alert("edit me")}>
          <Cog6ToothIcon className="size-6" />
        </button>
      </div>
      <div>
        {/* <Link className="hover:text-cyan-100" to={`/members/${project?.id}`}>
          Members
        </Link> */}
        {/* <ProjectMembers projectId={project?.id} /> */}
      </div>
      <div>my role in project: {userRole?.roleName}</div>
      <div>
        {/* <Link className="hover:text-cyan-100" to={`/tasks/${project?.id}`}>
          Tasks
        </Link> */}

        <TasksComponent
          projectId={project?.id}
          userRole={userRole}
          authToken={authToken}
        />
      </div>
    </div>
  );
}
