import axios, { AxiosResponse } from "axios";
import { Platform } from "../types/PlatformType";
import { useSelector } from "react-redux";
import { RootState } from "../store/store";
import { ApiResponse } from "../types/ApiResponseType";

export class PlatformService {
  baseUrl: string;
  token: string;

  constructor(baseUrl = "https://localhost:7117/api/v1/platform/") {
    this.baseUrl = baseUrl;
    this.token = useSelector((state: RootState) => state.token.token);
  }

  async addPlatform(platform: Platform): Promise<ApiResponse> {
    let apiResponse: ApiResponse = { data: "", status: "" };
    await axios
      .post(
        this.baseUrl + `${platform.user}/${platform.platformName}`,
        {
          username: platform.username,
          password: platform.password,
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
        apiResponse.data = response.data;
      })
      .catch((response) => {
        console.error(response);
      });

    return apiResponse;
  }

  async checkIfPlatformExists(platform: Platform): Promise<boolean> {
    let value = true;

    await axios
      .get(this.baseUrl + `${platform.user}/platforms`, {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${this.token}`,
        },
      })
      .then((response) => {
        response.status;
        let exist = response.data.some((x: { username: string; platformName: string }) => x.username === platform.username && x.platformName === platform.platformName);
        if (!exist) value = false;
      });
    return value;
  }
}
