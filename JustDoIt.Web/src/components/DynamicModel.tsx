import {
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
  Button,
  useDisclosure,
} from "@nextui-org/react";
import { ReactNode, useState } from "react";

interface Props {
  children: ReactNode;
}
export default function DynamicModal({ children }: Props) {
  const { isOpen, onOpen, onOpenChange } = useDisclosure();
  const [modalContent, setModalContent] = useState("Default content");

  const openModalWithContent = (content) => {
    setModalContent(content);
    onOpen();
  };

  return (
    <>
      <Button onPress={() => openModalWithContent(children)}>Open Modal</Button>

      <Modal isOpen={isOpen} onOpenChange={onOpenChange}>
        <ModalHeader>Modal Title</ModalHeader>
        <ModalBody>{modalContent}</ModalBody>
        <ModalFooter>
          <Button auto flat color="danger" onPress={onOpenChange}>
            Close
          </Button>
        </ModalFooter>
      </Modal>
    </>
  );
}
