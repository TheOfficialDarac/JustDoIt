import { Link } from "react-router-dom";
import { ProjectResponse } from "../../types/Types";
import ProjectMembers from "./ProjectMembers";

interface Props {
	project: ProjectResponse;
}

export default function SelectedProject({ project }: Readonly<Props>) {
	return (
		<div className='p-3 border w-full flex flex-col'>
			<div className='border'>
				<h3>{project?.title}</h3>
				<p>{project?.description}</p>
			</div>
			<div>
				<Link to={`/tasks/${project?.id}`}>See tasks?</Link>
			</div>
			<div>
				can edit members if admin & send invite
				<ProjectMembers projectId={project?.id} />
			</div>
		</div>
	);
}
