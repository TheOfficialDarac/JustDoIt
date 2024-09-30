import { Button, useDisclosure } from "@nextui-org/react";
import { ProjectRole, State, Task } from "../../helpers/Types";
import LoadingSpinner from "../layout/LoadingSpinner";
import TaskGroup from "../tasks/TaskGroup";
import { useCallback, useEffect, useState } from "react";
import CreateTaskComponent from "./CreateTaskComponent";

interface Props {
	projectId: number;
	userRole: ProjectRole;
	authToken: string;
	// projectKey: string;
}

const TasksComponent = ({
	// projectKey,
	projectId,
	userRole,
	authToken,
}: Props) => {
	const { isOpen, onClose, onOpen, onOpenChange, isControlled } =
		useDisclosure();
	const loadingSpinner = useDisclosure();
	const [tasks, setTasks] = useState<Task[]>([]);
	const [states, setStates] = useState<State[]>([]);

	const fetchTasks = useCallback(async () => {
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
				console.log("TASKS RESPONSE: ", json);
				if (json.result.isSuccess) {
					setTasks(() => json.data);
				} else {
					setTasks(() => []);
				}
			}
		} catch (error) {
			console.error(error);
		}
		onClose();
	}, [authToken, onClose, onOpen, projectId, setTasks]);
	const fetchStates = useCallback(async () => {
		onOpen();
		try {
			const response = await fetch(`/api/v1/utils/states`, {
				method: "GET",
				headers: {
					"Content-Type": "application/json",
					Authorization: `Bearer ${authToken}`,
				},
			});

			console.log(response);
			if (response.ok) {
				const json = await response.json();
				console.log("STATES RESPONSE: ", json);
				if (json.result.isSuccess) {
					setStates(() => json.data);
				} else {
					setStates(() => []);
				}
			}
		} catch (error) {
			console.error(error);
		}
		onClose();
	}, [authToken, onClose, onOpen]);

	useEffect(() => {
		loadingSpinner.onOpen();
		fetchTasks();
		fetchStates();
		loadingSpinner.onClose();
	}, []);

	return (
		<div className='border p-2'>
			<CreateTaskComponent
				authToken={authToken}
				projectId={projectId}
				fetchData={fetchTasks}
			/>
			{/* TasksComponent projectID: {projectId} */}
			<div className='p-2 flex w-full'>
				<TaskGroup
					title={"TO DO"}
					state={1}
					tasks={tasks}
					roleName={userRole?.roleName}
					authToken={authToken}
					fetchData={fetchTasks}
				/>
				<TaskGroup
					title={"In Progress"}
					state={2}
					tasks={tasks}
					roleName={userRole?.roleName}
					authToken={authToken}
					fetchData={fetchTasks}
				/>
				<TaskGroup
					title={"Checking"}
					state={3}
					tasks={tasks}
					roleName={userRole?.roleName}
					authToken={authToken}
					fetchData={fetchTasks}
				/>
				<TaskGroup
					title={"Done"}
					state={4}
					tasks={tasks}
					roleName={userRole?.roleName}
					authToken={authToken}
					fetchData={fetchTasks}
				/>
			</div>
			<LoadingSpinner
				spinnerIsShown={loadingSpinner.isOpen}
				closeSpinner={loadingSpinner.onClose}
				showSpinner={loadingSpinner.onOpen}
			/>
		</div>
	);
};

export default TasksComponent;
