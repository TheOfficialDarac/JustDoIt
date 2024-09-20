import { Input } from "@nextui-org/react";
import React, { SyntheticEvent, useState } from "react";

const Register = () => {
	const [email, setEmail] = useState<string>("");
	const [password, setPassword] = useState<string>("");
	const [repeatPassword, setRepeatPassword] = useState<string>("");
	const [message, setMessage] = useState<string>("");

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

			await fetch("/api/auth/register", {
				method: "POST",
				mode: "cors",
				headers: {
					"Content-Type": "application/json",
				},
				body: JSON.stringify({
					email: email,
					password: password,
				}),
			})
				.then(async (response) => {
					if (response.ok) {
						const json = await response.json();
						if (json.result.isSuccess) {
							setMessage(() => "Registration successful. Please Login");
						}
						// Optionally navigate or show a success message
					}
				})
				.catch((message) => {
					console.error("Error upon registration:", message);
					setMessage(() => "Error: Registration Error.");
				});
		},
		[email, password, repeatPassword]
	);

	return (
		<div className='w-full'>
			<h3 className='text-center p-2 m-2'>Register</h3>
			<form
				className='flex flex-col'
				onSubmit={(e) => {
					e.preventDefault();
					handleSubmit(e);
				}}
			>
				<div className='flex mx-auto'>
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

				<div className='flex gap-2'>
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
			<div>{message}</div>
		</div>
	);
};

export default Register;
