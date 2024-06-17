import { Input } from "@nextui-org/react";
import React from "react";

const Register = () => {
  const [value, setValue] = React.useState("Enter email address");

  const validateEmail = (value: string) =>
    value.match(/^[A-Z0-9._%+-]+@[A-Z0-9.-]+.[A-Z]{2,4}$/i);

  const isInvalid = React.useMemo(() => {
    if (value === "") return false;

    return validateEmail(value) ? false : true;
  }, [value]);

  return (
    <Input
      value={value}
      type="email"
      label="Email"
      variant="bordered"
      isInvalid={isInvalid}
      color={isInvalid ? "danger" : "success"}
      errorMessage="Please enter a valid email"
      onValueChange={setValue}
      className="max-w-xs"
    />
  );
};

export default Register;
