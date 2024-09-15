import { Button, Input } from "@nextui-org/react";
import { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useAuth } from "../../hooks/useAuth";

interface Project {
  title: string;
  adminId: string;
  pictureUrl: string;
  description: string;
}

const EditTaskPage = () => {
  const { id } = useParams();
  const [error, setError] = useState<string>("");

  const [title, setTitle] = useState<string>("");
  const [description, setDescription] = useState<string>("");

  const { user } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    const updatedProject: Project = {
      adminId: user.id,
      title: title,
      description: description,
      pictureUrl: "",
    };
    console.log("final project:", updatedProject);
    // return;
    try {
      const response = await fetch("/api/Project/create", {
        method: "POST",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(updatedProject),
      });

      if (response.ok) {
        setError("Success");
        navigate("/projects");
        // Optionally navigate or show a success message
      } else {
        setError("Failed to craete task.");
      }
    } catch (error) {
      console.error("Error creating task:", error);
      setError("Error: Error creating task.");
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
          placeholder="Enter Project title"
          variant="bordered"
          type="text"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
        />
        <Input
          label="Description"
          placeholder="Enter Project description"
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
