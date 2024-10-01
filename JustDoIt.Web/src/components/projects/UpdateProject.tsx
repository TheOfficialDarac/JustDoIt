import { PhotoIcon } from "@heroicons/react/16/solid";
import { Project, ProjectResponse, Status } from "../../helpers/Types";
import {
  Button,
  Modal,
  ModalContent,
  ModalHeader,
  ModalBody,
  ModalFooter,
  Input,
  Textarea,
  Image,
  Select,
  SelectItem,
} from "@nextui-org/react";
import type { UseDisclosureReturn } from "@nextui-org/use-disclosure";
import {
  SyntheticEvent,
  useCallback,
  useEffect,
  useRef,
  useState,
} from "react";

type Props = {
  project: Project;
  authToken: string;
  fetchProjects: () => void;
  disclosure: UseDisclosureReturn;
  statuses: Status[];
};

const UpdateProject = ({
  project,
  authToken,
  fetchProjects,
  disclosure,
  statuses,
}: Props) => {
  const { isOpen, onOpen, onOpenChange, onClose } = disclosure;
  const [image, setImage] = useState<string>("");

  const inputRef = useRef<React.MutableRefObject<HTMLInputElement>>();

  const getPicture = useCallback(async () => {
    if (project?.pictureUrl) {
      const url: string = `/api/v1/attachments/get-file?filepath=${project?.pictureUrl}`;
      fetch(url, {
        method: "GET",
      }).then(async (response) => {
        response.blob().then((blob) => {
          const file = new File([blob], `${project?.id}.jpg`, {
            type: blob.type,
          });
          setImage(() => URL.createObjectURL(file));
        });
      });
    }
  }, [project?.id, project?.pictureUrl]);

  const handleImageChange = useCallback(() => {
    const file = inputRef.current.files[0];

    if (file) {
      const reader = new FileReader();

      reader.onload = function (e) {
        setImage(() => e.target.result);
      };

      reader.readAsDataURL(file); // Read the file as a data URL
    }
    // console.log(image);
  }, []);

  useEffect(() => {
    if (isOpen) {
      /* empty */
      const thatDIV = document.querySelector("div.flex.flex-row.max-h-2");
      thatDIV?.classList.remove("max-h-2");
      setImage(() => "");
      getPicture();
    }
  }, [getPicture, isOpen, project]);

  const handleSubmit = async (e: SyntheticEvent) => {
    e.preventDefault();
    const formData = new FormData(e.target);
    formData.set("id", project?.id.toString());

    console.log("submitted edit project: ", formData);
    // return;
    await fetch("/api/v1/projects/update", {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${authToken}`,
      },
      body: formData,
    })
      .then(async (response) => {
        const json = await response.json();
        if (response.ok) {
          fetchProjects();
        } else {
          console.warn(json.result);
        }
      })
      .catch((error) => console.error(error));

    onClose();
  };

  // const handleDelete = useCallback(async (e: PressEvent): void => {
  // 	await fetch("/api/v1/projects/delete", {
  // 		method: "delete",
  // 		body: JSON.stringify({ id: project?.id }),
  // 		headers: {
  // 			Authorization: `Bearer ${authToken}`,
  // 			"Content-Type": "application/json",
  // 		},
  // 	})
  // 		.then(async (response) => {
  // 			console.log("response on delete", response);
  // 			const json = await response.json();
  // 			if (response.ok) {
  // 			}
  // 		})
  // 		.catch((error) => console.error(error));
  // 	fetchProjects();
  // 	onClose();
  // }, []);

  return (
    <Modal isOpen={isOpen} onOpenChange={onOpenChange} size="lg">
      <ModalContent>
        {(onClose) => (
          <form onSubmit={handleSubmit}>
            <ModalHeader className="flex flex-col gap-1">
              Update Project
            </ModalHeader>
            <ModalBody>
              <div className="flex flex-col">
                <input
                  type="file"
                  name="attachment"
                  className="hidden"
                  accept="image/*"
                  ref={inputRef}
                  onChange={handleImageChange}
                />
                {image ? (
                  <Image
                    removeWrapper
                    className={"mx-auto w-48 cursor-pointer"}
                    src={image}
                    onClick={() => {
                      inputRef.current.click();
                    }}
                    radius="lg"
                    // loading="lazy"
                    shadow="sm"
                  />
                ) : (
                  <button
                    type="button"
                    className="p-0 mx-auto text-center"
                    onClick={() => {
                      inputRef.current.click();
                    }}
                  >
                    <PhotoIcon className="size-24" />
                  </button>
                )}
              </div>
              <Input
                type="text"
                label="Title"
                name="title"
                defaultValue={project?.title}
              />
              <div className="grid gap-2 grid-flow-col grid-cols-2">
                <Input
                  defaultValue={project?.key}
                  type="text"
                  label="Key"
                  name="Key"
                />
                <Select
                  label="Project Status"
                  placeholder="Select a status"
                  className="max-w-xl"
                  name="StatusId"
                  defaultSelectedKeys={[project?.statusId.toString()]}
                >
                  {statuses.map((status) => (
                    <SelectItem key={status.id} value={status.id}>
                      {status.value}
                    </SelectItem>
                  ))}
                </Select>
              </div>
              {/* <Input type="text" label="Description" name="description" /> */}
              <Textarea
                label="Description"
                placeholder="Enter your description"
                className=""
                name="description"
                defaultValue={project?.description}
              />
            </ModalBody>
            <ModalFooter>
              <Button color="danger" variant="light" onPress={onClose}>
                Close
              </Button>
              {/* <Button
									color='danger'
									variant='light'
									onPress={handleDelete}
								>
									Delete
								</Button> */}
              <Button color="primary" type="submit">
                Save Changes
              </Button>
            </ModalFooter>
          </form>
        )}
      </ModalContent>
    </Modal>
  );
};

export default UpdateProject;
