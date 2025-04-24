import * as crypto from "crypto";

export default function Decrypt(encrypted: string, rawKey: string) {
  const pemKey = rawKey.replace(/\\r\n/g, "\n");
  const key = Buffer.from(pemKey, "utf8");
  const buffer = Buffer.from(
    encrypted,
    "base64"
  );
  const decrypted = crypto.privateDecrypt(
    {
      key: key,
      padding: 1,
      passphrase: "",
    },
    buffer
  );
  
  return decrypted.toString("utf8");
}
