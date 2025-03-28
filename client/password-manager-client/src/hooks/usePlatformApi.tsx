import { useSelector } from "react-redux";
import { RootState } from "../store/store";
import { Endpoints as endpoints } from "../enum/links";
import useAuth from "./useAuth";

export default function usePlatformApi() {
  const auth = useAuth();
  const token = useSelector((state: RootState) => state.token.token);

  async function AddPlatform(platformName: string, platformUsername: string, platformPassword: string) {
    let response = new Response();
    let headers = new Headers();

    headers.append("Content-Type", "application/json");
    headers.append("Authorization", `Bearer ${token}`);

    response = await fetch(endpoints.Register + `${auth?.user}/${platformName}`, {
      method: "POST",
      headers,
      body: JSON.stringify({
        Username: platformUsername,
        Password: platformPassword,
      }),
    });
  }

  return { AddPlatform };
}
