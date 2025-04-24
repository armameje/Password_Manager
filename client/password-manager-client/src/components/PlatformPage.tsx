import { Dispatch, SetStateAction, useEffect, useState } from "react";
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from "./ui/card";
import { Button } from "./ui/button";
import { PlatformService } from "@/services/PlatformService";
import Decrypt from "@/services/RSAService";
import { toast } from "sonner";
import { useDispatch } from "react-redux";
import { selectPlatform, addNewPlatform } from "@/store/slice/PlatformSlice";

type PlatformPageProps = {
  platformName: string;
  username: string;
  isEmpty?: boolean;
  setOpenModal: Dispatch<SetStateAction<boolean>>;
};

export default function PlatformPage({ platformName, username, isEmpty, setOpenModal }: PlatformPageProps) {
  const [kagi, setKagi] = useState("");
  const platformService = new PlatformService();
  const dispatch = useDispatch();

  async function onGetPasswordClick() {
    const response = await platformService.getPlatformDetails({ platformName, platformUsername: username });

    const decryptedString = Decrypt(response.platformPassword as string, kagi);
    setPasswordToClipboard(decryptedString);
  }

  async function setPasswordToClipboard(password: string) {
    await navigator.clipboard.writeText(password);
    toast("Password is copied to clipboard");
  }

  function handleClickEditPlatform() {
    setOpenModal(true);

    dispatch(selectPlatform({ platformName, username }));
  }

  function handleClickAddPlatform() {
    setOpenModal(true);
    dispatch(addNewPlatform());
  }

  async function handleClickDeletePlatform() {
    await platformService.deletePlatform({ platformName, platformUsername: username });

    toast("Platform Deleted");
  }

  useEffect(() => {

    const getKagi = async () => {
      let k = await platformService.kagi();

      setKagi(k);
    };

    getKagi();
  }, []);

  if (isEmpty) {
    return (
      <Card onClick={handleClickAddPlatform} className="h-1/6 flex justify-center items-center hover:cursor-pointer hover:bg-[#07797d81] hover:text-[#F5EEDD] hover:transform-[scale(1.01)] hover:">
        <CardTitle>Add</CardTitle>
      </Card>
    );
  } else {
    return (
      <Card className="flex flex-row py-5 justify-center hover:transform-[scale(1.01)] hover:shadow-[0_3px_6px_rgba(7,122,125,1)]">
        <div className="flex flex-col w-2/3">
          <CardHeader>
            <CardTitle className="text-lg">{platformName}</CardTitle>
            <CardDescription className="text-base">{username}</CardDescription>
          </CardHeader>
          <CardContent className="mt-3">
            <Button className="" variant={"outline"} onClick={onGetPasswordClick}>
              Get Password
            </Button>
          </CardContent>
        </div>
        <CardFooter className="w-1/3 flex justify-end pr-10">
          <Button className="mr-3 hover:cursor-pointer hover:bg-[#07797d81] hover:text-[#F5EEDD] hover:transform-[scale(1.1)]" variant={"secondary"} onClick={handleClickEditPlatform}>
            Edit
          </Button>
          <Button className="hover:cursor-pointer hover:transform-[scale(1.1)]" variant={"destructive"} onClick={handleClickDeletePlatform}>
            Delete
          </Button>
        </CardFooter>
      </Card>
    );
  }
}
