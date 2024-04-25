import React from "react";
import ReactDOM from "react-dom/client";
import LoginPage from "./pages/LoginPage.tsx";
import { createBrowserRouter, redirect, RouterProvider } from "react-router-dom";
import "./index.css";
import RootPage from "./pages/RootPage.tsx";
import { store } from "./store/store.ts";
import { Provider } from "react-redux";
import AccountCreatePage from "./pages/AccountCreatePage.tsx";

let isLoggedIn = false;

const router = createBrowserRouter([
  {
    path: "/",
    element: <RootPage />,
    children: [
      {
        path: "account/create",
        element: <AccountCreatePage />
      }
    ]
  },
  {
    path: "login",
    element: <LoginPage />,
  },
]);

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <Provider store={store}>
      <RouterProvider router={router} />
    </Provider>
  </React.StrictMode>
);
