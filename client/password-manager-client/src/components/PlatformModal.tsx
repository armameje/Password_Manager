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
  const [platformPassword, setPlatformPassword] = useState("");
  const [newPlatformUsername, setNewPlatformUsername] = useState(platform.platformUsername);
  const [errorMessage, setErrorMessage] = useState("");
  const [kagi, setKagi] = useState("");

  function closeModal() {
    setIsOpen((x) => !x);
    setPlatformPassword("");
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

      if (response?.status == 200) {
        closeModal();
        toast(`Successfully changed`);
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

    const getKagi = async () => {
      const k = await platformService.kagi();
      setKagi(k);
    };

    getKagi();
  }, [platform]);

  return (
    <Dialog open={isOpen} onOpenChange={closeModal}>
      <DialogOverlay className="">
        <DialogContent className="z-[51] p-10" onOpenAutoFocus={(e) => e.preventDefault()}>
          <DialogHeader>
            <DialogTitle className="mb-3">Edit Platform</DialogTitle>
            <Input type="text" value={platform.platformName} className="disabled:opacity-100 disabled:font-bold" disabled />
            <Input type="text" value={newPlatformUsername} onChange={(e) => setNewPlatformUsername(e.target.value)} className="" />
            <Input type="password" onChange={(e) => setPlatformPassword(e.target.value)} placeholder="New Password" />
            <div className="h-[24px] text-center text-red-600 italic">{errorMessage}</div>
          </DialogHeader>
          <DialogDescription className="flex justify-center">
            <Button onClick={updatePlatform}>Save</Button>
          </DialogDescription>
        </DialogContent>
      </DialogOverlay>
    </Dialog>
  );
}
