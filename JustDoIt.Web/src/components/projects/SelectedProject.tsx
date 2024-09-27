import { ProjectResponse, ProjectRoleResponse } from "../../types/Types";
import { useCallback, useEffect, useState } from "react";
import { Image } from "@nextui-org/react";
import TasksComponent from "../tasks/TasksComponent";
import UpdateProject from "./UpdateProject";

interface Props {
	project: ProjectResponse;
	authToken: string;
	fetchProjects: () => void;
}

export default function SelectedProject({
	project,
	authToken,
	fetchProjects,
}: Readonly<Props>) {
	const [userRole, setUserRole] = useState<ProjectRoleResponse>(null);
	const [image, setImage] = useState<string>("");

	const getRole = useCallback(async () => {
		await fetch(`/api/v1/projects/user-role?ProjectId=${project?.id}`, {
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
	}, [authToken, project?.id]);

	const getImage = useCallback(() => {
		if (project?.pictureUrl) {
			const url: string = `/api/v1/attachments/get-file?filepath=${project?.pictureUrl}`;
			fetch(url, {
				method: "GET",
			}).then(async (response) => {
				// console.log("pic: ", response);
				response.blob().then((blob) => {
					const file = new File([blob], `${project?.id}.jpg`, {
						type: blob.type,
					});
					setImage(() => URL.createObjectURL(file));
				});
			});
		}
	}, [project?.id, project?.pictureUrl]);

	useEffect(() => {
		setImage(() => "");
		getImage();
		getRole();
	}, [getImage, getRole]);

	return (
		<div className='p-3 border w-full flex flex-col'>
			<div className='border'>
				<div className='flex justify-between p-2'>
					<h3>{project?.title}</h3>
					<UpdateProject
						project={project}
						authToken={authToken}
						fetchProjects={fetchProjects}
					/>
				</div>
				<div className='flex'>
					<div className='p-2'>
						<Image
							width={120}
							alt='Project Logo image'
							src={image}
						/>
					</div>
					<div className='border rounded-md flex-1 m-2 p-3'>
						<p className=''>{project?.description}</p>
					</div>
				</div>
			</div>
			<div>
				{/* <Link className="hover:text-cyan-100" to={`/members/${project?.id}`}>
          Members
        </Link> */}
				{/* <ProjectMembers projectId={project?.id} /> */}
			</div>
			{/* <div>my role in project: {userRole?.roleName}</div> */}
			<div>
				<TasksComponent
					projectId={project?.id}
					userRole={userRole}
					authToken={authToken}
				/>
			</div>
		</div>
	);
}
