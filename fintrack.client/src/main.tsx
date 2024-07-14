import axios from 'axios';
import React from 'react';
import ReactDOM from 'react-dom/client';
import axiosRetry from 'axios-retry';
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import "bootswatch/dist/darkly/bootstrap.min.css";
import "./cssReset.css"
import "./main.css"
import {routes} from "./navigation/routes.tsx";
import UserContextProvider from '@components/UserContextProvider.tsx';

axiosRetry(axios, { retries: 3, retryDelay: axiosRetry.exponentialDelay })

const router = createBrowserRouter(routes)

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <UserContextProvider>
      <RouterProvider router={router} />
    </UserContextProvider>
  </React.StrictMode>,
);
