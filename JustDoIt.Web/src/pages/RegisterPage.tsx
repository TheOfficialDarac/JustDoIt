import { Avatar, Input } from "@nextui-org/react";
import React, { useRef, useState } from "react";

const Register = () => {
	const [email, setEmail] = useState<string>("");
	const [password, setPassword] = useState<string>("");
	const [repeatPassword, setRepeatPassword] = useState<string>("");
	const [error, setError] = useState<string>("");

	//#region Validation

	const validateEmail = (email: string) =>
		email.match(/^[A-Z0-9._%+-]+@[A-Z0-9.-]+.[A-Z]{2,4}$/i);

	const isInvalid = React.useMemo(() => {
		// if (email === "") return false;

		return validateEmail(email) ? false : true;
	}, [email]);

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

	//#endregion Validation

	//#region register

	//#endregiion register

	const handleSubmit = async (e) => {
		e.preventDefault();
		const updatedTask = {
			email: email,
			password: password,
			confirmPassword: repeatPassword,
		};
		// console.log("final user:", updatedTask);
		// return;

		await fetch("/api/auth/register", {
			method: "POST",
			mode: "cors",
			headers: {
				"Content-Type": "application/json",
			},
			body: JSON.stringify(updatedTask),
		})
			.then((response) => {
				if (response.ok) {
					setError(() => "Registration successful. Please Login");
					// Optionally navigate or show a success message
				}
			})
			.catch((error) => {
				console.error("Error upon registrating:", error);
				setError(() => "Error: Registration Error.");
			});
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
				<div className='flex gap-3'>
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

				<div className=''>
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
				<button
					type='submit'
					className='border-2 border-solid rounded-xl border-gray-200 p-4 text-foreground-500 hover:text-sky-400 text-small hover:border-sky-400'
				>
					Register
				</button>
			</form>
			<div>{error}</div>
		</>
	);
};

export default Register;
