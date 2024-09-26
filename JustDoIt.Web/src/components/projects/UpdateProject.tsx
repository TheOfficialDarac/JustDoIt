import { Cog6ToothIcon } from "@heroicons/react/16/solid";
import {
	useDisclosure,
	Button,
	Modal,
	ModalContent,
	ModalHeader,
	ModalBody,
	ModalFooter,
	Input,
	Textarea,
} from "@nextui-org/react";
import { ProjectResponse } from "../../types/Types";
import { ReactFilesPreview } from "react-files-preview";
import { firebaseApp } from "../../Firebase";
import {
	getBytes,
	getMetadata,
	getStorage,
	listAll,
	ref,
} from "firebase/storage";
import { useCallback, useEffect, useState } from "react";

type Props = {
	project: ProjectResponse;
};

const UpdateProject = ({ project }: Props) => {
	const { isOpen, onOpen, onOpenChange } = useDisclosure();
	const [picture, setPicture] = useState<File>(null);
	const storage = getStorage(firebaseApp);

	const getPicture = useCallback(async () => {
		fetch(project?.pictureUrl, {
			method: "GET",
		}).then((response) => console.log(response));
		// const pictureRef = ref(storage, `/project-photos/${project?.id}.jpeg`);
		// getMetadata(pictureRef).then(
		// 	(metadata) => {
		// 		console.log("metadata: ", metadata);
		// 	}
		// 		// const file = new File();
		// );
		// getBytes(pictureRef).then((res) => console.log("BYTES: ", res));
	}, [project?.id, storage]);

	useEffect(() => {
		if (isOpen) {
			getPicture();
			const thatDIV = document.querySelector("div.flex.flex-row.max-h-2");
			thatDIV?.classList.remove("max-h-2");
		}
	}, [getPicture, isOpen]);

	return (
		<>
			<button onClick={onOpen}>
				<Cog6ToothIcon className='size-6' />
			</button>
			<Modal
				isOpen={isOpen}
				onOpenChange={onOpenChange}
				size='lg'
			>
				<ModalContent>
					{(onClose) => (
						<form>
							<ModalHeader className='flex flex-col gap-1'>
								Modal Title
							</ModalHeader>
							<ModalBody>
								<Input
									type='text'
									label='Title'
									name='title'
									defaultValue={project?.title}
								/>
								{/* <Input type="text" label="Description" name="description" /> */}
								<Textarea
									label='Description'
									placeholder='Enter your description'
									className=''
									name='description'
									defaultValue={project?.description}
								/>
								<ReactFilesPreview
									accept='image/*'
									allowEditing={false}
									downloadFile={false}
									files={[]}
									getFiles={(files) => setPicture(files[0])}
									// onChange={}
									// onClick={() => {}}
									// onDrop
									// onRemove
									// maxFileSize
									maxFiles={1}
									// disabled
									// fileHeight
									// fileWidth
									// width
									multiple={false}
									showFileSize
									// removeFile
								/>
							</ModalBody>
							<ModalFooter>
								<Button
									color='danger'
									variant='light'
									onPress={onClose}
								>
									Close
								</Button>
								<Button
									color='primary'
									onPress={onClose}
								>
									Action
								</Button>
							</ModalFooter>
						</form>
					)}
				</ModalContent>
			</Modal>
		</>
	);
};

export default UpdateProject;
