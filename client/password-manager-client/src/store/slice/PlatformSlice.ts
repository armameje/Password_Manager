import { createSlice, PayloadAction } from "@reduxjs/toolkit";

interface IPlatformState {
  isNewPlatform?: boolean;
  platformName: string;
  username: string;
  platformPassword?: string;
}

const initialState: IPlatformState = {
  isNewPlatform: false,
  platformName: "",
  username: "",
  platformPassword: "",
};

const platformSlice = createSlice({
  name: "platform",
  initialState,
  reducers: {
    selectPlatform: (state, action: PayloadAction<IPlatformState>) => {
      state.isNewPlatform = false;
      state.platformName = action.payload.platformName;
      state.username = action.payload.username;
      state.platformPassword = action.payload.platformPassword;
    },
    addNewPlatform: (state) => {
      state.isNewPlatform = true;
      state.platformName = "";
      state.username = "";
      state.platformPassword = "";
    }
  },
});

export const { selectPlatform, addNewPlatform } = platformSlice.actions;

export default platformSlice.reducer;
