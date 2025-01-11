import { PayloadAction, createSlice } from "@reduxjs/toolkit";

interface IUserState {
  user: string | null;
}

const initialState: IUserState = {
  user: null,
};

const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    assignUser: (state, action: PayloadAction<IUserState>) => {
      state.user = action.payload.user;
    },
  },
});

export const { assignUser } = userSlice.actions;

export default userSlice.reducer;