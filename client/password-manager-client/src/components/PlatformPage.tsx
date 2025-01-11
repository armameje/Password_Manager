type PlatformPageProps = {
  platformName?: string;
  username?: string;
  isEmpty?: boolean;
};

export default function PlatformPage({ platformName, username, isEmpty }: PlatformPageProps) {
  if (isEmpty) {
    return (
      <div className="flex bg-pink-300 pt-2 pb-6 px-5 w-1/5 h-1/5 flex-col justify-center items-center">
        <div>Add</div>
      </div>
    );
  } else {
    return (
      <div className="flex bg-pink-300 pt-2 pb-6 px-5 w-1/5 h-1/5 flex-col justify-between">
        <div className="text-4xl">{platformName}</div>
        <div>{username}</div>
      </div>
    );
  }
}
