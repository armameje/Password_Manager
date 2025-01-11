import { PayloadAction, createSlice } from "@reduxjs/toolkit";

interface ITokenState {
  expiry?: number | null;
  token: string;
}

const initialState: ITokenState = {
  expiry: null,
  token: "",
};

const tokenSlice = createSlice({
  name: "token",
  initialState,
  reducers: {
    assignToken: (state, action: PayloadAction<ITokenState>) => {
      state.token = action.payload.token;
      state.expiry = action.payload.expiry;
    },
  },
});

// export actions
export const { assignToken } = tokenSlice.actions;

export default tokenSlice.reducer;
