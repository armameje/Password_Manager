import { useEffect, PropsWithChildren } from "react";
import { useNavigate } from "react-router-dom";
import useAuth from "../hooks/useAuth";

type ProtectedRouteProps = PropsWithChildren;

export default function ProtectedRoute({ children }: ProtectedRouteProps) {
  const user = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (user?.user  === null) {
      navigate("/login", { replace: true });
    }
  }, [navigate, user?.user]);

  return children;
}
