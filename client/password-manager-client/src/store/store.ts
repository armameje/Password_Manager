import { configureStore, Store } from "@reduxjs/toolkit";
import tokenReducer from "./slice/TokenSlice";
import userReducer from "./slice/UserSlice";

export const store: Store = configureStore({
  reducer: {
    token: tokenReducer,
    user: userReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;