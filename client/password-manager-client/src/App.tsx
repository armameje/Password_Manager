import { Routes, Route } from "react-router-dom";
import LoginPage from "./pages/LoginPage";
import RegistrationPage from "./pages/RegistrationPage";
import RequireAuth from "./components/RequireAuth";
import Dashboard from "./pages/Dashboard";

export default function App() {
    return (
        <Routes>
            <Route path="login" element={<LoginPage />} />
            <Route path="register" element={<RegistrationPage />} />

            <Route element={<RequireAuth />}>
                <Route path="/" element={<Dashboard />} />
            </Route>
        </Routes>
    )
}