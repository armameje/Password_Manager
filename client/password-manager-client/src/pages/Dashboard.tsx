import { Outlet } from "react-router-dom";
import useAuth from "../hooks/useAuth";
import PlatformPage from "../components/PlatformPage";
import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { RootState } from "../store/store";
import { Endpoints } from "../enum/links";

export default function Dashboard() {
  const auth = useAuth();
  const [platforms, setPlatforms] = useState<string[]>();
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
        {platforms?.map(x => <PlatformPage platformName={x} />  )}
        <PlatformPage isEmpty={true} />
      </div>
      <Outlet />
    </div>
  );
}
