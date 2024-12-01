const url = "https://localhost:7117";

type LoginResponse = {
  error: string;
  token: string;
};

export async function LoginUser(username: string, password: string): Promise<LoginResponse> {
  let response = new Response();

  try {
    response = await fetch(url + "/api/v1/user/login", {
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
    response = await fetch(url + "/api/v1/user/register", {
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
