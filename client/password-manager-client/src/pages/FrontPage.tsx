type FrontPageProps = {
  children: React.ReactNode;
};

export default function FrontPage({ children }: FrontPageProps) {
  return <div className="bg-red-400 min-h-full">{children}</div>;
}
