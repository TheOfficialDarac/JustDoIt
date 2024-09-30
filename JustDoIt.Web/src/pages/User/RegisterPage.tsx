import { Divider, Input, useDisclosure } from "@nextui-org/react";
import React, { SyntheticEvent, useCallback, useState } from "react";
import LoadingSpinner from "../../components/layout/LoadingSpinner";

const Register = () => {
	const [email, setEmail] = useState<string>("");
	const [password, setPassword] = useState<string>("");
	const [repeatPassword, setRepeatPassword] = useState<string>("");
	const [message, setMessage] = useState<string>("");

	const { isOpen, onOpen, onClose } = useDisclosure();

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

	const handleSubmit = useCallback(
		async (e: SyntheticEvent) => {
			e.preventDefault();

			onOpen();
			await fetch("/api/v1/auth/register", {
				method: "POST",
				headers: {
					"Content-Type": "application/json",
				},
				body: JSON.stringify({
					email: email,
					password: password,
				}),
			})
				.then(async (response) => {
					console.log("register response: ", response);
					if (response.ok) {
						setMessage(() => "Registration successful. Please Login");
						// Optionally navigate or show a success message
					} else {
						// const json = await response.json();
						setMessage(() => "Registration failed. Please try again.");
					}
				})
				.catch((message) => {
					console.error("Error upon registration:", message);
					setMessage(() => "Error: Registration Error.");
				});
			onClose();
		},
		[email, password]
	);

	return (
		<div className='w-full flex flex-col'>
			<div>
				<h3 className='text-center p-2 m-2 text-xl'>Register</h3>
			</div>
			<form
				className='flex flex-col w-full max-w-prose mx-auto'
				onSubmit={(e) => {
					e.preventDefault();
					handleSubmit(e);
				}}
			>
				<div className=''>
					<Input
						value={email}
						type='email'
						label='Email'
						variant='bordered'
						isInvalid={isInvalid}
						color={isInvalid ? "danger" : "default"}
						errorMessage='Please enter a valid email'
						onValueChange={setEmail}
						className=''
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
						isRequired
					/>
				</div>
				<div className='grid justify-items-end pt-3'>
					<button
						type='submit'
						className='border-2 border-solid rounded-xl border-gray-200 p-4 text-foreground-500 hover:text-sky-400 text-small hover:border-sky-400'
					>
						Register
					</button>
				</div>
				<div className='text-center p-3'>
					<Divider className='my-3' />
					<p className='py-2'>{message}</p>
				</div>
			</form>
			<LoadingSpinner
				spinnerIsShown={isOpen}
				closeSpinner={onClose}
				showSpinner={onOpen}
			/>
		</div>
	);
};

export default Register;
