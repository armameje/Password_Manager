import { Dispatch, SetStateAction, useEffect, useState } from "react";
import useAuth from "../hooks/useAuth";
import NewPlatformModal from "./NewPlatformModal";
import PlatformInfoModal from "./PlatformInfoModal";

type PlatformDetailsProps = {
  platform?: string;
  username?: string;
  password?: string;
  setModal: Dispatch<SetStateAction<boolean>>;
  isNew: boolean;
};

export default function Modal({ platform, username, setModal, isNew }: PlatformDetailsProps) {
  const auth = useAuth();

  if (isNew) {
    return <NewPlatformModal currentUser={auth?.user as string} setModal={setModal} />
  }

  return <PlatformInfoModal username={auth?.user as string} setModal={setModal} platformName={platform} platformUsername={username} />
}
