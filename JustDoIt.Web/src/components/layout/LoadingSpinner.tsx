import { Modal, ModalContent, Spinner, ModalBody } from "@nextui-org/react";

interface Props {
	isOpen: boolean;
	onClose: () => void;
	onOpen: () => void;
}
function LoadingSpinner({ isOpen, onClose, onOpen }: Props) {
	return (
		<Modal
			backdrop={"blur"}
			isOpen={isOpen}
			onClose={onClose}
			hideCloseButton
			isDismissable={false}
			shadow={undefined}
		>
			<ModalContent className='bg-transparent border-none outline-none shadow-none'>
				<ModalBody className='bg-transparent border-none outline-none shadow-none'>
					<Spinner />
				</ModalBody>
			</ModalContent>
		</Modal>
	);
}
export default LoadingSpinner;
