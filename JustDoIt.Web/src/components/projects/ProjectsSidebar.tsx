import {
	ChangeEvent,
	Dispatch,
	memo,
	SetStateAction,
	SyntheticEvent,
	useEffect,
	useState,
} from "react";
import { ProjectResponse } from "../../types/Types";
import { Input } from "@nextui-org/react";

interface Props {
	projects: ProjectResponse[];
	setSelectedIndex: Dispatch<SetStateAction<number>>;
}
const ProjectsSidebar = memo(function ({
	projects,
	setSelectedIndex,
}: Readonly<Props>) {
	const [filteredProjects, setFilteredProjects] = useState<ProjectResponse[]>(
		[]
	);

	useEffect(() => {
		console.log("we here again");
		setFilteredProjects(() => projects);
	}, [projects]);

	const handleSearch = (e) => {
		if (e.target.value) {
			const result = filteredProjects.filter((x) => {
				return x?.title
					.toLowerCase()
					.includes(e.target.value.toString().toLowerCase());
			});
			setFilteredProjects(() => result);
		} else setFilteredProjects(() => projects);
	};

	return (
		<div>
			<div className='border p-3'>
				<Input
					type='text'
					label='Project title'
					placeholder='Search by title'
					className='max-w-xs'
					onChange={handleSearch}
				/>
			</div>
			<ul className='flex flex-col gap-2 p-1'>
				{filteredProjects?.map((project, index) => (
					<li
						className='border'
						key={project.id}
					>
						<button
							className='p-2'
							onClick={() => setSelectedIndex(() => index)}
						>
							{project.title}
						</button>
					</li>
				))}
			</ul>
		</div>
	);
});
export default ProjectsSidebar;
