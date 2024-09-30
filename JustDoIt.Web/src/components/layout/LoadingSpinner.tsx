import { Modal, ModalContent, Spinner, ModalBody } from "@nextui-org/react";

interface Props {
  spinnerIsShown: boolean;
  closeSpinner: () => void;
  showSpinner: () => void;
}
function LoadingSpinner({ spinnerIsShown, closeSpinner, showSpinner }: Props) {
  return (
    <Modal
      backdrop={"blur"}
      isOpen={spinnerIsShown}
      onClose={closeSpinner}
      hideCloseButton
      isDismissable={false}
      shadow={undefined}
    >
      <ModalContent className="bg-transparent border-none outline-none shadow-none">
        <ModalBody className="bg-transparent border-none outline-none shadow-none">
          <Spinner />
        </ModalBody>
      </ModalContent>
    </Modal>
  );
}
export default LoadingSpinner;
