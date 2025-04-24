import { Dispatch, SetStateAction, useState } from "react";
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogOverlay, DialogTitle } from "./ui/dialog";
import { Input } from "./ui/input";
import useAuth from "@/hooks/useAuth";
import { Button } from "./ui/button";
import { UserService } from "@/services/UserService";
import { toast } from "sonner";
import { useDispatch } from "react-redux";
import { assignToken } from "@/store/slice/TokenSlice";
import { useNavigate } from "react-router-dom";

type AccountManagementProps = {
  isOpen: boolean;
  setIsOpen: Dispatch<SetStateAction<boolean>>;
};

export default function AccountManagementModal({ isOpen, setIsOpen }: AccountManagementProps) {
  const auth = useAuth();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const userService = new UserService();
  const [username, setUsername] = useState(auth?.user as string);
  const [password, setPassword] = useState("");
  const [isPasswordInputVisible, setIsPasswordInputVisible] = useState(false);
//   const [errorMessage, setErrorMessage] = useState("");

  async function handleClickChangePassword() {
    setIsPasswordInputVisible((x) => !x);
  }

  async function handleClickSavePassword() {
    const response = await userService.changeUserPassword(password);

    if (response.status == 200) {
      toast("Successfully changed password");
    }
  }

  async function handleClickDeleteUser() {
    const response = await userService.deleteUser();

    if (response.status == 200) {
      toast("Delete user, routig back to Registration");
      auth?.setUser(null);
      dispatch(assignToken({ token: "" }));
      navigate("/register", { replace: true });
    }
  }

  return (
    <Dialog open={isOpen} onOpenChange={setIsOpen}>
      <DialogOverlay>
        <DialogContent onOpenAutoFocus={(e) => e.preventDefault()}>
          <DialogHeader>
            <DialogTitle>Account Management</DialogTitle>
          </DialogHeader>
          <DialogDescription>
            <Input type="text" value={username} onChange={(e) => setUsername(e.target.value)} className="text-black font-bold mb-2 disabled:opacity-100" disabled />
            <div className={`${isPasswordInputVisible ? "visible " : "invisible "} flex justify-between mb-5`}>
              <Input type="password" value={password} onChange={(e) => setPassword(e.target.value)} placeholder="Password" className="w-3/4" />
              <Button onClick={handleClickSavePassword} className="hover:transform-[scale(1.1)] bg-[#077A7D] hover:bg-[#077A7D] text-[#F5EEDD] ml-5">
                Save
              </Button>
              <svg
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                color="red"
                width={25}
                height={25}
                viewBox="0 0 24 24"
                strokeWidth={1.5}
                stroke="currentColor"
                onClick={() => {
                  setIsPasswordInputVisible((x) => !x);
                  setPassword("");
                }}
                className="block m-auto hover:transform-[scale(1.1)] hover:cursor-pointer"
              >
                <path strokeLinecap="round" strokeLinejoin="round" d="m9.75 9.75 4.5 4.5m0-4.5-4.5 4.5M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z" />
              </svg>
            </div>
            <div className="flex justify-around">
              <Button onClick={handleClickChangePassword} className="hover:transform-[scale(1.1)]" variant={"secondary"} disabled={isPasswordInputVisible}>
                Change Password
              </Button>
              <Button onClick={handleClickDeleteUser} className="hover:transform-[scale(1.1)]" variant={"destructive"}>
                Delete Account
              </Button>
            </div>
          </DialogDescription>
        </DialogContent>
      </DialogOverlay>
    </Dialog>
  );
}
