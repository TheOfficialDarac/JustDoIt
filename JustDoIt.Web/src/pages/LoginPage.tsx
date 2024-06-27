import { useState } from "react";
import { useAuth } from "../hooks/useAuth";
import { useNavigate } from "react-router-dom";
export const LoginPage = () => {
  // state variables for email and passwords
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [rememberme, setRememberme] = useState<boolean>(false);
  // state variable for error messages
  const [error, setError] = useState<string>("");
  const navigate = useNavigate();

  const { login } = useAuth();

  // const handleLogin = async (e) => {
  //   e.preventDefault();
  //   // Here you would usually send a request to your backend to authenticate the user
  //   // For the sake of this example, we're using a mock authentication

  //   if (username === "user" && password === "password") {
  //     // Replace with actual authentication logic
  //     await login({ username });
  //   } else {
  //     alert("Invalid username or password");
  //   }
  // };

  // handle change events for input fields
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    if (name === "email") setEmail(value);
    if (name === "password") setPassword(value);
    if (name === "rememberme") setRememberme(e.target.checked);
  };

  const handleRegisterClick = () => {
    navigate("/register");
  };

  // handle submit event for the form
  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    // validate email and passwords
    if (!email || !password) {
      setError("Please fill in all fields.");
    } else {
      // clear error message
      setError("");
      // post data to the /register api

      let loginurl = "http://localhost:5153/api";
      if (rememberme == true) loginurl += "/auth/login?useCookies=true";
      else loginurl += "/auth/login?useSessionCookies=true";

      fetch(loginurl, {
        method: "POST",
        mode: "cors", // no-cors, *cors, same-origin
        headers: {
          // 'Content-Type': 'application/x-www-form-urlencoded'
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          email: email,
          password: password,
        }),
      })
        .then(async (data) => {
          // handle success or error from the server
          console.log(data);
          if (data.ok) {
            setError("Successful Login.");
            await login({ email });
            navigate("/secret");
            // window.location.href = "/";
          } else setError("Error Logging In.");
        })
        .catch((error) => {
          // handle network error
          console.error(error.message);
          setError("Error: Error Logging in.");
        });
    }
  };
  return (
    <div>
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="email">Email:</label>
          <input
            id="email"
            name="email"
            type="email"
            value={email}
            onChange={(e) => {
              // setEmail(e.target.value);
              handleChange(e);
            }}
          />
        </div>
        <div>
          <label htmlFor="password">Password:</label>
          <input
            name="password"
            id="password"
            type="password"
            value={password}
            onChange={(e) => {
              // setPassword(e.target.value);
              handleChange(e);
            }}
          />
        </div>
        <div>
          <input
            type="checkbox"
            id="rememberme"
            name="rememberme"
            checked={rememberme}
            onChange={handleChange}
          />
          <span>Remember Me</span>
        </div>
        <button type="submit">Login</button>
        <div>
          <button onClick={handleRegisterClick}>Register</button>
        </div>
      </form>
      {error && <p className="error">{error}</p>}
    </div>
  );
};
