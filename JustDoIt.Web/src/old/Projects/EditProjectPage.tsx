import { Button, Input } from "@nextui-org/react";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

interface Project {
  id: string;
  title: string;
  adminId: string;
  pictureUrl: string;
  description: string;
}

const EditTaskPage = () => {
  const { id } = useParams();
  const [project, setProject] = useState<Project | null>(null);
  const [error, setError] = useState<string>("");

  const [title, setTitle] = useState<string>("");
  const [description, setDescription] = useState<string>("");

  useEffect(() => {
    fetchData();
  }, [id]);

  const fetchData = async () => {
    if (id) {
      try {
        const response = await fetch("/api/Project/" + id, {
          method: "GET",
          mode: "cors",
          headers: {
            "Content-Type": "application/json",
          },
        });

        if (response.ok) {
          const data = await response.json();
          setProject(data);
          setTitle(data.title);
          setDescription(data.description);
        } else {
          console.error("Failed to fetch project");
        }
      } catch (error) {
        console.error("Error fetching project:", error);
      }
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (project) {
      const updatedTask: Project = {
        ...project,
        title,
        description,
      };
      console.log("final task:", updatedTask);
      //   return;
      try {
        const response = await fetch("/api/Project/update", {
          method: "PUT",
          mode: "cors",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(updatedTask),
        });

        if (response.ok) {
          setError("Edits made successfully");
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
          placeholder="Enter project title"
          variant="bordered"
          type="text"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
        />
        <Input
          label="Description"
          placeholder="Enter project description"
          type="text"
          variant="bordered"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
        <Button type="submit">Save</Button>
      </form>
      {error && <p className="m-2 p-2 text-center danger">{error}</p>}
    </div>
  );
};

export default EditTaskPage;
