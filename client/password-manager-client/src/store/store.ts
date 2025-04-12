import { configureStore, Store } from "@reduxjs/toolkit";
import tokenReducer from "./slice/TokenSlice";
import userReducer from "./slice/UserSlice";
import platformReducer from "./slice/PlatformSlice";

export const store: Store = configureStore({
  reducer: {
    token: tokenReducer,
    user: userReducer,
    platform: platformReducer
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
