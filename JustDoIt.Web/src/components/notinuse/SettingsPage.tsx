import {
	Avatar,
	Button,
	Input,
	Modal,
	ModalBody,
	ModalContent,
	ModalFooter,
	ModalHeader,
	useDisclosure,
} from "@nextui-org/react";
import { usePreferences } from "../../hooks/usePreferences";
import { useAuth } from "../../hooks/useAuth";
import { useEffect, useMemo, useRef } from "react";
import React from "react";
import DynamicModal from "../../components/DynamicModel";

const SettingsPage = () => {
	const { changeTheme } = usePreferences();
	const { isOpen, onOpen, onOpenChange } = useDisclosure();
	const { user } = useAuth();

	const toggleTheme = () => {
		changeTheme();
	};

	useEffect(() => {
		console.log("user", user);
	});

	const [email, setEmail] = React.useState("");
	const [username, setUsername] = React.useState("");
	const [password, setPassword] = React.useState("");
	const [repeatPassword, setRepeatPassword] = React.useState("");
	const [firstName, setFirstName] = React.useState("");
	const [lastName, setLastName] = React.useState("");
	const [phoneNum, setPhoneNum] = React.useState("");
	const [profilePic, setProfilePic] = React.useState("");
	// const [isValidProfileImage, setIsValidProfileImage] = React.useState(false);

	const hiddenFileInput = useRef(null);

	//#region Validation

	const validateEmail = (email: string) =>
		email.match(/^[A-Z0-9._%+-]+@[A-Z0-9.-]+.[A-Z]{2,4}$/i);

	const isInvalid = useMemo(() => {
		// if (email === "") return false;

		return validateEmail(email) ? false : true;
	}, [email]);

	const validateUsername = useMemo(() => {
		if (username !== "") return false;

		//! TODO add username validations
		return true;
	}, [username]);

	const validateFirstName = useMemo(() => {
		return false;

		return firstName.match("([a-zA-Z]{3,30}\\s*)+") ? false : true;
	}, [firstName]);

	const validateLastName = useMemo(() => {
		return false;

		return lastName.match("[a-zA-Z]{3,30}") ? false : true;
	}, [lastName]);

	const validatePassword = useMemo(() => {
		return false;

		// return repeatPassword.match(
		//   "^(?=.*[a-z])(?=.*[A-Z])(?=.*d)(?=.*[@$!%*?&])[A-Za-zd@$!%*?&]{8,}$"
		// )
		//   ? false
		//   : true;
	}, [password]);

	const validateRepeatPassword = useMemo(() => {
		if (password === repeatPassword) return false;

		// return repeatPassword.match(
		//   "^(?=.*[a-z])(?=.*[A-Z])(?=.*d)(?=.*[@$!%*?&])[A-Za-zd@$!%*?&]{8,}$"
		// )
		//   ? false
		//   : true;
	}, [repeatPassword, password]);

	const validatePhoneNum = useMemo(() => {
		// if (phoneNum === "") return false;

		return phoneNum.match(
			"^(\\+\\d{1,2}\\s)?\\(?\\d{3}\\)?[\\s.-]\\d{3}[\\s.-]\\d{4}$"
		)
			? false
			: true;
	}, [phoneNum]);

	// const validateProfilePic = React.useMemo(() => {
	//   if (profilePic === "") return false;

	//   setIsValidProfileImage(true);
	//   //! TODO add password validations
	//   return true;
	// }, [profilePic]);

	//#endregion Validation

	//#region register

	//#endregiion register

	const handlePictureChange = (e) => {
		if (e.target.files && e.target.files[0]) {
			// console.log("imageURL", URL.createObjectURL(e.target.files[0]));
			setProfilePic(URL.createObjectURL(e.target.files[0]));
		}
	};

	const handleSubmit = async (e) => {
		e.preventDefault();
		const updatedTask = {
			email: email,
			firstName: firstName,
			lastName: lastName,
			userName: username,
			phoneNumber: phoneNum,
			password: password,
			confirmPassword: repeatPassword,
			pictureURL: profilePic,
		};
		console.log("final user:", updatedTask);
		return;
		try {
			const response = await fetch("/api/Task/create", {
				method: "POST",
				mode: "cors",
				headers: {
					"Content-Type": "application/json",
				},
				body: JSON.stringify(updatedTask),
			});

			if (response.ok) {
				setError("");
				// Optionally navigate or show a success message
			} else {
				setError("Failed to craete task.");
			}
		} catch (error) {
			console.error("Error creating task:", error);
			setError("Error: Error creating task.");
		}
	};
	return (
		<div className='m-3 p-3 flex flex-col gap-2'>
			{/* <Button onPress={onOpen}>Profile settings</Button> */}
			{/* <DynamicModal></DynamicModal> */}
			<Button onClick={toggleTheme}>Change theme</Button>
		</div>
	);
};

export default SettingsPage;
