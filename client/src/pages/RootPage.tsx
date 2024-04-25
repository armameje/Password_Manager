import { Outlet, Route, redirect } from "react-router-dom";
import SidebarItem, { SidebarItemProps } from "../components/sidebarItem";

let test: SidebarItemProps = {
  platform: "Facebook",
  username: "email",
};

const list: SidebarItemProps[] = [
  {
    platform: "Facebook",
    username: "asdef",
  },
  {
    platform: "Instagram",
    username: "asdef",
  },
  {
    platform: "Reddit",
    username: "asdef",
  },
  {
    platform: "Youtube",
    username: "asdef",
  },
  {
    platform: "Google",
    username: "asdef",
  },
  {
    platform: "League of Legendsa asdasda",
    username: "asdef",
  },
  {
    platform: "Steam",
    username: "asdef",
  },
  {
    platform: "R6",
    username: "asdef",
  },
  {
    platform: "Slack",
    username: "asdef",
  },
  {
    platform: "Teams",
    username: "asdef",
  },
  {
    platform: "Yahoo",
    username: "asdef",
  },
  {
    platform: "SQL",
    username: "asdef",
  },
  {
    platform: "Apache",
    username: "asdef",
  },
  {
    platform: "Nginx",
    username: "asdef",
  },
];

export default function RootPage() {
  return (
    <div className="flex h-full w-full">
      <div id="sidebar" className="flex flex-col w-2/5 bg-orange-500 ">
        <div className="flex w-full items-center justify-around order-1 border-t py-3">
          <h2 className="">Password Manager</h2>
          <button className="p-0">New</button>
        </div>
        <div className="h-full w-full overflow-y-auto p-4 flex flex-col gap-4">
          {list.map((x) => (
            <SidebarItem {...x} />
          ))}
        </div>
      </div>
      <div className="flex justify-center items-center w-full">
        <Outlet />
      </div>
    </div>
  );
}
