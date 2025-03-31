import { Dispatch, SetStateAction, useEffect, useState } from "react";
import { PlatformService } from "../services/PlatformService";
import Decrypt from "../services/RSAService";

type PlatformInfoModalProps = {
  username: string;
  platformName?: string;
  platformUsername?: string;
  setModal: Dispatch<SetStateAction<boolean>>;
};

export default function PlatformInfoModal({ username, platformName, platformUsername, setModal }: PlatformInfoModalProps) {
  const platformService = new PlatformService();
  const [platformPassword, setPlatformPassword] = useState("");

  async function setPasswordToClipboard() {
    await navigator.clipboard.writeText(platformPassword);
  }

  async function getPlatform() {
    const platformInfo = await platformService.getPlatformDetails({ username, platformName, platformUsername });

    const password = Decrypt(platformInfo.platformPassword as string);
    setPlatformPassword(password);
  }

  useEffect(() => {
    getPlatform();
  }, []);
  return (
    <>
      <div className="bg-slate-500 opacity-65 w-screen h-screen z-0 absolute" onClick={() => setModal(false)} />
      <div className="centered fixed top-1/2 left-1/2">
        <div className="w-[450px] h-[270px] bg-white z-10 rounded-[16px] shadow-[0_5px_20px_0_rgba(0,0,0,0.04) py-4 px-6 flex flex-col">
          <div className="h-1/2 flex justify-center content-center flex-wrap">
            <div className="">{platformName}</div>
          </div>
          <div className="h-1/2">
            <div className="">{platformUsername}</div>
            <div className="flex justify-around">
              <input className="border-2" type="password" name="" id="" value={platformPassword.substring(0, 15)} disabled />
              <button onClick={setPasswordToClipboard}>Copy</button>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
