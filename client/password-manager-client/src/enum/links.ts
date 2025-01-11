const baseURL = "https://localhost:7117"

export enum Endpoints {
    Login = baseURL + "/api/v1/user/login",
    Register = baseURL + "/api/v1/user/register",
    Delete_User = baseURL + "/api/v1/user/deleteuser",
    Change_User_Password = baseURL + "/api/v1/user/changeuserpassword",
    Platform_Endpoint = baseURL + "/api/v1/platform/"
}