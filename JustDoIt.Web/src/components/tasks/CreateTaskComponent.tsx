import { PlusIcon, PhotoIcon } from "@heroicons/react/16/solid";
import {
  Button,
  Modal,
  ModalContent,
  ModalHeader,
  Divider,
  ModalBody,
  image,
  Input,
  Textarea,
  ModalFooter,
  useDisclosure,
  Image,
  DatePicker,
} from "@nextui-org/react";
import React, { SyntheticEvent, useCallback, useRef } from "react";

type Props = {
  authToken: string;
  projectId: number;
  fetchData: () => void;
};

const CreateTaskComponent = ({ authToken, projectId, fetchData }: Props) => {
  const { isOpen, onOpen, onOpenChange, onClose } = useDisclosure();
  const inputRef = useRef();

  const handleSubmit = useCallback(
    async (e: SyntheticEvent) => {
      e.preventDefault();
      const formData = new FormData(e.target);
      // console.log(formData);
      formData.set("projectId", projectId?.toString());

      await fetch("/api/v1/tasks/create", {
        method: "post",
        headers: {
          Authorization: `Bearer ${authToken}`,
        },
        body: formData,
      })
        .then(async (response) => {
          console.log("task create response: ", response);
          const json = await response.json();
          if (response.ok) {
            console.log("task create json: ", json);
            fetchData();
          } else {
            console.warn(json.result);
          }
        })
        .catch((error) => console.error(error));
      onClose();
    },
    [authToken, projectId]
  );

  //   {
  //     "title": "string",
  //     "summary": "string",
  //     "description": "string",
  //     "projectId": 0,
  //     "issuerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  //     "pictureUrl": "string",
  //     "deadline": "2024-09-26T22:40:18.534Z"
  //   }
  return (
    <>
      <div>
        <Button onPress={onOpen}>
          <PlusIcon className="size-6" />
        </Button>
      </div>
      <Modal isOpen={isOpen} onOpenChange={onOpenChange} size="xl">
        <ModalContent>
          {(onClose) => (
            <form onSubmit={handleSubmit} action="POST">
              <ModalHeader className="flex flex-col gap-1">
                Create New Task
              </ModalHeader>
              <Divider />
              <ModalBody>
                <div className="flex flex-col">
                  <input
                    type="file"
                    name="attachment"
                    className="hidden"
                    accept="image/*"
                    ref={inputRef}
                    // onChange={handleImageChange}
                  />
                  {false ? (
                    <Image
                      removeWrapper
                      className={"mx-auto w-48 cursor-pointer"}
                      //   src={image}
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
                <Input placeholder="Title" name="title" label="Title" />
                <DatePicker
                  name="deadline"
                  label="Deadline"
                  variant="bordered"
                  showMonthAndYearPickers
                />
                <Textarea
                  name="summary"
                  placeholder="Summary"
                  label="Summary"
                ></Textarea>
                <Textarea
                  name="description"
                  placeholder="Description"
                  label="Description"
                ></Textarea>
              </ModalBody>
              <Divider />
              <ModalFooter>
                <Button color="danger" variant="light" onPress={onClose}>
                  Close
                </Button>
                <Button color="primary" type="submit">
                  Create Task
                </Button>
              </ModalFooter>
            </form>
          )}
        </ModalContent>
      </Modal>
    </>
  );
};

export default CreateTaskComponent;
