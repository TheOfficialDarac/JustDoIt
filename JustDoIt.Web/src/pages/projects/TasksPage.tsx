import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { ProjectRoleResponse, TaskResponse } from "../../types/Types";
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
	const [userRole, setUserRole] = useState<ProjectRoleResponse>(null);

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
		};
		fetchData();

		const getRole = async () => {
			await fetch(`/api/v1/projects/user-role?ProjectId=${projectId}`, {
				method: "GET",
				headers: {
					"Content-Type": "application/json",
					Authorization: `Bearer ${authToken}`,
				},
			})
				.then(async (response) => {
					if (response.ok) {
						const json = await response.json();
						if (json.result.isSuccess) {
							setUserRole(() => json.data);
						} else {
							console.warn("user has no role");
						}
					}
				})
				.catch((error) => console.error(error));
		};
		getRole().then(() => onClose());
	}, [authToken, onClose, onOpen, projectId]);

	return (
		<div className='border p-2'>
			TasksPage projectID: {projectId}
			{userRole?.roleName == "ADMIN" && <h4>I HAVE THE POWER!</h4>}
			<div className='border p-2 flex w-full'>
				<TaskGroup
					title={"TO DO"}
					state={"1"}
					tasks={tasks}
					roleName={userRole?.roleName}
					authToken={authToken}
				/>
				<TaskGroup
					title={"In Progress"}
					state={"2"}
					tasks={tasks}
					roleName={userRole?.roleName}
					authToken={authToken}
				/>
				<TaskGroup
					title={"Checking"}
					state={"3"}
					tasks={tasks}
					roleName={userRole?.roleName}
					authToken={authToken}
				/>
				<TaskGroup
					title={"Done"}
					state={"4"}
					tasks={tasks}
					roleName={userRole?.roleName}
					authToken={authToken}
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
