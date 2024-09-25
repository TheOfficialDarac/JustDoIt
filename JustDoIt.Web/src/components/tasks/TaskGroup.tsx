import { useState } from "react";

import { TaskResponse } from "../../types/Types";
import {
	Button,
	Divider,
	Modal,
	ModalBody,
	ModalContent,
	ModalFooter,
	ModalHeader,
	useDisclosure,
} from "@nextui-org/react";
import TaskComments from "./TaskComments";
import TaskAttachments from "./TaskAttachments";

interface Props {
	title: string;
	state: string;
	tasks: TaskResponse[];
	roleName: string;
	authToken: string;
}

const TaskGroup = ({ title, state, tasks, roleName, authToken }: Props) => {
	const { isOpen, onOpen, onOpenChange } = useDisclosure();
	const [selectedTask, setSelectedTask] = useState<TaskResponse>(null);
	const handleSelectTask = (task: TaskResponse) => {
		setSelectedTask(() => task);
		onOpen();
	};
	return (
		<>
			<figure className='border p-2 flex-1'>
				<figcaption className='p-2 text-center'>{title}</figcaption>
				<ul>
					{tasks.map((task) => {
						if (task.state === state)
							return (
								<li
									className='border rounded-sm'
									key={task?.id}
								>
									<Button
										className='p-2 w-full bg-transparent'
										onPress={() => handleSelectTask(task)}
									>
										{task?.title}
									</Button>
								</li>
							);
					})}
				</ul>
			</figure>
			<Modal
				isOpen={isOpen}
				onOpenChange={onOpenChange}
				size='3xl'
			>
				<ModalContent>
					{(onClose) => (
						<>
							<ModalHeader className='flex gap-1 py-2 items-center px-6'>
								<p className='flex-1 text-center'>{selectedTask?.title}</p>
								<div className='flex flex-col flex-1 text-xs'>
									<p>{selectedTask?.createdDate} created @</p>
									<p>{selectedTask?.deadline} deadline</p>
								</div>
							</ModalHeader>
							<Divider />
							<ModalBody>
								<p>{selectedTask?.summary}</p>
								<Divider className='my-1' />
								<p>{selectedTask?.description}</p>
								<Divider />
								<TaskAttachments
									taskId={selectedTask?.id}
									roleName={roleName}
									authToken={authToken}
								/>
								<Divider />

								<TaskComments />
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
									onPress={onClose}
								>
									Action
								</Button>
							</ModalFooter>
						</>
					)}
				</ModalContent>
			</Modal>
		</>
	);
};

export default TaskGroup;
