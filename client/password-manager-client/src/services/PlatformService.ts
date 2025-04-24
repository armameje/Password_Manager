import axios from "axios";
import { Platform } from "../types/PlatformType";
import { useSelector } from "react-redux";
import { RootState } from "../store/store";
import { ApiResponse } from "../types/ApiResponseType";
import { PlatformCard } from "../types/PlatformCardType";
import useAuth from "@/hooks/useAuth";

export class PlatformService {
  baseUrl: string;
  token: string;
  user?: string | null;

  constructor(baseUrl = "https://localhost:7117/api/v1/platform/") {
    this.baseUrl = baseUrl;
    this.token = useSelector((state: RootState) => state.token.token);
    this.user = useAuth()?.user;
  }

  async addPlatform(platform: Platform): Promise<ApiResponse> {
    let apiResponse: ApiResponse = { data: "", status: "" };
    console.log(platform);
    await axios
      .post(
        this.baseUrl + `${this.user}/${platform.platformName}`,
        {
          username: platform.platformUsername,
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
        console.log(response);
        apiResponse.status = response.status;
        apiResponse.data = response.data;
      })
      .catch((response) => {
        apiResponse.data = response.request.responseText;
        apiResponse.status = response.status;
      });

    return apiResponse;
  }

  async updatePlatform(platform: Platform, newPlatformUsername: string): Promise<ApiResponse> {
    let apiResponse: ApiResponse = { data: "", status: "" };

    await axios
      .post(
        this.baseUrl + `${this.user}/${platform.platformName}/modify`,
        {
          username: platform.platformUsername,
          password: platform.platformPassword,
          newusername: newPlatformUsername,
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
      .catch();

    return apiResponse;
  }

  async checkIfPlatformExists(platform: Platform): Promise<boolean> {
    let value = true;

    await axios
      .get(this.baseUrl + `${this.user}/platforms`, {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${this.token}`,
        },
      })
      .then((response) => {
        response.status;
        let exist = response.data.some((x: { username: string; platformName: string }) => x.username === platform.platformUsername && x.platformName === platform.platformName);
        if (!exist) value = false;
      });
    return value;
  }

  async getPlatformDetails(platform: Platform) {
    let platformInfo: Platform = platform;
    await axios
      .get(this.baseUrl + `${this.user}/${platform.platformName}/${platform.platformUsername}`, {
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

  async deletePlatform(platform: Platform) {
    await axios.delete(this.baseUrl + `${this.user}/delete/${platform.platformName}/${platform.platformUsername}`, {
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${this.token}`,
      },
    });
  }

  async kagi() {
    let string = "sd";

    await axios
      .get(this.baseUrl + "kagi", {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${this.token}`,
        },
      })
      .then((response) => {
        string = response.data;
      });

    return string;
  }
}
