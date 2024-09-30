import {
	Dispatch,
	SetStateAction,
	useCallback,
	useEffect,
	useState,
} from "react";
import { ReactFilesPreview } from "react-files-preview";
import { TaskAttachmentResponse } from "../../helpers/Types";

interface Props {
	taskId: number;
	canEdit: boolean;
	authToken: string;
	isOpen: boolean;
	setAttachmentsInParent: React.Dispatch<SetStateAction<File[]>>;
}
const TaskAttachments = ({
	taskId,
	canEdit,
	authToken,
	isOpen,
	setAttachmentsInParent,
}: Props) => {
	const [attachments, setAttachments] = useState<File[]>([]);

	const getAttachmentsAsFiles = useCallback(
		async (attachments: TaskAttachmentResponse[]) => {
			attachments.forEach(async (attachment) => {
				const url: string = `/api/v1/attachments/get-file?filepath=${attachment.filepath}`;
				await fetch(url, {
					method: "GET",
				}).then(async (response) => {
					await response.blob().then((blob) => {
						setAttachments((prev) => [
							...prev,
							new File(
								[blob],
								`${attachment?.id}.${attachment?.filepath
									.split(/[#?]/)[0]
									.split(".")
									.pop()
									.trim()}`,
								{ type: blob.type }
							),
						]);
					});
				});
			});
		},
		[]
	);
	const getTaskAttachments = useCallback(async () => {
		fetch(`/api/v1/attachments/tasks?Id=${taskId}`, {
			method: "GET",
			headers: {
				"Content-Type": "application/json",
				Authorization: `Bearer ${authToken}`,
			},
		})
			.then(async (response) => {
				const json = await response.json();
				if (response.ok) {
					if (json.result.isSuccess) {
						await getAttachmentsAsFiles(json.data);
					}
				}
			})
			.catch((error) => console.error(error));
	}, [authToken, getAttachmentsAsFiles, taskId]);

	useEffect(() => {
		if (isOpen) {
			const thatDIV = document.querySelector("div.flex.flex-row.max-h-2");
			thatDIV?.classList.remove("max-h-2");

			getTaskAttachments();
		}
	}, [getTaskAttachments, isOpen]);

	return (
		<div>
			<ReactFilesPreview
				accept='image/*,.pdf'
				allowEditing={false}
				files={attachments}
				getFiles={(files) => setAttachmentsInParent(() => files)}
				downloadFile={true}
				// onChange
				// onClick
				// onDrop
				// onRemove
				// maxFileSize
				// maxFiles
				disabled={canEdit}
				// fileHeight
				// fileWidth
				// width
				// height='full'
				multiple
				showFileSize
				// removeFile
			/>
		</div>
	);
};

export default TaskAttachments;
