import axios from 'axios';
import React from 'react';
import ReactDOM from 'react-dom/client';
import App from "./App.tsx";
import axiosRetry from 'axios-retry';
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import Login from "@pages/login/Login.tsx";
import "./cssReset.css"
import "bootswatch/dist/darkly/bootstrap.min.css";
import "./main.css"

axiosRetry(axios, { retries: 3, retryDelay: axiosRetry.exponentialDelay })

const router = createBrowserRouter([
    {
        path: "/",
        element: <App />
    },
    {
        path: "/login",
        element: <Login />
    }
])

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>,
);