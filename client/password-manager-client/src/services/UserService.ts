import useAuth from "@/hooks/useAuth";
import { RootState } from "@/store/store";
import { LoginResponse } from "@/types/LoginResponseType";
import axios from "axios";
import { useSelector } from "react-redux";

export class UserService {
  baseUrl: string;
  token: string;
  user?: string | null;

  constructor(baseUrl = "https://localhost:7117/api/v1/user/") {
    this.baseUrl = baseUrl;
    this.token = useSelector((state: RootState) => state.token.token);
    this.user = useAuth()?.user;
  }

  async loginUser(username: string, password: string) {
    let apiResponse: LoginResponse = { error: "", token: "", status: 0 };

    await axios
      .post(
        this.baseUrl + "login",
        {
          username,
          password,
        },
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      )
      .then((response) => {
        apiResponse.error = response.data.error;
        apiResponse.token = response.data.token;
        apiResponse.status = response.status;
      })
      .catch((error) => {});

    return apiResponse;
  }

  async registerUser(username: string, password: string) {
    let apiResponse: LoginResponse = {};

    await axios
      .post(
        this.baseUrl + "register",
        {
          username,
          password,
        },
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      )
      .then((response) => {
        apiResponse.error = response.data.error;
        apiResponse.token = response.data.token;
        apiResponse.status = response.status;
      })
      .catch((error) => {});

    return apiResponse;
  }

  async deleteUser() {
    let apiResponse: LoginResponse = {};

    await axios
      .delete(this.baseUrl + `deleteuser/${this.user}`, {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${this.token}`,
        },
      })
      .then((response) => {
        apiResponse.status = response.status;
      })
      .catch((e) => {console.error(e)});

    return apiResponse;
  }

  async changeUserPassword(password: string) {
    let apiResponse: LoginResponse = {};

    await axios
      .put(
        this.baseUrl + "changeuserpassword",
        {
          username: this.user,
          password,
        },
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${this.token}`,
          },
        }
      )
      .then((response) => {
        apiResponse.status = response.status;
      })
      .catch((e) => {console.error(e)});

    return apiResponse;
  }
}