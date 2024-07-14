import {RouteObject} from "react-router-dom";
import Login from "@pages/login/Login.tsx";
import Register from "@pages/register/Register.tsx";
import Home from "@pages/home/Home.tsx";

export const Path = {
    Home: "/",
    Login: "/login",
    Register: "/register"
}

export const routes: RouteObject[] = [
    {
        path: Path.Home,
        element: <Home />
    },
    {
        path: Path.Login,
        element: <Login />
    },
    {
        path: Path.Register,
        element: <Register />
    }
]
