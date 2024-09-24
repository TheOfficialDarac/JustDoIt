import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { TaskResponse } from "../../types/Types";
import TaskGroup from "../../components/tasks/TaskGroup";
import LoadingSpinner from "../../components/layout/LoadingSpinner";
import { useDisclosure } from "@nextui-org/react";

interface Props {
	authToken: string;
}

const TasksPage = ({ authToken }: Props) => {
	const { onOpen, isOpen, onClose } = useDisclosure();

	const { projectId } = useParams();
	const [tasks, setTasks] = useState<TaskResponse[]>([]);

	useEffect(() => {
		const fetchData = async () => {
			onOpen();
			try {
				const response = await fetch(`/api/v1/tasks?ProjectId=${projectId}`, {
					method: "GET",
					headers: {
						"Content-Type": "application/json",
						Authorization: `Bearer ${authToken}`,
					},
				});

				console.log(response);
				if (response.ok) {
					const json = await response.json();
					console.log("TASKS: ", json);
					if (json.result.isSuccess) {
						setTasks(() => json.data);
						console.log(json);
					}
				}
			} catch (error) {
				console.error(error);
			}
			onClose();
		};
		fetchData();
	}, [authToken, onClose, onOpen, projectId]);
	
	return (
		<div className='border p-2'>
			TasksPage projectID: {projectId}
			<div className='border p-2 flex w-full'>
				<TaskGroup
					title={"TO DO"}
					state={"1"}
					tasks={tasks}
				/>
				<TaskGroup
					title={"In Progress"}
					state={"2"}
					tasks={tasks}
				/>
				<TaskGroup
					title={"Checking"}
					state={"3"}
					tasks={tasks}
				/>
				<TaskGroup
					title={"Done"}
					state={"4"}
					tasks={tasks}
				/>
			</div>
			<LoadingSpinner
				isOpen={isOpen}
				onClose={onClose}
				onOpen={onOpen}
			/>
		</div>
	);
};

export default TasksPage;
