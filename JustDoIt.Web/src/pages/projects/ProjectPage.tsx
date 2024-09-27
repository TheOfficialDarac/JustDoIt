import { useParams } from "react-router-dom";

type Props = {};

const ProjectPage = () => {
	const { projectId } = useParams();
	return (
		<div>
			ProjectPage, id: {projectId}
			<div>
				side actions{" "}
				<ul>
					<li>tasks</li>
					<li>members</li>
				</ul>
			</div>
			<div>info</div>
		</div>
	);
};

export default ProjectPage;
