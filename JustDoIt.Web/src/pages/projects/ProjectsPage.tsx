import { useCallback, useEffect, useState } from "react";
import ProjectsSidebar from "../../components/projects/ProjectsSidebar";
import SelectedProject from "../../components/projects/SelectedProject";
import { AuthResponse, ProjectResponse, UserResponse } from "../../types/Types";
import LoadingSpinner from "../../components/layout/LoadingSpinner";
import { useDisclosure } from "@nextui-org/react";

interface Props {
	user: UserResponse;
	authToken: string;
}
function ProjectsPage({ user, authToken }: Props) {
	const [projects, setProjects] = useState<ProjectResponse[]>([]);
	const [selectedIndex, setSelectedIndex] = useState<number>(0);
	const { onOpen, isOpen, onClose } = useDisclosure();

	useEffect(() => {
		const fetchProjects = () => {
			onOpen();
			fetch("/api/v1/projects/user", {
				method: "GET",
				headers: {
					"Content-Type": "application/json",
					Authorization: `Bearer ${authToken}`,
				},
			})
				.then(async (response) => {
					if (response.ok) {
						const json = await response.json();
						setProjects((prev) => [...prev, ...json.data]);
					}
				})
				.catch((error) => {
					console.error(error);
				})
				.finally(onClose);
		};
		fetchProjects();
	}, [authToken, onClose, onOpen]);

	return (
		<div className='flex gap-3 border p-2'>
			{/* <div>{user?.userName}</div> */}

			<ProjectsSidebar
				projects={projects}
				setSelectedIndex={setSelectedIndex}
			/>
			<SelectedProject project={projects[selectedIndex]} />
			<LoadingSpinner
				isOpen={isOpen}
				onClose={onClose}
				onOpen={onOpen}
			/>
		</div>
	);
}

export default ProjectsPage;
