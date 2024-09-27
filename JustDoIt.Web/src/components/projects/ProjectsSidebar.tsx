import { Dispatch, memo, SetStateAction, useEffect, useState } from "react";
import { ProjectResponse } from "../../types/Types";
import { Input } from "@nextui-org/react";
import CreateProjectModal from "./CreateProjectModal";

interface Props {
  projects: ProjectResponse[];
  setSelectedProject: Dispatch<SetStateAction<ProjectResponse>>;
  authToken: string;
  fetchProjects: () => void;
}
const ProjectsSidebar = memo(function ({
  projects,
  setSelectedProject,
  authToken,
  fetchProjects,
}: Readonly<Props>) {
  const [filteredProjects, setFilteredProjects] = useState<ProjectResponse[]>(
    []
  );

  useEffect(() => {
    setFilteredProjects(() => projects);
  }, [projects]);

  const handleSearch = (e: {
    target: { value: { toString: () => string } };
  }) => {
    if (e.target.value) {
      const result = filteredProjects.filter((x) => {
        return x?.title
          .toLowerCase()
          .includes(e.target.value.toString().toLowerCase());
      });
      setFilteredProjects(() => result);
    } else setFilteredProjects(() => projects);
  };

  return (
    <div className="flex flex-col justify-items-center text-center border-spacing-8">
      <div className="border p-3">
        <Input
          type="text"
          placeholder="Search"
          className="max-w-xs"
          onChange={handleSearch}
        />
      </div>
      <ul className="flex flex-col gap-2 p-1">
        {filteredProjects?.map((project) => (
          <li className="border" key={project.id}>
            <button
              className="p-2"
              onClick={() => setSelectedProject(() => project)}
            >
              {project.title}
            </button>
          </li>
        ))}
      </ul>
      <div className="p-2">
        <CreateProjectModal
          authToken={authToken}
          fetchProjects={fetchProjects}
        />
      </div>
    </div>
  );
});
export default ProjectsSidebar;
