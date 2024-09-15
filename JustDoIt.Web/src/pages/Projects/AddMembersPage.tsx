import {
  Button,
  Input,
  Modal,
  ModalBody,
  ModalContent,
  ModalFooter,
  ModalHeader,
  Radio,
  RadioGroup,
  useDisclosure,
} from "@nextui-org/react";
import { ReactNode, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useAuth } from "../../hooks/useAuth";
import { ProjectMembers } from "../../components/ProjectMembers";

interface Project {
  title: string;
  adminId: string;
  pictureUrl: string;
  description: string;
}

const EditTaskPage = () => {
  const { id } = useParams();
  const [error, setError] = useState<string>("");
  const [display, setDisplay] = useState<ReactNode>();

  const { isOpen, onOpen, onOpenChange } = useDisclosure();

  const { user } = useAuth();
  const navigate = useNavigate();

  //   const handleSubmit = async (e) => {
  //     e.preventDefault();
  //     const updatedProject: Project = {
  //       adminId: user.id,
  //       title: title,
  //       description: description,
  //       pictureUrl: "",
  //     };
  //     console.log("final project:", updatedProject);
  //     // return;
  //     try {
  //       const response = await fetch("/api/Project/create", {
  //         method: "POST",
  //         mode: "cors",
  //         headers: {
  //           "Content-Type": "application/json",
  //         },
  //         body: JSON.stringify(updatedProject),
  //       });

  //       if (response.ok) {
  //         setError("Success");
  //         navigate("/projects");
  //         // Optionally navigate or show a success message
  //       } else {
  //         setError("Failed to craete task.");
  //       }
  //     } catch (error) {
  //       console.error("Error creating task:", error);
  //       setError("Error: Error creating task.");
  //     }
  //   };

  function leaveProject(): void {
    // throw new Error("Function not implemented.");
    setDisplay(
      <>
        {(onClose) => (
          <>
            <ModalHeader className="flex flex-col gap-1">
              Modal Title
            </ModalHeader>
            <ModalBody></ModalBody>
            <ModalFooter>
              <Button color="danger" variant="light" onPress={onClose}>
                Close
              </Button>
              <Button color="primary" onPress={onClose}>
                Action
              </Button>
            </ModalFooter>
          </>
        )}
      </>
    );
    onOpen;
  }

  return (
    <div className="p-4">
      <form
        // onSubmit={handleSubmit}
        className="p-4 border rounded-lg flex gap-4 flex-col"
      >
        <Button type="button" onClick={() => leaveProject()}>
          Leave
        </Button>
        <Button type="button" onPress={() => AddToProject()}>
          Add
        </Button>
        <Button type="button" onPress={() => RemoveFromProject()}>
          Remove
        </Button>
        <div className="m-2 border-1 rounded-lg p-4 grid grid-cols-4">
          <ProjectMembers project={{ id: id }} />
        </div>

        {/* <Button type="submit">Save</Button> */}
      </form>
      {/* {error && <p className="m-2 p-2 text-center danger">{error}</p>} */}
      <Modal isOpen={isOpen} onOpenChange={onOpenChange}>
        <ModalContent children={display}></ModalContent>
      </Modal>
    </div>
  );
};

export default EditTaskPage;
