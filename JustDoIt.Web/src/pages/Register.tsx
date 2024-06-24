import { Avatar, Input } from "@nextui-org/react";
import React, { useRef } from "react";

const Register = () => {
  const [email, setEmail] = React.useState("");
  const [username, setUsername] = React.useState("");
  const [password, setPassword] = React.useState("");
  const [firstName, setFirstName] = React.useState("");
  const [lastName, setLastName] = React.useState("");
  const [phoneNum, setPhoneNum] = React.useState("");
  const [profilePic, setProfilePic] = React.useState("");
  const [isValidProfileImage, setIsValidProfileImage] = React.useState(false);

  const hiddenFileInput = useRef(null);
  //#region Validation

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

  const validateFirstName = React.useMemo(() => {
    if (firstName === "") return false;

    //! TODO add Name validations
    return true;
  }, [firstName]);

  const validateLastName = React.useMemo(() => {
    if (lastName === "") return false;

    //! TODO add Name validations
    return true;
  }, [lastName]);

  const validatePassword = React.useMemo(() => {
    if (password === "") return false;

    //! TODO add password validations
    return true;
  }, [password]);

  const validatePhoneNum = React.useMemo(() => {
    if (phoneNum === "") return false;

    //! TODO add password validations
    return true;
  }, [phoneNum]);

  const validateProfilePic = React.useMemo(() => {
    if (profilePic === "") return false;

    setIsValidProfileImage(true);
    //! TODO add password validations
    return true;
  }, [profilePic]);

  //#endregion Validation

  return (
    <>
      <h3 className="text-center p-2 m-2">Register</h3>
      <form
        className="flex flex-col items-center p-4 gap-4 border border-neutral-300 border-solid rounded-xl w-full"
        onSubmit={() => {
          alert("Has been submitted");
        }}
      >
        <Avatar
          src={""}
          name=""
          className="w-10 h-10 cursor-pointer"
          onClick={() => {
            hiddenFileInput.current.click();
          }}
          data-hover="border-gray-400 border border-2 border-blue-300"
        />
        {/* <fieldset className="w-100">
          <legend>Do you agree to the terms?</legend> */}
        <Input
          value={email}
          type="email"
          label="Email"
          variant="bordered"
          isInvalid={isInvalid}
          color={isInvalid ? "danger" : "default"}
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
          color={validateUsername ? "danger" : "default"}
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
          color={validatePassword ? "danger" : "default"}
          errorMessage="Please enter a valid username"
          onValueChange={setPassword}
          className="max-w-xs"
        />

        <Input
          value={firstName}
          type="text"
          label="First Name"
          variant="bordered"
          isInvalid={validateFirstName}
          color={validateFirstName ? "danger" : "default"}
          errorMessage="Please enter a valid First Name"
          onValueChange={setFirstName}
          className="max-w-xs"
        />

        <Input
          value={lastName}
          type="text"
          label="Last Name"
          variant="bordered"
          isInvalid={validateLastName}
          color={validateLastName ? "danger" : "default"}
          errorMessage="Please enter a valid Last Name"
          onValueChange={setLastName}
          className="max-w-xs"
        />

        <Input
          value={phoneNum}
          type="tel"
          label="Phone Number"
          variant="bordered"
          isInvalid={validatePhoneNum}
          color={validatePhoneNum ? "danger" : "default"}
          errorMessage="Please enter a valid Phone Number"
          onValueChange={setPhoneNum}
          className="max-w-xs"
        />

        <input
          type="file"
          accept="image/*"
          style={{ display: "none" }}
          // onChangeCapture={}
          // onChange={setProfilePic}
          ref={hiddenFileInput}
          name="profilePicture"
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
        {/* </fieldset> */}
        <button
          type="submit"
          className="border border-2 border-solid rounded-xl border-sky-400 p-3 text-foreground-500 hover:text-sky-400"
        >
          Register
        </button>
      </form>
    </>
  );
};

export default Register;
