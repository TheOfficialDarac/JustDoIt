import { ProjectResponse } from "../../types/Types";

interface Props {
	project: ProjectResponse;
}

export default function SelectedProject({ project }: Readonly<Props>) {
	return <div>{project?.title}</div>;
}
