import { createSlice, PayloadAction } from "@reduxjs/toolkit";

interface IPlatformState {
  platformName: string;
  username: string;
  platformPassword?: string;
}

const initialState: IPlatformState = {
  platformName: "",
  username: "",
  platformPassword: "",
};

const platformSlice = createSlice({
  name: "platform",
  initialState,
  reducers: {
    selectPlatform: (state, action: PayloadAction<IPlatformState>) => {
        state.platformName = action.payload.platformName;
        state.username = action.payload.username;
        state.platformPassword = action.payload.platformPassword;
    },
  },
});

export const { selectPlatform } = platformSlice.actions;

export default platformSlice.reducer;