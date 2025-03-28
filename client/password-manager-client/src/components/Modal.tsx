import { Dispatch, SetStateAction, useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { RootState } from "../store/store";
import { Endpoints } from "../enum/links";
import useAuth from "../hooks/useAuth";
import Decrypt from "../services/RSAService";
import { PlatformService } from "../services/PlatformService";
import NewPlatformModal from "./NewPlatformModal";

type PlatformDetailsProps = {
  platform?: string;
  username?: string;
  password?: string;
  setModal: Dispatch<SetStateAction<boolean>>;
  isNew: boolean;
};

type PlatformDetails = {
  platformName: string;
  platformPassword: string;
  platformUsername: string;
  username: string;
};

export default function Modal({ platform, username, setModal, isNew }: PlatformDetailsProps) {
  const auth = useAuth();
  const platformService = new PlatformService();
  const token = useSelector((state: RootState) => state.token.token);
  const [platformPassword, setPlatformPassword] = useState("");
  // const [newPlatformName, setNewPlatformName] = useState("");
  // const [newPlatformUsername, setNewPlatformUsername] = useState("");
  // const [newPlatformPassword, setNewPlatformPassword] = useState("");

  async function SetPasswordToClipboard() {
    await navigator.clipboard.writeText(platformPassword);
  }

  const addPlatform = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    // await platformService.checkIfPlatformExists({ user: auth?.user as string, platformName: newPlatformName, username: newPlatformUsername, password: newPlatformPassword });
    // await platformService.addPlatform({ user: auth?.user as string, platformName: newPlatformName, username: newPlatformUsername, password: newPlatformPassword });
  };

  useEffect(() => {
    const getPlatformDetails = async () => {
      let headers = new Headers();
      headers.append("Content-Type", "application/json");
      headers.append("Authorization", `Bearer ${token}`);

      const response = await fetch(Endpoints.Platform_Endpoint + `${auth?.user}/Twitter/_athakan`, {
        method: "GET",
        headers,
      });

      const json: PlatformDetails = await response.json();
      setPlatformPassword(Decrypt(json.platformPassword));
    };
    getPlatformDetails();
  }, []);

  if (isNew) {
    return <NewPlatformModal currentUser={auth?.user as string} setModal={setModal} />
  }

  return (
    <>
      <div className="bg-slate-500 opacity-65 w-screen h-screen z-0 absolute" onClick={() => setModal(false)} />
      <div className="centered fixed top-1/2 left-1/2">
        <div className="w-[450px] h-[270px] bg-white z-10 rounded-[16px] shadow-[0_5px_20px_0_rgba(0,0,0,0.04) py-4 px-6 flex flex-col">
          <div className="h-1/2 flex justify-center content-center flex-wrap">
            <div className="">{platform}</div>
          </div>
          <div className="h-1/2">
            <div className="">{username}</div>
            <div className="flex justify-around">
              <input className="border-2" type="password" name="" id="" value={platformPassword.substring(0, 15)} disabled />
              <button onClick={SetPasswordToClipboard}>Copy</button>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
