import { ReactNode, useEffect, useState } from "react";
import { useAuth } from "../../hooks/useAuth";

interface Task {
  Id: number;
  title: string;
  adminId: string;
  description: string;
  projectId: number;
  pictureUrl: string;
  state: string;
  deadline: string;
}

const TasksPage = () => {
  const { user } = useAuth();
  const [tasks, setTasks] = useState<Task[] | null>(null);
  const [display, setDisplay] = useState<ReactNode>(null);

  useEffect(() => {
    const getProjects = async () => {
      if (user) {
        try {
          const response = await fetch(
            "/api/Task" + new URLSearchParams().toString(),
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
            // setTasks(data);
            console.log(data);
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
    if (tasks) {
      setDisplay(
        <div>
          {tasks.map((task, index) => (
            <div key={index}>{task?.projectId}</div>
          ))}
        </div>
      );
    }
  }, [tasks]);

  return <>{display}</>;
};

export default TasksPage;
