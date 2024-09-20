import { Dispatch, memo, SetStateAction } from "react";
import { ProjectResponse } from "../../types/Types";

interface Props {
	projects: ProjectResponse[];
	setSelectedIndex: Dispatch<SetStateAction<number>>;
}
export default const ProjectsSidebar = memo(function ProjectsSidebar({
	projects,
	setSelectedIndex,
}: Readonly<Props>) {
	console.log("Projects: ", projects);
	return (
		<ul className='flex flex-col gap-2 p-1'>
			{projects?.map((project, index) => (
				<li
					className='border p-2 cursor-pointer'
					key={project.id}
					onClick={() => setSelectedIndex(() => index)}
				>
					{project.title}
				</li>
			))}
		</ul>
	);
}
