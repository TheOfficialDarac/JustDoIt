import { ReactNode, useState } from "react";
import { useAuth } from "../../hooks/useAuth";
import { useNavigate } from "react-router-dom";
import {
	Button,
	Checkbox,
	Input,
	Link,
	// Modal,
	// ModalBody,
	// ModalContent,
	// Spinner,
	useDisclosure,
} from "@nextui-org/react";
import { EnvelopeIcon, LockClosedIcon } from "@heroicons/react/16/solid";
import LoadingSpinner from "../../components/layout/LoadingSpinner";

export const LoginPage = () => {
	const navigate = useNavigate();
	const { isOpen, onOpen, onClose } = useDisclosure();
	const { login } = useAuth();

	// state variables for email and passwords
	const [email, setEmail] = useState<string>("");
	const [password, setPassword] = useState<string>("");
	const [rememberme, setRememberme] = useState<boolean>(false);

	// state variable for error messages
	const [message, setMessage] = useState<ReactNode>(<li></li>);

	// handle submit event for the form
	const handleSubmit = async (e: React.SyntheticEvent) => {
		e.preventDefault();
		onOpen();

		// clear error message
		setMessage(() => <li></li>);

		if (!email || !password) {
			setMessage(() => <li>Please fill fields.</li>);
			return;
		}

		// post data to the /register api

		await fetch("/api/v1/auth/login", {
			method: "POST",
			mode: "cors", // no-cors, *cors, same-origin
			headers: {
				"Content-Type": "application/json",
			},
			body: JSON.stringify({
				email: email,
				password: password,
				rememberme: rememberme,
			}),
		})
			.then(async (response) => {
				// handle success or error from the server
				const json = await response.json();

				if (response.ok) {
					setMessage(() => <li>Successful Login.</li>);
					await login(json.data.token, rememberme);
					// .then(() => navigate("/"));
				}

				setMessage(() =>
					json.result.errors.map(
						(element: { code: string; description: string }) => (
							<li key={element.code}>{element.description}</li>
						)
					)
				);
			})
			.catch((error) => {
				// handle network error
				console.error(error);
				setMessage(() => <li>Error: Error Logging in.</li>);
			})
			.finally(onClose);
	};
	return (
		<>
			<div className='mt-6 max-w-2xl mx-auto'>
				<form
					className='flex flex-col items-center p-4 gap-4 border-neutral-300 border-solid rounded-xl w-full border-2 shadow-sm'
					onSubmit={handleSubmit}
				>
					<Input
						autoFocus
						endContent={
							<EnvelopeIcon
								className='w-6 text-2xl text-default-400 pointer-events-none'
								// role='presentation'
							/>
						}
						label='Email'
						variant='bordered'
						//
						value={email}
						type='email'
						onValueChange={setEmail}
						// className="max-w-xs"
					/>
					<Input
						endContent={
							<LockClosedIcon
								className='w-6 text-2xl text-default-400 pointer-events-none'
								// role='presentation'
							/>
						}
						label='Password'
						type='password'
						variant='bordered'
						value={password}
						onValueChange={setPassword}
					/>
					<Checkbox
						defaultChecked={rememberme}
						onValueChange={() => setRememberme((prev) => !prev)}
					>
						Remember me
					</Checkbox>
					<div className='flex py-2 px-1 justify-around gap-2'>
						<Link
							color='primary'
							className='cursor-pointer'
							size='sm'
							onClick={() => navigate("/register")}
						>
							Create Account
						</Link>
					</div>
					<div>
						<Button
							color='primary'
							className='flex-1'
							type='submit'
						>
							{isOpen ? "Logging in..." : "Login"}
						</Button>
					</div>
				</form>
				<ul className='error p-2'>{message}</ul>
			</div>
			<LoadingSpinner
				isOpen={isOpen}
				onClose={onClose}
				onOpen={onOpen}
			/>
		</>
	);
};
