type FrontPageProps = {
  children: React.ReactNode;
};

export default function FrontPage({ children }: FrontPageProps) {
  return <div className="bg-orange-300 size-full flex items-center justify-center">{children}</div>;
}
