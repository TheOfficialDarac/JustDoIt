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

        <Input
          value={firstName}
          type="text"
          label="First Name"
          variant="bordered"
          isInvalid={validateFirstName}
          color={validateFirstName ? "danger" : "success"}
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
          color={validateLastName ? "danger" : "success"}
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
          color={validatePhoneNum ? "danger" : "success"}
          errorMessage="Please enter a valid Phone Number"
          onValueChange={setPhoneNum}
          className="max-w-xs"
        />

        {/* <Input
          value={profilePic}
          type="file"
          // label="Profile image"
          // variant="bordered"
          isInvalid={validateProfilePic}
          // color={validateProfilePic ? "danger" : "success"}
          // errorMessage="Please input image"
          onValueChange={setProfilePic}
          accept="image/*"
          ref={hiddenFileInput}
          style={{ display: "none" }}
          hidden={true}
        /> */}
        <input
          type="file"
          accept="image/*"
          style={{ display: "none" }}
          // onChangeCapture={}
          // onChange={setProfilePic}
          ref={hiddenFileInput}
        />
        <button
          type="button"
          onClick={() => {
            // if (hiddenFileInput) {
            hiddenFileInput.current.click();
            // }
          }}
        >
          Image
        </button>
        {isValidProfileImage ? (
          <Avatar src={""} className="w-20 h-20 text-large" />
        ) : null}
      </form>
    </>
  );
};

export default Register;
