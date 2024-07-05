import {
  Button,
  Divider,
  Listbox,
  ListboxItem,
  ListboxSection,
  Modal,
  ModalBody,
  ModalContent,
  ModalFooter,
  ModalHeader,
  useDisclosure,
} from "@nextui-org/react";
import { Key, ReactNode, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useAuth } from "../hooks/useAuth";

interface Task {
  id: number;
  title: string;
  adminId: string;
  description: string;
  projectId: number;
  pictureUrl: string;
  state: string;
  deadline: string;
}

const ProjectTasksPage = () => {
  const { id } = useParams();
  const [tasks, setTasks] = useState<Task[] | null>(null);

  const [selected, setSelected] = useState<Task | null>(null);
  const { isOpen, onOpen, onClose } = useDisclosure();

  const { user } = useAuth();
  const [projectRole, setProjectRole] = useState<string>();

  const navigate = useNavigate();

  useEffect(() => {
    fetchData();
  }, [id]);

  const fetchData = async () => {
    if (id) {
      try {
        const response = await fetch(
          "/api/Task?" + new URLSearchParams({ projectID: id }).toString(),
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
          console.log("Fetched tasks:", data); // Log the fetched tasks for debugging
          setTasks(data);
        } else {
          console.error("Failed to fetch tasks");
        }
      } catch (error) {
        console.error("Error fetching tasks:", error);
      }
    }
  };

  const GetTask = (key: Key) => {
    const tmp: Task | undefined = tasks?.filter(
      (t) => t.id.toString() == key.toString().split("-")[1]
    )[0];
    if (tmp) {
      checkIsAdminInProject(tmp);
      setSelected(tmp);
      // check userid check if is admin in project
      onOpen();
    }
  };

  const checkIsAdminInProject = async (tmp: Task) => {
    try {
      const response = await fetch(
        "/api/user/user_projects?" +
          new URLSearchParams({
            projectID: tmp.projectId,
            userId: user.id,
          }).toString(),
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
        console.log("Fetched user in proj:", data); // Log the fetched tasks for debugging
        setProjectRole(data[0].projectRole);
      } else {
        console.error("Failed to fetch tasks");
      }
    } catch (error) {
      console.error("Error fetching tasks:", error);
    }
  };

  return (
    <>
      <Modal
        backdrop={"blur"}
        isOpen={isOpen}
        onClose={onClose}
        className={"text-inherit bg-inherit"}
        isDismissable
      >
        <ModalContent>
          {(onClose) => (
            <>
              <ModalHeader className="flex flex-col gap-1 text-foreground-50">
                Title: {selected?.title}
              </ModalHeader>
              <Divider />

              <ModalBody className="py-3">
                <p className="text-foreground-100">
                  Deadline: {new Date(selected?.deadline).toLocaleDateString()}
                </p>
                <p className="text-foreground-100">
                  Description: {selected?.description}
                </p>
              </ModalBody>
              <Divider />
              <ModalFooter>
                <Button color="danger" variant="light" onPress={onClose}>
                  Close
                </Button>
                {projectRole == "admin" && (
                  <Button
                    color="primary"
                    onPress={() => navigate("/tasks/edit/" + selected?.id)}
                  >
                    Edit
                  </Button>
                )}
              </ModalFooter>
            </>
          )}
        </ModalContent>
      </Modal>
      <div className="grid grid-cols-3 p-4 gap-2 max-sm:grid-cols-1 max-sm:text-center">
        <div className="w-full border-small px-1 py-2 rounded-small border-default-200 dark:border-default-100">
          <Listbox
            aria-label="Tasks to be done"
            onAction={(key) => {
              GetTask(key);
            }}
          >
            <ListboxSection title={"TODO"}>
              {tasks
                ?.filter((task) => task.state == "todo") // Filter out tasks with undefined Id
                ?.map((task) => (
                  <ListboxItem key={`task-${task.id}`}>
                    {task.title}
                  </ListboxItem>
                )) || []}
            </ListboxSection>
          </Listbox>
        </div>
        <div className="w-full border-small px-1 py-2 rounded-small border-default-200 dark:border-default-100">
          <Listbox
            aria-label="Work in Progress tasks"
            onAction={(key) => {
              GetTask(key);
            }}
          >
            <ListboxSection title={"WIP"}>
              {tasks
                ?.filter((task) => task.state == "wip") // Filter out tasks with undefined Id
                ?.map((task) => (
                  <ListboxItem key={`task-${task.id}`}>
                    {task.title}
                  </ListboxItem>
                )) || []}
            </ListboxSection>
          </Listbox>
        </div>
        <div className="w-full border-small px-1 py-2 rounded-small border-default-200 dark:border-default-100">
          <Listbox
            aria-label="Tasks in integration check"
            onAction={(key) => {
              GetTask(key);
            }}
          >
            <ListboxSection title={"CHECKING"}>
              {tasks
                ?.filter((task) => task.state == "checking") // Filter out tasks with undefined Id
                ?.map((task) => (
                  <ListboxItem key={`task-${task.id}`}>
                    {task.title}
                  </ListboxItem>
                )) || []}
            </ListboxSection>
          </Listbox>
        </div>
      </div>
      <div className="mx-4 max-sm:text-center">
        <div className="w-full border-small px-1 py-2 rounded-small border-default-200 dark:border-default-100">
          <Listbox
            aria-label="Finished tasks"
            onAction={(key) => {
              GetTask(key);
            }}
          >
            <ListboxSection title={"DONE"}>
              {tasks
                ?.filter((task) => task.state == "done") // Filter out tasks with undefined Id
                ?.map((task) => (
                  <ListboxItem key={`task-${task.id}`}>
                    {task.title}
                  </ListboxItem>
                )) || []}
            </ListboxSection>
          </Listbox>
        </div>
      </div>
    </>
  );
};

export default ProjectTasksPage;
