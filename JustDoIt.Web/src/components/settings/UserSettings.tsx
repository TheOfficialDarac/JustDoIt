import { Button, Image, Input, useDisclosure } from "@nextui-org/react";
import { SyntheticEvent, useCallback, useRef, useState } from "react";
import LoadingSpinner from "../layout/LoadingSpinner";
import { firebaseApp } from "../../Firebase";
import { getDownloadURL, getStorage, ref, uploadBytes } from "firebase/storage";
import { AuthResponse, parseJwt } from "../../types/Types";

interface Props {
	user: AuthResponse;
	authToken: string;
	fetchUserData: () => void;
}

const UserSettings = ({ user, authToken, fetchUserData }: Props) => {
	const { isOpen, onOpen, onClose } = useDisclosure();

	const storage = getStorage(firebaseApp);
	const [image, setImage] = useState<string>(user?.pictureUrl);
	const imageInputRef = useRef<HTMLInputElement>(null);
	const [isEditing, setIsEditing] = useState<boolean>(false);

	const handleImageInputClick = useCallback(
		() => imageInputRef?.current.click(),
		[]
	);

	const handleChangeImage = useCallback(() => {
		const selectedFile = imageInputRef.current.files[0];

		if (selectedFile) {
			const reader = new FileReader();

			reader.onload = function (e) {
				setImage(() => e.target.result);
			};

			reader.readAsDataURL(selectedFile); // Read the file as a data URL
		}
	}, [setImage]);

	const handleFirebaseImageUpload = useCallback(async () => {
		let url = `gs://task-manager-just-do-it.appspot.com/profile-photos/${
			parseJwt(authToken).sub
		}.jpeg`;
		const storageRef = ref(storage, url);
		await uploadBytes(storageRef, imageInputRef.current.files[0]).then(
			async (snapshot) => {
				// console.log("SNAPSHOT: ", snapshot.ref);
				await getDownloadURL(snapshot.ref).then((downloadURL) => {
					// console.log("download URL: ", downloadURL);
					setImage(() => downloadURL);
					// console.log("Image: ", image);
				});
			}
		);
	}, [storage, authToken]);

	const handleFormSubmit = useCallback(
		async (e: SyntheticEvent) => {
			e.preventDefault();

			onOpen();
			if (image != user?.pictureUrl) {
				handleFirebaseImageUpload();
			}

			const formData: FormData = new FormData(e.target);
			formData.set("pictureUrl", image);

			// console.log(JSON.stringify(Object.fromEntries(formData.entries())));
			// return;

			await fetch("/api/v1/auth/update", {
				method: "PUT",
				body: JSON.stringify(Object.fromEntries(formData.entries())),
				headers: {
					"Content-Type": "application/json",
					Authorization: `Bearer ${authToken}`,
				},
			})
				.then(async (response) => {
					// console.log(response);
					if (response.ok) {
						const json = await response.json();
						if (json.result.isSuccess) {
							console.log("change success.");
							fetchUserData();
						} else {
							console.log("change failure.");
						}
					}
				})
				.finally(onClose);
			setIsEditing((prev) => !prev);
		},
		[
			onOpen,
			image,
			user?.pictureUrl,
			authToken,
			onClose,
			handleFirebaseImageUpload,
			fetchUserData,
		]
	);

	return (
		<div className='border p-2 flex flex-col'>
			{/* <h2>User Settings</h2> */}
			<form
				action='PUT'
				id='user-data-form'
				onSubmit={handleFormSubmit}
				className='flex flex-col gap-3 px-2'
			>
				<input
					type='file'
					name='pictureUrl'
					className='hidden'
					accept='image/apng, image/bmp, image/gif, image/jpeg, image/pjpeg, image/png, image/svg+xml, image/tiff, image/webp,image/x-icon'
					ref={imageInputRef}
					onChange={handleChangeImage}
				/>
				<Image
					removeWrapper
					className={"mx-auto w-48 " + (isEditing && " cursor-pointer")}
					isZoomed={isEditing}
					src={image}
					onClick={() => isEditing && handleImageInputClick()}
					radius='lg'
					loading='lazy'
					shadow='sm'
				/>
				<Input
					name='userName'
					isDisabled={!isEditing}
					type='text'
					label='Username'
					defaultValue={user?.userName}
				/>
				<div className='flex gap-2'>
					<Input
						name='lastName'
						isDisabled={!isEditing}
						type='text'
						label='Last Name'
						defaultValue={user?.lastName}
					/>
					<Input
						name='firstName'
						isDisabled={!isEditing}
						type='text'
						label='First Name'
						defaultValue={user?.firstName}
					/>
				</div>
				<div className='flex gap-2'>
					<Input
						name='phoneNumber'
						isDisabled={!isEditing}
						type='tel'
						label='Phone Number'
						defaultValue={user?.phoneNumber}
					/>
					<Input
						name='email'
						isDisabled={true}
						type='email'
						label='Email'
						defaultValue={user?.email}
					/>
				</div>
				<div className='flex gap-2'>
					<Button
						type='button'
						onClick={(e) => {
							if (isEditing) {
								setImage(() => user?.pictureUrl);
								e.currentTarget.form?.reset();
							}
							setIsEditing((prev) => !prev);
						}}
					>
						{isEditing ? "Cancel" : "Edit"}
					</Button>
					{isEditing && <Button type='submit'>Submit</Button>}
				</div>
			</form>
			<LoadingSpinner
				onClose={onClose}
				isOpen={isOpen}
				onOpen={onOpen}
			/>
		</div>
	);
};

export default UserSettings;
