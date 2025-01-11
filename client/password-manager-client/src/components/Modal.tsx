type PlatformDetailsProps = {
  platform: string;
  username: string;
  password: string;
};

export default function Modal({ platform, username, password }: PlatformDetailsProps) {
  console.log(platform);
  console.log(username);
  console.log(password);

  return (
    <>
      <div className="bg-slate-500 opacity-65 w-screen h-screen z-0 absolute" />
      <div className="centered fixed top-1/2 left-1/2">
        <div className="w-[450px] h-[270px] bg-white z-10 rounded-[16px] shadow-[0_5px_20px_0_rgba(0,0,0,0.04) py-4 px-6 flex flex-col">
          <div className="h-1/2 flex justify-center content-center flex-wrap">
            <div className="">Platform</div>
          </div>
          <div className="h-1/2">
            <div className="">Username</div>
            <div className="flex justify-around">
              <input className="border-2" type="text" name="" id="" value="sadsd" disabled/>
              <button>Copy</button>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
