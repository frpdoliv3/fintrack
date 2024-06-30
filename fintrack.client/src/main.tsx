import axios from 'axios';
import React from 'react';
import ReactDOM from 'react-dom/client';
import axiosRetry from 'axios-retry';
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import "./cssReset.css"
import "bootswatch/dist/darkly/bootstrap.min.css";
import "./main.css"
import {routes} from "./navigation/routes.tsx";

axiosRetry(axios, { retries: 3, retryDelay: axiosRetry.exponentialDelay })

const router = createBrowserRouter(routes)

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>,
);