import axios, { AxiosResponse } from "axios";
import { Platform } from "../types/PlatformType";
import { useSelector } from "react-redux";
import { RootState } from "../store/store";
import { ApiResponse } from "../types/ApiResponseType";
import { PlatformCard } from "../types/PlatformCardType";

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
        this.baseUrl + `${platform.username}/${platform.platformName}`,
        {
          username: platform.username,
          password: platform.platformPassword,
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
      .get(this.baseUrl + `${platform.username}/platforms`, {
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

  async getPlatformDetails(platform: Platform) {
    let platformInfo: Platform = platform;
    await axios
      .get(this.baseUrl + `${platform.username}/${platform.platformName}/${platform.platformUsername}`, {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${this.token}`,
        },
      })
      .then((response) => {
        platformInfo = response.data;
      });

    return platformInfo;
  }

  async getAllPlatforms(user: string) {
    let platforms: PlatformCard[] = [];

    await axios
      .get(this.baseUrl + `${user}/platforms`, {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${this.token}`,
        },
      })
      .then((response) => {
        platforms = response.data;
      });

    return platforms;
  }
}
