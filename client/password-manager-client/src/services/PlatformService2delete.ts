import { useSelector } from "react-redux";
import { RootState } from "../store/store";
import { Endpoints as endpoints } from "../enum/links";

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

export async function AddPlatform(userPerson: string, platforName: string, platformUsername: string, platformPassword: string) {
  let response = new Response();
  let headers = new Headers();

  const token = useSelector((state: RootState) => state.token.token);

  headers.append("Content-Type", "application/json");
  headers.append("Authorization", `Bearer ${token}`);

  try {
    response = await fetch(endpoints.Platform_Endpoint + `${userPerson}/${platforName}`, {
      method: "POST",
      headers,
      body: JSON.stringify({
        Username: platformUsername,
        Password: platformPassword,
      }),
    });

    console.log(response);
  } catch {}
}
