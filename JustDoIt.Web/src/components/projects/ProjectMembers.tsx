interface Props {
	projectId: number;
}

const ProjectMembers = ({ projectId }: Props) => {
	return (
		<div className='border p-2'>
			ProjectMembers of project with id {projectId}
		</div>
	);
};

export default ProjectMembers;
