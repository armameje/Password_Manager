import { Dispatch, SetStateAction, useState } from "react";
import { PlatformService } from "../services/PlatformService";
import { Platform } from "../types/PlatformType";

type NewPlatformModalProps = {
  currentUser: string;
  setModal: Dispatch<SetStateAction<boolean>>;
};

export default function NewPlatformModal({ currentUser, setModal }: NewPlatformModalProps) {
  const [platformAdditionMessage, setPlatformAdditionMessage] = useState("");
  const [validationMessageColor, setValidationMessageColor] = useState("");
  const [newPlatformName, setNewPlatformName] = useState("");
  const [newPlatformUsername, setNewPlatformUsername] = useState("");
  const [newPlatformPassword, setNewPlatformPassword] = useState("");
  const platformService = new PlatformService();

  async function addPlatform(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();

    const userInfo: Platform = {
      username: currentUser,
      platformName: newPlatformName,
      platformUsername: newPlatformUsername,
      platformPassword: newPlatformPassword,
    };

    try {
      const platformExist = await platformService.checkIfPlatformExists(userInfo);

      if (platformExist) {
        setValidationMessageColor("text-red-600");
        setPlatformAdditionMessage(`Username already taken for ${newPlatformName}`);
        return;
      }
      
      const response = await platformService.addPlatform(userInfo);

      if (response.status == 200) {
        setValidationMessageColor("text-green-300");
        setPlatformAdditionMessage("Successfully added platform");
      } else {
        setValidationMessageColor("text-red-600");
        setPlatformAdditionMessage("Error with ");
      }
    } catch {
    } finally {
      setTimeout(() => {
        setPlatformAdditionMessage("");
      }, 2000);
    }
  }

  return (
    <>
      <div className="bg-slate-500 opacity-65 w-screen h-screen z-0 absolute" onClick={() => setModal(false)} />
      <div className="centered fixed top-1/2 left-1/2">
        <div className="w-[450px] h-[270px] bg-white z-10 rounded-[16px] shadow-[0_5px_20px_0_rgba(0,0,0,0.04) py-4 px-6">
          <form className="w-full h-full flex flex-col justify-center gap-4" onSubmit={addPlatform}>
            <div>
              <span>PlatformName: </span>
              <input className="border outline-none p-1" type="text" name="" id="" onChange={(e) => setNewPlatformName(e.target.value)} />
            </div>
            <div>
              <span>Username: </span>
              <input className="border outline-none p-1" type="text" name="" id="" onChange={(e) => setNewPlatformUsername(e.target.value)} />
            </div>
            <div>
              <span>Password: </span>
              <input className="border outline-none p-1" type="password" name="" id="" onChange={(e) => setNewPlatformPassword(e.target.value)} />
            </div>
            <div className={`h-14 ${validationMessageColor}`}>{platformAdditionMessage}</div>
            <div className="flex justify-center">
              <button className="bg-white px-10">Add</button>
            </div>
          </form>
        </div>
      </div>
    </>
  );
}
