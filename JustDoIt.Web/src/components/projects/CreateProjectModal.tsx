import { PlusIcon } from "@heroicons/react/16/solid";
import {
  Modal,
  ModalContent,
  ModalHeader,
  ModalBody,
  ModalFooter,
  Button,
  useDisclosure,
  Divider,
  Input,
  Textarea,
} from "@nextui-org/react";
import { SyntheticEvent, useEffect, useState } from "react";
import { ReactFilesPreview } from "react-files-preview";

const CreateProjectModal = () => {
  const { isOpen, onOpen, onOpenChange, onClose } = useDisclosure();
  const [picture, setPicture] = useState<File>(null);
  const handleSubmit = async (e: SyntheticEvent) => {
    e.preventDefault();
    const formData = new FormData(e.target);
    formData.set("pictureUrl", picture.name);
    console.log(formData);

    // await fetch("", {})
    //   .then(async (response) => {})
    //   .catch((error) => console.error(error));

    onClose();
  };

  useEffect(() => {
    if (isOpen) {
      const thatDIV = document.querySelector("div.flex.flex-row.max-h-2");
      thatDIV?.classList.remove("max-h-2");
    }
  }, [isOpen]);

  return (
    <>
      <Button onPress={onOpen}>
        <PlusIcon className="size-6" />
      </Button>
      <Modal isOpen={isOpen} onOpenChange={onOpenChange}>
        <ModalContent>
          {(onClose) => (
            <>
              <form onSubmit={handleSubmit} action="POST">
                <ModalHeader className="flex flex-col gap-1">
                  Create Project
                </ModalHeader>
                <Divider />
                <ModalBody>
                  <Input type="text" label="Title" name="title" />
                  {/* <Input type="text" label="Description" name="description" /> */}
                  <Textarea
                    label="Description"
                    placeholder="Enter your description"
                    className=""
                    name="description"
                  />
                  <ReactFilesPreview
                    accept="image/*"
                    allowEditing={false}
                    downloadFile={false}
                    getFiles={(files) => setPicture(files[0])}
                    // onChange={}
                    // onClick={() => {}}
                    // onDrop
                    // onRemove
                    // maxFileSize
                    maxFiles={1}
                    // disabled
                    // fileHeight
                    // fileWidth
                    // width
                    multiple={false}
                    showFileSize
                    // removeFile
                  />
                </ModalBody>
                <Divider />
                <ModalFooter>
                  <Button color="danger" variant="light" onPress={onClose}>
                    Close
                  </Button>
                  <Button color="primary" type="submit">
                    Create
                  </Button>
                </ModalFooter>
              </form>
            </>
          )}
        </ModalContent>
      </Modal>
    </>
  );
};

export default CreateProjectModal;
