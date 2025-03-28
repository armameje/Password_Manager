import * as crypto from "crypto";

// export default function Decrypt() {
//   const rawKey: string = import.meta.env.VITE_KEEPKEY;
//   const pemKey = rawKey.replace(/\\n/g, "\n");

//   Buffer.from()

//   const key = new NodeRSA();
//   key.importKey(pemKey, "pkcs1");

//     const raw = key.decrypt(
//       "XkgQzqroBupeHNd5C/0Pc510Eel1+RUWmpnZjaJZKaD/ZD7Q/nF5YSg2rRs5tMZ2xQ2uE/dyn5DUpOpDjaz+G8vs0KhApiN/UcXkh2PQpaxxBfqGD5yiivyHeCl9uC4P9caSVUJXPpnFtGGOaSe2+oYzvbfalbEUSXanSGUSuHw=",
//       "utf8"
//     );
// }

export default function Decrypt(encrypted: string) {
  const rawKey: string = import.meta.env.VITE_KEEPKEY;
  const pemKey = rawKey.replace(/\\n/g, "\n");
  const key = Buffer.from(pemKey, "utf8");
  console.log(key);
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
