import { useEffect, useState } from "react";
import { ReactFilesPreview } from "react-files-preview";
import { TaskAttachmentResponse } from "../../types/Types";
import { getDownloadURL, getStorage, ref } from "firebase/storage";
import { firebaseApp } from "../../Firebase";

interface Props {
	taskId: number;
	roleName: string;
	authToken: string;
}
const TaskAttachments = ({ taskId, roleName, authToken }: Props) => {
	const [attachments, setAttachments] = useState<File[]>([]);
	const storage = getStorage(firebaseApp);

	useEffect(() => {
		const thatDIV = document.querySelector("div.flex.flex-row.max-h-2");
		thatDIV?.classList.remove("max-h-2");

		const getTaskAttachments = async () => {
			fetch(`/api/v1/tasks/attachments?TaskId=${taskId}`, {
				method: "GET",
				headers: {
					"Content-Type": "application/json",
					Authorization: `Bearer ${authToken}`,
				},
			})
				.then(async (response) => {
					if (response.ok) {
						// const json = await response.json();
						// console.log(json);
						// if (json.result.isSuccess) {
						// 	const files: File[] = [];
						// 	json.data.forEach(async (element: TaskAttachmentResponse) => {
						// 		const storageRef = ref(storage, element.filepath);
						// 		await getDownloadURL(storageRef).then(async (response) => {
						// 			console.log("Rres: ", response);
						// 			await fetch(response, {
						// 				headers: {
						// 					"Access-Control-Allow-Origin": "*",
						// 				},
						// 			}).then((res) => console.log("RES2: ", res));
						// 		});
						// 	});
						// 	setAttachments(() => files);
						// }
					}
					// console.log(response);
				})
				.catch((error) => console.error(error));
		};
		getTaskAttachments();
	}, [taskId]);
	return (
		<div>
			I can edit this
			<ReactFilesPreview
				accept='image/*,.pdf'
				allowEditing={true}
				files={attachments}
				downloadFile={true}
				// getFiles={(files) => console.log(files)}
				// onChange
				// onClick
				// onDrop
				// onRemove
				// maxFileSize
				// maxFiles
				// disabled
				// fileHeight
				// fileWidth
				// width
				// height='full'
				// multiple
				// showFileSize
				// removeFile
			/>
		</div>
	);
};

export default TaskAttachments;
