import { Dispatch, SetStateAction, useEffect } from "react";
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from "./ui/card";
import { Button } from "./ui/button";
import { PlatformService } from "@/services/PlatformService";
import useAuth from "@/hooks/useAuth";
import Decrypt from "@/services/RSAService";
import { toast } from "sonner";

type PlatformPageProps = {
  platformName: string;
  username: string;
  isEmpty?: boolean;
  setOpenModal: Dispatch<SetStateAction<boolean>>;
  setPlatformName: Dispatch<SetStateAction<string>>;
  setPlatformUsername: Dispatch<SetStateAction<string>>;
};

export default function PlatformPage({ platformName, username, isEmpty, setOpenModal, setPlatformName, setPlatformUsername }: PlatformPageProps) {
  const platformService = new PlatformService();
  const auth = useAuth();

  async function onGetPasswordClick() {
    const response = await platformService.getPlatformDetails({ username: auth?.user, platformName, platformUsername: username });

    const decryptedString = Decrypt(response.platformPassword as string);
    setPasswordToClipboard(decryptedString);
  }

  async function setPasswordToClipboard(password: string) {
    await navigator.clipboard.writeText(password);
    toast("Password is copied to clipboard")
  }

  useEffect(() => {
    setPlatformName(platformName);
    setPlatformUsername(username);
  }, []);
  if (isEmpty) {
    return (
      <Card className="h-1/6 flex justify-center items-center">
        <CardTitle>Add</CardTitle>
      </Card>
      // <button className="bg-pink-300 pt-2 pb-6 px-5 w-1/5 h-1/5 focus:outline-none" onClick={() => setOpenModal(true)}>
      //   <div className="">Add</div>
      // </button>
    );
  } else {
    return (
      <Card className="flex flex-row py-5 justify-center">
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
          <Button className="mr-3" variant={"secondary"}>
            Edit
          </Button>
          <Button variant={"destructive"}>Delete</Button>
        </CardFooter>
      </Card>
      // <button className="bg-pink-300 pt-2 pb-6 px-5 w-1/5 h-1/5 focus:outline-none" onClick={() => setOpenModal(true)}>
      //   <div className="">{platformName}</div>
      //   <div className="">{username}</div>
      // </button>
    );
  }
}
