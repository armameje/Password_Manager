import React, { useState } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import FrontPage from "./FrontPage";
import * as userService from "../services/UserService";
import useAuth from "../hooks/useAuth";
import { useDispatch } from "react-redux";
import { assignToken } from "../store/slice/TokenSlice";
import Modal from "../components/Modal";

export default function LoginPage() {
  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [errorMessage, setErrorMessage] = useState<string>("");
  const [submitting, setSubmitting] = useState(false);
  const [isModalOpen, setIsModalOpen] = useState(true);
  const auth = useAuth();
  const navigate = useNavigate();
  const location = useLocation();
  const from = location.state?.from?.pathname || "/";
  const dispatch = useDispatch();

  async function onLoginSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();

    try {
      setErrorMessage("");
      setSubmitting(true);

      if (!username || !password) {
        setErrorMessage("Username or Password must have value");
      } else {
        const loginResponse = await userService.LoginUser(username, password);

        if (!!loginResponse.error) {
          setErrorMessage(loginResponse.error);
        } else {
          auth?.setUser(username);
          dispatch(assignToken({ token: loginResponse.token }));
          navigate(from, { replace: true });
        }
      }
    } catch {
    } finally {
      setTimeout(() => {
        setErrorMessage("");
        setSubmitting(false);
      }, 2000);
    }
  }

  return (
    <FrontPage>
      <div className="w-1/2 h-1/2 bg-green-400 flex flex-col justify-center items-center">
        <form onSubmit={onLoginSubmit} className="flex flex-col">
          <input
            className="mb-2 border rounded-md px-2 py-1 outline-none"
            type="text"
            name="username"
            id="usernameID"
            placeholder="Username"
            autoComplete="off"
            onChange={(e) => setUsername(e.target.value)}
            disabled={submitting}
          />
          <input
            className="border rounded-md px-2 py-1 outline-none"
            type="password"
            name="password"
            id="passwordID"
            placeholder="Password"
            onChange={(e) => setPassword(e.target.value)}
            disabled={submitting}
          />
          <div className="h-6 text-red-600 italic text-center">{errorMessage}</div>
          <button type="submit" className="bg-white mt-2" disabled={submitting}>
            Login
          </button>
        </form>
        <div className="flex justify-end flex-col mt-5">
          <Link to="/register">
            <p className="italic opacity-50 hover:opacity-100 hover:cursor-pointer">Register here if you're new</p>
          </Link>
        </div>
      </div>
      {isModalOpen && <Modal />}
    </FrontPage>
  );
}
