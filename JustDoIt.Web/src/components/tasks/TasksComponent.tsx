import { Button, useDisclosure } from "@nextui-org/react";
import { ProjectRoleResponse, TaskResponse } from "../../helpers/Types";
import LoadingSpinner from "../layout/LoadingSpinner";
import TaskGroup from "../tasks/TaskGroup";
import { useCallback, useEffect, useState } from "react";
import CreateTaskComponent from "./CreateTaskComponent";

interface Props {
	projectId: number;
	userRole: ProjectRoleResponse;
	authToken: string;
}

const TasksComponent = ({ projectId, userRole, authToken }: Props) => {
	const { onOpen, isOpen, onClose } = useDisclosure();
	const [tasks, setTasks] = useState<TaskResponse[]>([]);

	const fetchData = useCallback(async () => {
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
	}, [authToken, onClose, onOpen, projectId]);

	useEffect(() => {
		fetchData();
	}, [fetchData]);

	return (
		<div className='border p-2'>
			<CreateTaskComponent
				authToken={authToken}
				projectId={projectId}
				fetchData={fetchData}
			/>
			{/* TasksComponent projectID: {projectId} */}
			<div className='p-2 flex w-full'>
				<TaskGroup
					title={"TO DO"}
					state={"1"}
					tasks={tasks}
					roleName={userRole?.roleName}
					authToken={authToken}
					fetchData={fetchData}
				/>
				<TaskGroup
					title={"In Progress"}
					state={"2"}
					tasks={tasks}
					roleName={userRole?.roleName}
					authToken={authToken}
					fetchData={fetchData}
				/>
				<TaskGroup
					title={"Checking"}
					state={"3"}
					tasks={tasks}
					roleName={userRole?.roleName}
					authToken={authToken}
					fetchData={fetchData}
				/>
				<TaskGroup
					title={"Done"}
					state={"4"}
					tasks={tasks}
					roleName={userRole?.roleName}
					authToken={authToken}
					fetchData={fetchData}
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

export default TasksComponent;
