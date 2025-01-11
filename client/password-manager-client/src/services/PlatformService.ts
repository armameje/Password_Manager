import { useSelector } from "react-redux";
import { RootState } from "../store/store";

const url = "https://localhost:7117";

export async function GetAllPlatforms(username: string): Promise<string[]> {
  let response = new Response();
  let headers = new Headers();
  const token = useSelector((state: RootState) => state.token.token);

  headers.append("Content-Type", "application/json");
  headers.append("Authorization", `Bearer ${token}`);

  console.log(token);

  try {
    response = await fetch(url + `/api/v1/platform/${username}/platforms`, {
      method: "GET",
      headers,
    });
  } catch {}

  return await response.json();
}