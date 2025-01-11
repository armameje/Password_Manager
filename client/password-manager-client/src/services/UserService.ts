import { Endpoints as endpoints } from "../enum/links";

type LoginResponse = {
  error: string;
  token: string;
};

export async function LoginUser(username: string, password: string): Promise<LoginResponse> {
  let response = new Response();

  try {
    response = await fetch(endpoints.Login, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        username: username,
        password: password,
      }),
    });
  } catch {
    return { error: "Connection Error", token: "" } satisfies LoginResponse;
  }

  return response.json();
}

export async function RegisterUser(username: string, password: string): Promise<LoginResponse> {
  let response = new Response();

  try {
    response = await fetch(endpoints.Register, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        username: username,
        password: password,
      }),
    });
  } catch {
    return { error: "Connection Error", token: "" } satisfies LoginResponse;
  }

  return response.json();
}

export async function DeleteUser(username: string) {
  let response = new Response();

  try {
    response = await fetch(endpoints.Delete_User);
  } catch {
    return { error: "Connection Error", token: "" };
  }
}

export async function ChangeUserPassword(username: string, password: string) {
  let response = new Response();

  try {
    response = await fetch(endpoints.Change_User_Password);
  } catch {
    return { error: "Conection Error", token: "" };
  }
}
