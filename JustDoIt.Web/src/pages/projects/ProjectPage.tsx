import { useDisclosure } from "@nextui-org/react";
import { useCallback, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import TasksComponent from "../../components/tasks/TasksComponent";
import { ProjectRole } from "../../helpers/Types";
import LoadingSpinner from "../../components/layout/LoadingSpinner";

type Props = {
	authToken: string;
};

const ProjectPage = ({ authToken }: Props) => {
	const { projectId } = useParams();
	const { isOpen, onClose, onOpen, onOpenChange, isControlled } =
		useDisclosure();
	const loadingSpinner = useDisclosure();
	const [userRole, setUserRole] = useState<ProjectRole>(null);

	const fetchProjectRole = useCallback(async () => {
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
	}, [authToken, projectId]);

	useEffect(() => {
		loadingSpinner.onOpen();
		fetchProjectRole();
		loadingSpinner.onClose();
	}, [fetchProjectRole]);

	return (
		<>
			<div>
				{/* ProjectPage, id: {projectId} my role: {userRole?.roleName} */}
				<div>
					<TasksComponent
						projectId={Number(projectId)}
						userRole={userRole}
						authToken={authToken}
						// projectKey={""}
					/>
				</div>
				{/* <div>info</div> */}
			</div>
			<LoadingSpinner
				spinnerIsShown={loadingSpinner.isOpen}
				closeSpinner={loadingSpinner.onClose}
				showSpinner={loadingSpinner.onOpen}
			/>
		</>
	);
};

export default ProjectPage;
