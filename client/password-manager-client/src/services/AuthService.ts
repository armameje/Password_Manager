import jwt from "jsonwebtoken";
import { useDispatch } from "react-redux";

const TOKEN_ISSUER = "PasswordManager_Jack";
const AUDIENCE = "pleabs";
const dispatch = useDispatch();

type JwtToken = {
  valid: boolean;
  expiry: number;
  expired: boolean;
  token: string;
};

export function verifyJWT(token: string): JwtToken {
  const decodedToken = jwt.decode(token, { complete: true });

  if (decodedToken?.payload.iss != TOKEN_ISSUER && decodedToken?.payload.aud != AUDIENCE) {
    return { valid: false } as JwtToken;
  }

  const tokenExpiry = new Date(0);
  tokenExpiry.setUTCSeconds(decodedToken.payload.exp ?? 0);

  const currentDate = new Date();

  if (tokenExpiry > currentDate) {
    return { valid: true, expired: true } as JwtToken;
  }
  
  
  return { valid: true, expiry: decodedToken.payload.exp, expired: false, token } as JwtToken;
}