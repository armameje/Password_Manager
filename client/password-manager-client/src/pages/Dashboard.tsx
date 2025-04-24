import { Outlet } from "react-router-dom";
import useAuth from "../hooks/useAuth";
import PlatformPage from "../components/PlatformPage";
import { useEffect, useState } from "react";
import { PlatformService } from "../services/PlatformService";
import { PlatformCard } from "../types/PlatformCardType";
import { Toaster } from "sonner";
import { PlatformModal } from "@/components/PlatformModal";
import { Button } from "@/components/ui/button";
import AccountManagementModal from "@/components/AccountManagementModal";

export default function Dashboard() {
  const auth = useAuth();
  const platformService = new PlatformService();
  let platformCounter = 0;
  const [platforms, setPlatforms] = useState<PlatformCard[]>();
  const [isAccountModalOpen, setIsAccountModalOpen] = useState(true);
  const [isModalOpen, setIsModalOpen] = useState(false);

  function capitalize(x: string) {
    const string = x;

    return string.charAt(0).toUpperCase() + string.slice(1);
  }

  useEffect(() => {
    const allPlatforms = async () => {
      const data = await platformService.getAllPlatforms(auth?.user as string);

      setPlatforms(data);
    };

    allPlatforms();
  }, [platforms]);

  return (
    <div className="flex h-full w-full flex-col">
      <div className="flex min-w-full bg-[#7AE2CF] pl-6 pr-8 py-4 text-2xl justify-between">
        <div className="text-[#06202B]">Hi {capitalize(auth?.user as string)} 👋</div>
        <Button onClick={() => setIsAccountModalOpen((x) => !x)} className="bg-[#06202B] hover:bg-[#06202B] hover:transform-[scale(1.1)]">
          Manage Account
        </Button>
      </div>
      <div className="flex w-full h-full bg-[#F5EEDD] p-8 gap-10 flex-col overflow-x-hidden">
        {platforms?.map((x) => (
          <PlatformPage platformName={x.platformName} username={x.username} isEmpty={false} setOpenModal={setIsModalOpen} key={platformCounter++} />
        ))}
        <PlatformPage isEmpty={true} platformName="" username="" setOpenModal={setIsModalOpen} />
      </div>
      <Outlet />
      <AccountManagementModal isOpen={isAccountModalOpen} setIsOpen={setIsAccountModalOpen} />
      <PlatformModal isOpen={isModalOpen} setIsOpen={setIsModalOpen} />
      <Toaster duration={900} />
    </div>
  );
}
