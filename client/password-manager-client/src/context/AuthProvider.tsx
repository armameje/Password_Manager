import { createContext, Dispatch, PropsWithChildren, SetStateAction, useState } from "react";

type AuthProps = {
  user: string | null;
  setUser: Dispatch<SetStateAction<string | null>>;
};

export const AuthContext = createContext<AuthProps | null>(null);

type AuthProviderProps = PropsWithChildren;

export function AuthProvider({ children }: AuthProviderProps) {
  const [auth, setAuth] = useState<string| null>(null);

  return <AuthContext.Provider value={{ user: auth, setUser: setAuth }}>{children}</AuthContext.Provider>;
}
