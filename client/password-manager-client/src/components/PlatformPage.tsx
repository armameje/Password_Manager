import { Dispatch, SetStateAction, use, useEffect } from "react";

type PlatformPageProps = {
  platformName: string;
  username: string;
  isEmpty?: boolean;
  setOpenModal: Dispatch<SetStateAction<boolean>>;
  setPlatformName: Dispatch<SetStateAction<string>>;
  setPlatformUsername: Dispatch<SetStateAction<string>>;
};

export default function PlatformPage({ platformName, username, isEmpty, setOpenModal, setPlatformName, setPlatformUsername }: PlatformPageProps) {
  useEffect(() => {
    setPlatformName(platformName);
    setPlatformUsername(username);
  }, []);
  if (isEmpty) {
    return (
      <button className="bg-pink-300 pt-2 pb-6 px-5 w-1/5 h-1/5 focus:outline-none" onClick={() => setOpenModal(true)}>
        <div className="">Add</div>
      </button>
    );
  } else {
    return (
      <button className="bg-pink-300 pt-2 pb-6 px-5 w-1/5 h-1/5 focus:outline-none" onClick={() => setOpenModal(true)}>
        <div className="">{platformName}</div>
        <div className="">{username}</div>
      </button>
    );
  }
}
