import { PhotoIcon, PlusIcon } from "@heroicons/react/16/solid";
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
	Image,
	Select,
	SelectItem,
} from "@nextui-org/react";
import {
	SyntheticEvent,
	useCallback,
	useEffect,
	useRef,
	useState,
} from "react";
import { Status } from "../../helpers/Types";

interface Props {
	authToken: string;
	statuses: Status[];
	fetchProjects: () => void;
}

const CreateProjectModal = ({ authToken, fetchProjects, statuses }: Props) => {
	const { isOpen, onOpen, onOpenChange, onClose } = useDisclosure();
	const [image, setImage] = useState<string>("");
	const inputRef = useRef<React.MutableRefObject<HTMLInputElement>>();

	const handleSubmit = useCallback(
		async (e: SyntheticEvent) => {
			e.preventDefault();
			const formData = new FormData(e.target);

			// console.log("submitted data: ", formData);
			// return;
			await fetch("/api/v1/projects/create", {
				method: "POST",
				headers: {
					Authorization: `Bearer ${authToken}`,
				},
				body: formData,
			})
				.then(async (response) => {
					console.log("create project response", response);
					// const errorResponse = await response.json();
					// console.error("Error details:", errorResponse);
					// console.log(json);
					if (response.ok) {
						// const json = await response.json();
						//
						fetchProjects();
					} else {
						console.warn("ERROR");
					}
				})
				.catch((error) => console.error(error));

			onClose();
		},
		[authToken, fetchProjects, onClose]
	);

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
			setImage(() => "");
		}
	}, [isOpen]);

	return (
		<>
			<Button
				onPress={onOpen}
				color='primary'
				endContent={<PlusIcon className='size-6' />}
			>
				Add New
			</Button>
			<Modal
				isOpen={isOpen}
				onOpenChange={onOpenChange}
				size='xl'
			>
				<ModalContent>
					{(onClose) => (
						<form
							onSubmit={handleSubmit}
							action='POST'
						>
							<ModalHeader className='flex flex-col gap-1'>
								Create Project
							</ModalHeader>
							<Divider />
							<ModalBody>
								<div className='flex flex-col'>
									<input
										type='file'
										name='Picture'
										className='hidden'
										accept='image/*'
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
											radius='lg'
											// loading="lazy"
											shadow='sm'
										/>
									) : (
										<button
											type='button'
											className='p-0 mx-auto text-center'
											onClick={() => {
												inputRef.current.click();
											}}
										>
											<PhotoIcon className='size-24' />
										</button>
									)}
								</div>

								<Input
									placeholder='Project title'
									type='text'
									label='Title'
									name='Title'
								/>
								<div className='grid gap-2 grid-flow-col grid-cols-2'>
									<Input
										placeholder='Task shorthand'
										type='text'
										label='Key'
										name='Key'
									/>
									<Select
										label='Project Status'
										placeholder='Select a status'
										className='max-w-xl'
										name='StatusId'
									>
										{statuses.map((status) => (
											<SelectItem key={status.id}>{status.value}</SelectItem>
										))}
									</Select>
								</div>
								<Textarea
									label='Description'
									placeholder='Enter your description'
									className=''
									name='Description'
								/>
							</ModalBody>
							<Divider />
							<ModalFooter>
								<Button
									color='danger'
									variant='light'
									onPress={onClose}
								>
									Close
								</Button>
								<Button
									color='primary'
									type='submit'
								>
									Create
								</Button>
							</ModalFooter>
						</form>
					)}
				</ModalContent>
			</Modal>
		</>
	);
};

export default CreateProjectModal;
