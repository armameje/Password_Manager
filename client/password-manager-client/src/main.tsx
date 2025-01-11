import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import LoginPage from "./pages/LoginPage.tsx";
import { createBrowserRouter, RouterProvider, BrowserRouter, Routes, Route } from "react-router-dom";
import ProtectedRoute from "./pages/ProtectedRoute.tsx";
import RegistrationPage from "./pages/RegistrationPage.tsx";
import Dashboard from "./pages/Dashboard.tsx";
import { Provider } from "react-redux";
import { store } from "./store/store.ts";
import RequireAuth from "./components/RequireAuth.tsx";
import { AuthProvider } from "./context/AuthProvider.tsx";
import App from "./App.tsx";

// const ROUTER = createBrowserRouter([
//   {
//     path: "/",
//     element: (
//       <RequireAuth>
//         <Dashboard />
//       </RequireAuth>
//     ),
//   },
//   {
//     path: "login",
//     element: <LoginPage />,
//   },
//   {
//     path: "register",
//     element: <RegistrationPage />,
//   },
// ]);

// ReactDOM.createRoot(document.getElementById("root")!).render(
//   <React.StrictMode>
//     <Provider store={store}>
//       <AuthProvider>
//         <RouterProvider router={ROUTER} />
//       </AuthProvider>
//     </Provider>
//   </React.StrictMode>
// );

ReactDOM.createRoot(document.getElementById("root")!).render(
  // <React.StrictMode>
    <BrowserRouter>
      <AuthProvider>
        <Provider store={store}>
          <Routes>
            <Route path="/*" element={<App />} />
          </Routes>
        </Provider>
      </AuthProvider>
    </BrowserRouter>
  // </React.StrictMode>
);
