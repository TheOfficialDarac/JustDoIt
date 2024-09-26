import { PlusIcon } from "@heroicons/react/16/solid";
import {
	Modal,
	ModalContent,
	ModalHeader,
	ModalBody,
	ModalFooter,
	Button,
	useDisclosure,
	Divider,
	Input,
	Textarea,
} from "@nextui-org/react";
import { SyntheticEvent, useCallback, useEffect, useState } from "react";
import { ReactFilesPreview } from "react-files-preview";
import { getStorage, ref, uploadBytes, getDownloadURL } from "firebase/storage";
import { firebaseApp } from "../../Firebase";

interface Props {
	authToken: string;
}

const CreateProjectModal = ({ authToken }: Props) => {
	const { isOpen, onOpen, onOpenChange, onClose } = useDisclosure();
	// const [image, setImage] = useState<string>(null);
	const [picture, setPicture] = useState<File>(null);
	const storage = getStorage(firebaseApp);

	const updatePicture = useCallback(
		async (projectId: number, url: string) => {
			await fetch("/api/v1/projects/update", {
				method: "PUT",
				headers: {
					"Content-Type": "application/json",
					Authorization: `Bearer ${authToken}`,
				},
				body: JSON.stringify({
					id: projectId,
					pictureUrl: url,
				}),
			})
				.then(async (response) => {
					console.log("update project response: ", response);
					const json = await response.json();
					console.log(json);
					if (response.ok) {
						if (json.result.isSuccess) {
							// handleFirebaseImageUpload(json.data.id);
						}
					} else {
						console.warn(json.result);
					}
				})
				.catch((error) => console.error(error));
		},
		[authToken]
	);

	const handleFirebaseImageUpload = useCallback(
		async (projectId: number) => {
			const url = `gs://task-manager-just-do-it.appspot.com/project-photos/${projectId}.jpeg`;

			const storageRef = ref(storage, url);
			await uploadBytes(storageRef, picture).then(async (snapshot) => {
				// console.log("SNAPSHOT: ", snapshot.ref);
				await getDownloadURL(snapshot.ref).then((downloadURL) => {
					console.log("download URL: ", downloadURL);
					updatePicture(projectId, downloadURL);
				});
			});
		},
		[storage, picture, updatePicture]
	);

	const handleSubmit = async (e: SyntheticEvent) => {
		e.preventDefault();
		const formData = new FormData(e.target);

		// formData.set("pictureUrl", image);
		console.log("submitted data: ", formData);

		await fetch("/api/v1/projects/create", {
			method: "POST",
			headers: {
				"Content-Type": "application/json",
				Authorization: `Bearer ${authToken}`,
			},
			body: JSON.stringify(Object.fromEntries(formData.entries())),
		})
			.then(async (response) => {
				console.log("create project response", response);
				const json = await response.json();
				console.log(json);
				if (response.ok) {
					if (json.result.isSuccess) {
						if (picture) handleFirebaseImageUpload(json.data.id);
					}
				} else {
					console.warn(json.result);
				}
			})
			.catch((error) => console.error(error));

		onClose();
	};

	useEffect(() => {
		if (isOpen) {
			const thatDIV = document.querySelector("div.flex.flex-row.max-h-2");
			thatDIV?.classList.remove("max-h-2");
		}
	}, [isOpen]);

	return (
		<>
			<Button onPress={onOpen}>
				<PlusIcon className='size-6' />
			</Button>
			<Modal
				isOpen={isOpen}
				onOpenChange={onOpenChange}
				size='xl'
			>
				<ModalContent>
					{(onClose) => (
						<form
							onSubmit={handleSubmit}
							action='POST'
						>
							<ModalHeader className='flex flex-col gap-1'>
								Create Project
							</ModalHeader>
							<Divider />
							<ModalBody>
								<Input
									type='text'
									label='Title'
									name='title'
								/>
								{/* <Input type="text" label="Description" name="description" /> */}
								<Textarea
									label='Description'
									placeholder='Enter your description'
									className=''
									name='description'
								/>
								<ReactFilesPreview
									accept='image/*'
									allowEditing={false}
									downloadFile={false}
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
							<Divider />
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
									type='submit'
								>
									Create
								</Button>
							</ModalFooter>
						</form>
					)}
				</ModalContent>
			</Modal>
		</>
	);
};

export default CreateProjectModal;
