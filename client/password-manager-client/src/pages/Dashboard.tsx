import { Outlet } from "react-router-dom";
import useAuth from "../hooks/useAuth";
import PlatformPage from "../components/PlatformPage";
import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { RootState } from "../store/store";
import { Endpoints } from "../enum/links";
import Modal from "../components/Modal";

type PlatformCard = {
  platformName: string;
  username: string;
};

export default function Dashboard() {
  const auth = useAuth();
  let platformCounter = 0;
  const [platforms, setPlatforms] = useState<PlatformCard[]>();
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isNewModalOpen, setNewIsModalOpen] = useState(false);
  const [platformName, setPlatformName] = useState("");
  const [platformUsername, setplatformUsername] = useState("");
  const token = useSelector((state: RootState) => state.token.token);

  useEffect(() => {
    const allPlatforms = async () => {
      let headers = new Headers();
      headers.append("Content-Type", "application/json");
      headers.append("Authorization", `Bearer ${token}`);
      const response = await fetch(Endpoints.Platform_Endpoint + `${auth?.user}/platforms`, {
        method: "GET",
        headers,
      });
      const json = await response.json();

      setPlatforms(json);
    };

    allPlatforms();
  }, []);

  return (
    <div className="flex h-full w-full bg-orange-400 flex-col">
      <div className="flex min-w-full bg-green-400 px-4 py-3 text-2xl">
        <div className="">Hi {auth?.user}</div>
        <div></div>
      </div>
      <div className="flex w-full h-full bg-slate-400 p-8 gap-10 justify-center flex-wrap">
        {platforms?.map((x) => (
            <PlatformPage platformName={x.platformName} username={x.username} isEmpty={false} setOpenModal={setIsModalOpen} setPlatformUsername={setplatformUsername} setPlatformName={setPlatformName} key={platformCounter++} />
        ))}
        <PlatformPage isEmpty={true} platformName="" username="" setPlatformUsername={setplatformUsername} setPlatformName={setPlatformName} setOpenModal={setNewIsModalOpen} />
      </div>
      <Outlet />
      {isModalOpen && <Modal platform={platformName} username={platformUsername} setModal={setIsModalOpen} isNew={false} />}
      {isNewModalOpen && <Modal platform="" username="" setModal={setNewIsModalOpen} isNew={true} />}
      {/* create new modal for new platforms */}
    </div>
  );
}
