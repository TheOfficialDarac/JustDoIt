import {
	Modal,
	ModalContent,
	ModalHeader,
	Divider,
	ModalBody,
	ModalFooter,
	Button,
	useDisclosure,
	Textarea,
	Input,
	DatePicker,
	Select,
	SelectItem,
} from "@nextui-org/react";
import React, { FormEvent, useCallback, useEffect, useState } from "react";
import TaskAttachments from "./TaskAttachments";
import TaskComments from "./TaskComments";
import { TaskAttachmentResponse, TaskResponse } from "../../types/Types";
import {
	getLocalTimeZone,
	parseDateTime,
	today,
} from "@internationalized/date";

type Props = {
	task: TaskResponse;
	roleName: string;
	authToken: string;
	fetchData: () => void;
};

const UpdateTaskComponent = ({
	task,
	roleName,
	authToken,
	fetchData,
}: Props) => {
	const { onOpen, isOpen, onClose, onOpenChange } = useDisclosure();
	const [isEnabledEditing, setIsEnabledEditing] = useState<boolean>(false);
	const [attachments, setAttachments] = useState<File[]>([]);

	const handleUpdateAttachments = useCallback(
		async (attachments: File[]) => {
			const formData = new FormData();
			formData.set("taskId", task?.id.toString());
			for (const file of attachments) {
				formData.append("attachments", file, file.name);
			}

			await fetch("/api/v1/attachments/task-attachments", {
				method: "PUT",
				headers: {
					Authorization: `Bearer ${authToken}`,
				},
				body: formData,
			})
				.then(async (response) => {
					console.log("update attachments response: ", response);
					if (response.ok) {
						//
					}
				})
				.catch((error) => console.error(error));
		},
		[authToken, task?.id]
	);

	const handleSubmit = useCallback(
		(e: FormEvent<HTMLFormElement>): void => {
			e.preventDefault();

			const data: FormData = new FormData(e.target);
			//   console.log("data: ", data);
			//   return;
			data.set("id", task?.id.toString());
			//   data.set("state", task?.state);

			const state = data.get("isActive");
			data.delete("isActive");
			data.set("isActive", state == "1" ? true : false);

			if (attachments) handleUpdateAttachments(attachments);

			fetch("/api/v1/tasks/update", {
				method: "PUT",
				headers: {
					Authorization: `Bearer ${authToken}`,
				},
				body: data,
			})
				.then(async (response) => {
					console.log("task text response: ", response);
					if (response.ok) {
						//
						fetchData();
					}
				})
				.catch((error) => console.error(error));

			onClose();
		},
		[
			attachments,
			authToken,
			fetchData,
			handleUpdateAttachments,
			onClose,
			task?.id,
		]
	);

	useEffect(() => {
		if (isOpen) {
			const thatDIV = document.querySelector("div.flex.flex-row.max-h-2");
			thatDIV?.classList.remove("max-h-2");
			setIsEnabledEditing(() => false);
		}
	}, [isOpen]);

	return (
		<>
			<Button
				className='p-2 w-full bg-transparent'
				onPress={onOpen}
			>
				{task?.title}
			</Button>
			<Modal
				isOpen={isOpen}
				onOpenChange={onOpenChange}
				size='3xl'
				scrollBehavior={"outside"}
			>
				<ModalContent>
					{(onClose) => (
						<form onSubmit={handleSubmit}>
							<ModalHeader className='flex gap-1 py-2 items-center px-6'>
								Task
							</ModalHeader>
							<Divider />
							<ModalBody>
								<div className='flex flex-1 text-xs gap-3'>
									<Select
										label='Select Task state'
										className=''
										name='state'
										isDisabled={!isEnabledEditing}
										defaultSelectedKeys={task?.state}
									>
										<SelectItem
											key={"1"}
											value={"1"}
										>
											To Do
										</SelectItem>
										<SelectItem
											key={"2"}
											value={"2"}
										>
											In progress
										</SelectItem>
										<SelectItem
											key={"3"}
											value={"3"}
										>
											Checking
										</SelectItem>
										<SelectItem
											key={"4"}
											value={"4"}
										>
											Done
										</SelectItem>
									</Select>
									<Select
										label='Select task status'
										name='isActive'
										className=''
										isDisabled={!isEnabledEditing}
										defaultSelectedKeys={task?.isActive ? "1" : "0"}
									>
										<SelectItem
											key={"0"}
											value={1}
										>
											Active
										</SelectItem>
										<SelectItem
											key={"1"}
											value={0}
										>
											Not Active
										</SelectItem>
									</Select>
								</div>
								<div className='flex flex-1 text-xs gap-3'>
									<DatePicker
										label='Created Date'
										variant='bordered'
										hideTimeZone
										showMonthAndYearPickers
										defaultValue={parseDateTime(task?.createdDate)}
										hourCycle={24}
										isDisabled
									/>
									<DatePicker
										label='Deadline'
										variant='bordered'
										hideTimeZone
										showMonthAndYearPickers
										defaultValue={parseDateTime(task?.deadline)}
										hourCycle={24}
										isDisabled={!isEnabledEditing}
										minValue={today(getLocalTimeZone())}
									/>
								</div>
								<Input
									label='Title'
									name='title'
									defaultValue={task?.title}
									disabled={!isEnabledEditing}
								/>
								<Textarea
									label='Summary'
									name='summary'
									disabled={!isEnabledEditing}
									size='sm'
									defaultValue={task?.summary}
								/>
								<Divider className='my-1' />
								<Textarea
									label='Description'
									size='md'
									name='description'
									disabled={!isEnabledEditing}
									defaultValue={task?.description}
								/>
								<Divider />
								<TaskAttachments
									taskId={task?.id}
									canEdit={!isEnabledEditing}
									authToken={authToken}
									isOpen={!isEnabledEditing}
									setAttachmentsInParent={setAttachments}
								/>
								<Divider />

								{/* <TaskComments /> */}
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
								{isEnabledEditing && (
									<Button
										color='primary'
										type='submit'
									>
										Save Changes
									</Button>
								)}
								{!isEnabledEditing && (
									<Button
										color='primary'
										type='button'
										onPress={() => setIsEnabledEditing(() => true)}
									>
										Edit
									</Button>
								)}
							</ModalFooter>
						</form>
					)}
				</ModalContent>
			</Modal>
		</>
	);
};

export default UpdateTaskComponent;
