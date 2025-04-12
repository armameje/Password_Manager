import { Dispatch, SetStateAction, useState } from "react";
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogOverlay, DialogTitle } from "./ui/dialog";
import { Input } from "./ui/input";
import { useSelector } from "react-redux";
import { RootState } from "@/store/store";
import { Button } from "./ui/button";

type PopUpProps = {
  isOpen: boolean;
  setIsOpen: Dispatch<SetStateAction<boolean>>;
};

export function PopUp({ isOpen, setIsOpen }: PopUpProps) {
  const platform = useSelector((state: RootState) => state.platform);
  const [platformPassword, setPlatformPassword] = useState("");

  function closeModal() {
    setIsOpen((x) => !x);
    setPlatformPassword("");
  }

  return (
    <Dialog open={isOpen} onOpenChange={closeModal}>
      <DialogOverlay className="">
        <DialogTitle>Edit Platform</DialogTitle>
        <DialogContent className="z-[51] p-10" onOpenAutoFocus={(e) => e.preventDefault()}>
          <DialogHeader>
            <Input type="text" value={platform.platformName} className="disabled:opacity-100 disabled:font-bold" disabled />
            <Input type="text" value={platform.username} className="focu" />
            <Input type="password" value={platformPassword} onChange={(e) => setPlatformPassword(e.target.value)} placeholder="New Password" />
          </DialogHeader>
          <DialogDescription className="flex justify-around">
            <Button>Save</Button>
            <Button></Button>
          </DialogDescription>
        </DialogContent>
      </DialogOverlay>
    </Dialog>
  );
}
