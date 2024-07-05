import { Button, Input, Radio, RadioGroup } from "@nextui-org/react";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

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

const EditTaskPage = () => {
  const { id } = useParams();
  const [task, setTask] = useState<Task | null>(null);
  const [error, setError] = useState<string>("");

  const [title, setTitle] = useState<string>("");
  const [description, setDescription] = useState<string>("");
  const [state, setState] = useState<string>("");
  const [deadline, setDeadline] = useState<string>("");

  useEffect(() => {
    fetchData();
  }, [id]);

  const fetchData = async () => {
    if (id) {
      try {
        const response = await fetch("/api/Task/" + id, {
          method: "GET",
          mode: "cors",
          headers: {
            "Content-Type": "application/json",
          },
        });

        if (response.ok) {
          const data = await response.json();
          setTask(data);
          setTitle(data.title);
          setDescription(data.description);
          setState(data.state);
          setDeadline(data.deadline.split("T")[0]); // Set deadline as YYYY-MM-DD
        } else {
          console.error("Failed to fetch task");
        }
      } catch (error) {
        console.error("Error fetching task:", error);
      }
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (task) {
      const updatedTask: Task = {
        ...task,
        title,
        description,
        state,
        deadline,
      };
      console.log("final task:", updatedTask);
      //   return;
      try {
        const response = await fetch("/api/Task/update", {
          method: "PUT",
          mode: "cors",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(updatedTask),
        });

        if (response.ok) {
          setError("");
          // Optionally navigate or show a success message
        } else {
          setError("Failed to update task.");
        }
      } catch (error) {
        console.error("Error updating task:", error);
        setError("Error: Error updating task.");
      }
    }
  };

  return (
    <div className="p-4">
      <form
        onSubmit={handleSubmit}
        className="p-4 border rounded-lg flex gap-4 flex-col"
      >
        <Input
          autoFocus
          label="Title"
          placeholder="Enter task title"
          variant="bordered"
          type="text"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
        />
        <Input
          label="Description"
          placeholder="Enter task description"
          type="text"
          variant="bordered"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
        <Input
          label="Deadline"
          placeholder="Enter task deadline"
          type="date"
          variant="bordered"
          value={deadline}
          onChange={(e) => setDeadline(e.target.value)}
        />
        <RadioGroup
          label="Select project state"
          color="secondary"
          value={state}
          onChange={(e) => setState(e.target.value)}
        >
          <Radio value="todo">TODO</Radio>
          <Radio value="wip">Work in progress</Radio>
          <Radio value="checking">Checking</Radio>
          <Radio value="done">DONE</Radio>
        </RadioGroup>
        <Button type="submit">Save</Button>
      </form>
      {error && <p className="m-2 p-2 text-center danger">{error}</p>}
    </div>
  );
};

export default EditTaskPage;
