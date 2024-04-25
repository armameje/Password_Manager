import { useState } from "react";

export default function LoginPage() {
  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");

  return (
    <section className="bg-orange-300 size-full flex items-center justify-center">
      <div className="bg-white w-5/12 h-1/2 rounded-3xl flex">
        <div className="flex justify-center items-center w-2/5 border-r-green-400 border-r">Icon</div>
        <div className="flex justify-center items-center">
          <form>
            <div className="">
              <input type="text" name="uagqw#@$" id="usernameID" placeholder="Username" autoComplete="off" onChange={(e) => setUsername(e.target.value)} />
              <input type="password" name="password" id="passwordID" placeholder="Password" onChange={(e) => setPassword(e.target.value)} />
              <button>Submit</button>
            </div>
          </form>
        </div>
      </div>
    </section>
  );
}
