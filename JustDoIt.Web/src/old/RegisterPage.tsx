import { Avatar, Input } from "@nextui-org/react";
import React, { useRef, useState } from "react";

const Register = () => {
	const [email, setEmail] = useState("");
	const [username, setUsername] = useState("");
	const [password, setPassword] = useState("");
	const [repeatPassword, setRepeatPassword] = useState("");
	const [firstName, setFirstName] = useState("");
	const [lastName, setLastName] = useState("");
	const [phoneNum, setPhoneNum] = useState("");
	const [profilePic, setProfilePic] = useState("");
	// const [isValidProfileImage, setIsValidProfileImage] = React.useState(false);

	const hiddenFileInput = useRef(null);

	//#region Validation

	const validateEmail = (email: string) =>
		email.match(/^[A-Z0-9._%+-]+@[A-Z0-9.-]+.[A-Z]{2,4}$/i);

	const isInvalid = React.useMemo(() => {
		// if (email === "") return false;

		return validateEmail(email) ? false : true;
	}, [email]);

	const validateUsername = React.useMemo(() => {
		if (username !== "") return false;

		//! TODO add username validations
		return true;
	}, [username]);

	const validateFirstName = React.useMemo(() => {
		return false;

		return firstName.match("([a-zA-Z]{3,30}\\s*)+") ? false : true;
	}, [firstName]);

	const validateLastName = React.useMemo(() => {
		return false;

		return lastName.match("[a-zA-Z]{3,30}") ? false : true;
	}, [lastName]);

	const validatePassword = React.useMemo(() => {
		return false;

		// return repeatPassword.match(
		//   "^(?=.*[a-z])(?=.*[A-Z])(?=.*d)(?=.*[@$!%*?&])[A-Za-zd@$!%*?&]{8,}$"
		// )
		//   ? false
		//   : true;
	}, [password]);

	const validateRepeatPassword = React.useMemo(() => {
		if (password === repeatPassword) return false;

		// return repeatPassword.match(
		//   "^(?=.*[a-z])(?=.*[A-Z])(?=.*d)(?=.*[@$!%*?&])[A-Za-zd@$!%*?&]{8,}$"
		// )
		//   ? false
		//   : true;
	}, [repeatPassword, password]);

	const validatePhoneNum = React.useMemo(() => {
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
		<>
			<br />
			<br />
			<h3 className='text-center p-2 m-2'>Register</h3>
			<form
				className='flex flex-col items-center p-4 gap-4 border-neutral-300 border-solid rounded-xl w-full border-2 shadow-sm'
				onSubmit={(e) => {
					e.preventDefault();
					handleSubmit(e);
				}}
			>
				<Avatar
					src={profilePic}
					className='w-20 h-20 cursor-pointer'
					onClick={() => {
						hiddenFileInput.current.click();
					}}
					data-hover='border-gray-400 border border-2 border-blue-300'
				/>

				<div className='flex gap-3'>
					<Input
						value={username}
						type='text'
						label='Username'
						variant='bordered'
						isInvalid={validateUsername}
						color={validateUsername ? "danger" : "default"}
						errorMessage='Please enter a valid username'
						onValueChange={setUsername}
						className='max-w-xs'
						isRequired
					/>

					<Input
						value={email}
						type='email'
						label='Email'
						variant='bordered'
						isInvalid={isInvalid}
						color={isInvalid ? "danger" : "default"}
						errorMessage='Please enter a valid email'
						onValueChange={setEmail}
						className='max-w-xs'
						isRequired
					/>
				</div>

				<div className='flex gap-3'>
					<Input
						value={password}
						type='password'
						label='Password'
						variant='bordered'
						isInvalid={validatePassword}
						color={validatePassword ? "danger" : "default"}
						errorMessage='Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character'
						onValueChange={setPassword}
						className='max-w-xs'
						isRequired
					/>

					<Input
						value={repeatPassword}
						type='password'
						label='Repeat Password'
						variant='bordered'
						isInvalid={validateRepeatPassword}
						color={validateRepeatPassword ? "danger" : "default"}
						errorMessage='Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character'
						onValueChange={setRepeatPassword}
						className='max-w-xs'
						isRequired
					/>
				</div>
				<div className='flex gap-3'>
					<Input
						value={firstName}
						type='text'
						label='First Name'
						variant='bordered'
						isInvalid={validateFirstName}
						color={validateFirstName ? "danger" : "default"}
						errorMessage='Please enter a valid First Name'
						onValueChange={setFirstName}
						className='max-w-xs'
					/>

					<Input
						value={lastName}
						type='text'
						label='Last Name'
						variant='bordered'
						isInvalid={validateLastName}
						color={validateLastName ? "danger" : "default"}
						errorMessage='Please enter a valid Last Name'
						onValueChange={setLastName}
						className='max-w-xs'
					/>
				</div>

				<Input
					value={phoneNum}
					type='tel'
					label='Phone Number'
					variant='bordered'
					isInvalid={validatePhoneNum}
					color={validatePhoneNum ? "danger" : "default"}
					errorMessage='Please enter a valid Phone Number'
					onValueChange={setPhoneNum}
					className='max-w-xs'
				/>

				<input
					type='file'
					accept='image/*'
					style={{ display: "none" }}
					// onChangeCapture={}
					onChange={(e) => {
						handlePictureChange(e);
					}}
					ref={hiddenFileInput}
					name='profilePicture'
				/>
				{/* <button
          type="button"
          onClick={() => {
            hiddenFileInput.current.click();
          }}
          className="border-solid border border-gray-200 border-2 text-foreground-500 py-4 px-3 motion-reduce:transition-none cursor-pointer rounded-xl w-full max-w-xs text-left text-small font-normal bg-transparent hover:border-gray-400"
        > 
          Image
        </button>
         */}
				<button
					type='submit'
					className='border-2 border-solid rounded-xl border-gray-200 p-4 text-foreground-500 hover:text-sky-400 text-small hover:border-sky-400'
				>
					Register
				</button>
			</form>
		</>
	);
};

export default Register;
