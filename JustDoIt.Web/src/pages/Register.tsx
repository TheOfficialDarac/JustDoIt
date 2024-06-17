import { Input } from "@nextui-org/react";
import React from "react";

const Register = () => {
  const [email, setEmail] = React.useState("");
  const [username, setUsername] = React.useState("");
  const [password, setPassword] = React.useState("");

  const validateEmail = (email: string) =>
    email.match(/^[A-Z0-9._%+-]+@[A-Z0-9.-]+.[A-Z]{2,4}$/i);

  const isInvalid = React.useMemo(() => {
    if (email === "") return false;

    return validateEmail(email) ? false : true;
  }, [email]);

  const validateUsername = React.useMemo(() => {
    if (username === "") return false;

    //! TODO add username validations
    return true;
  }, [username]);

  const validatePassword = React.useMemo(() => {
    if (password === "") return false;

    //! TODO add password validations
    return true;
  }, [password]);

  return (
    <form className="p-2 m-auto flex-1">
      <Input
        value={email}
        type="email"
        label="Email"
        variant="bordered"
        isInvalid={isInvalid}
        color={isInvalid ? "danger" : "success"}
        errorMessage="Please enter a valid email"
        onValueChange={setEmail}
        className="max-w-xs"
      />

      <Input
        value={username}
        type="text"
        label="Username"
        variant="bordered"
        isInvalid={validateUsername}
        color={validateUsername ? "danger" : "success"}
        errorMessage="Please enter a valid username"
        onValueChange={setUsername}
        className="max-w-xs"
      />

      <Input
        value={password}
        type="text"
        label="Password"
        variant="bordered"
        isInvalid={validatePassword}
        color={validatePassword ? "danger" : "success"}
        errorMessage="Please enter a valid username"
        onValueChange={setPassword}
        className="max-w-xs"
      />
    </form>
  );
};

export default Register;
