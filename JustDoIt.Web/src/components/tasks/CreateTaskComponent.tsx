import { PlusIcon, PhotoIcon } from "@heroicons/react/16/solid";
import {
	getLocalTimeZone,
	now,
	parseZonedDateTime,
	today,
} from "@internationalized/date";
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
	Select,
	SelectItem,
} from "@nextui-org/react";
import React, { SyntheticEvent, useCallback, useRef, useState } from "react";
import { State } from "../../helpers/Types";
import { color } from "framer-motion";

type Props = {
	authToken: string;
	projectId: number;
	fetchData: () => void;
};

const CreateTaskComponent = ({
	authToken,
	projectId,
	fetchData,
	states,
}: Props) => {
	const { isOpen, onOpen, onOpenChange, onClose } = useDisclosure();
	const inputRef = useRef();

	const [image, setImage] = useState<string>("");
	const handleSubmit = useCallback(
		async (e: SyntheticEvent) => {
			e.preventDefault();
			const formData = new FormData(e.target);
			// console.log(formData);
			formData.set("projectId", projectId?.toString());

			// const date = parseZonedDateTime(formData.get("deadline"));
			// formData.set("deadline", date.toDate("[Europe/Zagreb]"));
			// console.log("formdata: ", formData);
			// return;

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
					console.log("task create json: ", json);
					if (response.ok) {
						fetchData();
					} else {
						console.warn(json.result);
					}
				})
				.catch((error) => console.error(error));
			onClose();
		},
		[authToken, fetchData, onClose, projectId]
	);

	const handleImageChange = useCallback((): void => {
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

	return (
		<div className='flex-1 flex justify-items-center'>
			<Button
				className='mx-auto'
				onPress={onOpen}
			>
				<PlusIcon className='size-6' />
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
								Create New Task
							</ModalHeader>
							<Divider />
							<ModalBody>
								<div className='grid grid-cols-2 gap-3 items-center'>
									<div className='flex flex-col'>
										<input
											type='file'
											name='attachment'
											className='hidden'
											accept='image/*'
											ref={inputRef}
											onChange={handleImageChange}
										/>
										{image ? (
											<Image
												removeWrapper
												className={"mx-auto h-24 cursor-pointer"}
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
									<Textarea
										name='summary'
										placeholder='Summary'
										label='Summary'
									></Textarea>
								</div>
								<div className='grid gap-3 grid-cols-2'>
									{/* <DatePicker
										className='hidden'
										variant='bordered'
										hideTimeZone
										showMonthAndYearPickers
									/> */}
									<DatePicker
										name='deadline'
										label='Deadline'
										variant='bordered'
										hideTimeZone
										showMonthAndYearPickers
										minValue={today(getLocalTimeZone())}
										defaultValue={today(getLocalTimeZone())}
									/>
								</div>
								<Textarea
									name='description'
									placeholder='Description'
									label='Description'
								></Textarea>
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
									Create Task
								</Button>
							</ModalFooter>
						</form>
					)}
				</ModalContent>
			</Modal>
		</div>
	);
};

export default CreateTaskComponent;
