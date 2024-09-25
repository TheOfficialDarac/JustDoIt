import { Dispatch, memo, SetStateAction, useEffect, useState } from "react";
import { ProjectResponse } from "../../types/Types";
import { Button, Input } from "@nextui-org/react";
import { PlusIcon } from "@heroicons/react/16/solid";
import CreateProjectModal from "./CreateProjectModal";

interface Props {
  projects: ProjectResponse[];
  setSelectedProject: Dispatch<SetStateAction<ProjectResponse>>;
}
const ProjectsSidebar = memo(function ({
  projects,
  setSelectedProject,
}: Readonly<Props>) {
  const [filteredProjects, setFilteredProjects] = useState<ProjectResponse[]>(
    []
  );
  const [isOpenCreateModal, setIsOpenCreateModal] = useState<boolean>(false);

  useEffect(() => {
    console.log("we here again");
    setFilteredProjects(() => projects);
  }, [projects]);

  const handleSearch = (e) => {
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
    <>
      <div className="flex flex-col justify-items-center text-center border-spacing-8">
        <div className="border p-3">
          <Input
            type="text"
            // label="Project title"
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
          <CreateProjectModal />
          {/* <Button
            className="w-full"
            // onPress={onOpen}
            onPress={() => setIsOpenCreateModal(() => true)}
          >
            <PlusIcon className="size-6 text-foreground" />
          </Button> */}
        </div>
      </div>
    </>
  );
});
export default ProjectsSidebar;
