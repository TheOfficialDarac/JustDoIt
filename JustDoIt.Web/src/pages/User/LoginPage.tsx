import { useState } from "react";
import { useAuth } from "../../hooks/useAuth";
import { useNavigate } from "react-router-dom";
import {
  Button,
  Checkbox,
  Input,
  Link,
  Modal,
  ModalBody,
  ModalContent,
  Spinner,
  useDisclosure,
} from "@nextui-org/react";
import { EnvelopeIcon, LockClosedIcon } from "@heroicons/react/16/solid";

export const LoginPage = () => {
  const navigate = useNavigate();
  const { isOpen, onOpen, onClose } = useDisclosure();
  const { login } = useAuth();

  // state variables for email and passwords
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [rememberme, setRememberme] = useState<boolean>(false);

  // state variable for error messages
  const [message, setMessage] = useState<string>("");

  // handle submit event for the form
  const handleSubmit = async (e: React.SyntheticEvent) => {
    e.preventDefault();
    onOpen();

    // clear error message
    setMessage(() => "");

    // console.log({
    //   email: email,
    //   password: password,
    // });
    // return;
    // validate email and passwords
    // const formData: FormData = new FormData(e.target);
    // console.log(Object.create(formData));
    // return;

    if (!email || !password) {
      setMessage(() => "Please fill fields.");
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
        if (!response.ok) {
          setMessage(() => "Username or password incorrect.");
        }
        // console.log("RESPONSE", response);
        // console.log("JSON", await response.json());

        const json = await response.json();

        if (json.result.isSuccess) await login(json.data.token);

        // setMessage(() => "Successful Login.");
        // await login(email).then(() => navigate("/"));
      })
      .catch((error) => {
        // handle network error
        console.error(error);
        setMessage(() => "Error: Error Logging in.");
      });
    // .finally(() => onClose);
  };
  return (
    <>
      <div className="mt-6 max-w-2xl mx-auto">
        <form
          className="flex flex-col items-center p-4 gap-4 border-neutral-300 border-solid rounded-xl w-full border-2 shadow-sm"
          onSubmit={handleSubmit}
        >
          <Input
            autoFocus
            endContent={
              <EnvelopeIcon
                className="w-6 text-2xl text-default-400 pointer-events-none"
                role="presentation"
              />
            }
            label="Email"
            variant="bordered"
            //
            value={email}
            type="email"
            onValueChange={setEmail}
            // className="max-w-xs"
          />
          <Input
            endContent={
              <LockClosedIcon
                className="w-6 text-2xl text-default-400 pointer-events-none"
                role="presentation"
              />
            }
            label="Password"
            type="password"
            variant="bordered"
            value={password}
            onValueChange={setPassword}
          />
          <Checkbox
            defaultChecked={rememberme}
            onValueChange={() => setRememberme((prev) => !prev)}
          >
            Remember me
          </Checkbox>
          <div className="flex py-2 px-1 justify-around gap-2">
            {/* <Link color="primary" size="sm">
            Forgot password?
            </Link> */}
            <Link
              color="primary"
              className="cursor-pointer"
              size="sm"
              onClick={() => navigate("/register")}
            >
              Create Account
            </Link>
          </div>
          <div>
            <Button color="primary" className="flex-1" type="submit">
              {isOpen ? "Logging in..." : "Login"}
            </Button>
          </div>
        </form>
        {message && <p className="error p-2">{message}</p>}
      </div>
      {/* {loading && <></>} */}
      <Modal
        backdrop={"blur"}
        isOpen={isOpen}
        onClose={onClose}
        hideCloseButton
        isDismissable={false}
      >
        <ModalContent className="bg-transparent">
          {(onClose) => (
            <>
              <ModalBody className="bg-transparent">
                <Spinner />
              </ModalBody>
            </>
          )}
        </ModalContent>
      </Modal>
    </>
  );
};
