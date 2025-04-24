import React, { useState } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import FrontPage from "./FrontPage";
import useAuth from "../hooks/useAuth";
import { useDispatch } from "react-redux";
import { assignToken } from "../store/slice/TokenSlice";
import { Card, CardContent, CardDescription, CardFooter, CardTitle } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { UserService } from "@/services/UserService";

export default function LoginPage() {
  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [errorMessage, setErrorMessage] = useState<string>("");
  const [submitting, setSubmitting] = useState(false);
  const auth = useAuth();
  const navigate = useNavigate();
  const location = useLocation();
  const from = location.state?.from?.pathname || "/";
  const dispatch = useDispatch();
  const userService = new UserService();

  async function onLoginSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();

    try {
      setErrorMessage("");
      setSubmitting(true);

      if (!username || !password) {
        setErrorMessage("Username or Password must have value");
      } else {
        const loginResponse = await userService.loginUser(username, password);

        if (!!loginResponse.error) {
          setErrorMessage(loginResponse.error);
        } else {
          auth?.setUser(username);
          dispatch(assignToken({ token: loginResponse.token as string }));
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
      <Card className="w-1/2 h-1/2 flex flex-col items-center">
        <CardTitle className="text-xl mt-7">The Vault</CardTitle>
        <CardDescription className="mb-10">You're one-stop password vault</CardDescription>
        <CardContent className="w-1/2">
          <form onSubmit={onLoginSubmit} className="flex flex-col gap-2 items-center">
            <input
              className="mb-2 border rounded-md px-2 py-1 outline-none w-full"
              type="text"
              name="username"
              id="usernameID"
              placeholder="Username"
              autoComplete="off"
              onChange={(e) => setUsername(e.target.value)}
              disabled={submitting}
            />
            <input
              className="border rounded-md px-2 py-1 outline-none w-full"
              type="password"
              name="password"
              id="passwordID"
              placeholder="Password"
              onChange={(e) => setPassword(e.target.value)}
              disabled={submitting}
            />
            <div className="h-6 text-red-600 italic text-center">{errorMessage}</div>
            <Button variant={"outline"} className="hover:cursor-pointer mb-3 w-1/2" disabled={submitting}>
              Login
            </Button>
          </form>
        </CardContent>
        <CardFooter className="flex flex-col items-center ">
          <Link to="/register">
            <p className="italic opacity-50 hover:opacity-100 hover:cursor-pointer">Register here if you're new</p>
          </Link>
        </CardFooter>
      </Card>
    </FrontPage>
  );
}
