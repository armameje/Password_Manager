import { Dispatch, SetStateAction, useEffect, useState } from "react";
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogOverlay, DialogTitle } from "./ui/dialog";
import { Input } from "./ui/input";
import { useSelector } from "react-redux";
import { RootState } from "@/store/store";
import { Button } from "./ui/button";
import { PlatformService } from "@/services/PlatformService";
import Decrypt from "@/services/RSAService";
import { toast } from "sonner";
import { ApiResponse } from "@/types/ApiResponseType";

type PlatformModalProps = {
  isOpen: boolean;
  setIsOpen: Dispatch<SetStateAction<boolean>>;
};

export function PlatformModal({ isOpen, setIsOpen }: PlatformModalProps) {
  const platform = useSelector((state: RootState) => state.platform);
  const platformService = new PlatformService();
  // For modifying existing platform accounts
  const [platformPassword, setPlatformPassword] = useState("");
  const [newPlatformUsername, setNewPlatformUsername] = useState(platform.platformUsername);

  // For new platform account
  const [isNew, setIsNew] = useState<boolean>(platform.isNewPlatform);
  const [toAddPlatformName, setToAddPlatformName] = useState("");
  const [toAddPlatformUsername, setToAddPlatformUsername] = useState("");
  const [toAddPlatformPassword, setToAddPlatformPassword] = useState("");
  const [confirmSamePassword, setConfirmSamePassword] = useState("");
  const [errorMessage, setErrorMessage] = useState("");
  const [kagi, setKagi] = useState("");

  function closeModal() {
    setIsOpen((x) => !x);
    setPlatformPassword("");
  }

  function closeModalForNewPlatform() {
    setIsOpen((x) => !x);
    setTimeout(() => {
      emptyStringStates();
    }, 500);
  }

  function emptyStringStates() {
    setToAddPlatformName("");
    setToAddPlatformUsername("");
    setToAddPlatformPassword("");
    setConfirmSamePassword("");
  }

  async function addPlatform() {
    let response: ApiResponse = { data: "", status: 0 };

    console.log(`${toAddPlatformName} ${toAddPlatformUsername} ${toAddPlatformPassword} ${confirmSamePassword}`);
    try {
      if (!!!toAddPlatformName) {
        setErrorMessage("Enter Platform Name");
      } else if (!!!toAddPlatformUsername) {
        setErrorMessage("Enter Platform Username");
      } else if (!!!toAddPlatformPassword) {
        setErrorMessage("Enter Platform Password");
      } else if (!!!confirmSamePassword) {
        setErrorMessage("Please confirm password");
      } else if (toAddPlatformPassword !== confirmSamePassword) {
        setErrorMessage("Password is not the same");
      } else {
        response = await platformService.addPlatform({ platformName: toAddPlatformName, platformUsername: toAddPlatformUsername, platformPassword: toAddPlatformPassword });
      }

      console.log(response);
      if (response?.status == 200) {
        closeModal();
        emptyStringStates();
        toast("Successfull added platform");
      } else if (response.status > 0) {
        setErrorMessage(response.data);
      }

    } catch {
    } finally {
      setTimeout(() => {
        setErrorMessage("");
      }, 1500);
    }
  }

  async function updatePlatform() {
    try {
      const existingPassword = await platformService.getPlatformDetails({ platformName: platform.platformName, platformUsername: platform.username });
      let response: ApiResponse = { data: "", status: 0 };

      if (!!!newPlatformUsername) {
        setErrorMessage("Enter username");
      } else if (!!!platformPassword) {
        setErrorMessage("Enter new password");
      } else if (Decrypt(existingPassword.platformPassword as string, kagi) === platformPassword) {
        setErrorMessage("Can't use old password");
      } else {
        response = await platformService.updatePlatform({ platformName: platform.platformName, platformUsername: platform.username, platformPassword }, newPlatformUsername);
      }

      console.log(response);

      if (response?.status == 200) {
        closeModal();
        toast("Successfully changed");
      }
    } catch {
    } finally {
      setTimeout(() => {
        setErrorMessage("");
      }, 2000);
    }
  }

  useEffect(() => {
    setNewPlatformUsername(platform.username);
    setIsNew(platform.isNewPlatform);

    const getKagi = async () => {
      const k = await platformService.kagi();
      setKagi(k);
    };

    getKagi();
  }, [platform]);

  if (isNew) {
    return (
      <Dialog open={isOpen} onOpenChange={closeModalForNewPlatform}>
        <DialogOverlay>
          <DialogContent className="z-[51] p-10" onOpenAutoFocus={(e) => e.preventDefault()}>
            <DialogHeader className="">
              <DialogTitle className="mb-3">Add Platform</DialogTitle>
              <Input type="text" value={toAddPlatformName} placeholder="Platform Name" onChange={(e) => setToAddPlatformName(e.target.value)} className="selection:bg-[#077A7D] selection:text-[#F5EEDD]" />
              <Input type="text" value={toAddPlatformUsername} placeholder="Platform Username" onChange={(e) => setToAddPlatformUsername(e.target.value)} className="selection:bg-[#077A7D] selection:text-[#F5EEDD]" />
              <Input type="password" value={toAddPlatformPassword} onChange={(e) => setToAddPlatformPassword(e.target.value)} placeholder="Password" className="selection:bg-[#077A7D] selection:text-[#F5EEDD]" />
              <Input type="password" value={confirmSamePassword} onChange={(e) => setConfirmSamePassword(e.target.value)} placeholder="Confirm Password" className="selection:bg-[#077A7D] selection:text-[#F5EEDD]" />
              <div className="h-[24px] text-center text-red-600 italic">{errorMessage}</div>
            </DialogHeader>
            <DialogDescription className="flex justify-center">
              <Button onClick={addPlatform}>Save</Button>
            </DialogDescription>
          </DialogContent>
        </DialogOverlay>
      </Dialog>
    );
  }

  return (
    <Dialog open={isOpen} onOpenChange={closeModal}>
      <DialogOverlay className="">
        <DialogContent className="z-[51] p-10" onOpenAutoFocus={(e) => e.preventDefault()}>
          <DialogHeader>
            <DialogTitle className="mb-3">Edit Platform</DialogTitle>
            <Input type="text" value={platform.platformName} className="disabled:opacity-100 disabled:font-bold  selection:bg-[#077A7D] selection:text-[#F5EEDD]" disabled />
            <Input type="text" value={newPlatformUsername} onChange={(e) => setNewPlatformUsername(e.target.value)} className="selection:bg-[#077A7D] selection:text-[#F5EEDD]" />
            <Input type="password" onChange={(e) => setPlatformPassword(e.target.value)} placeholder="New Password" className="selection:bg-[#077A7D] selection:text-[#F5EEDD]" />
            <div className="h-[24px] text-center text-red-600 italic selection:bg-[#077A7D] selection:text-[#F5EEDD]">{errorMessage}</div>
          </DialogHeader>
          <DialogDescription className="flex justify-center">
            <Button onClick={updatePlatform}>Save</Button>
          </DialogDescription>
        </DialogContent>
      </DialogOverlay>
    </Dialog>
  );
}
