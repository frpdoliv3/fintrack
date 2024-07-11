import { ReactNode, useContext } from "react";
import { UserContext } from "./UserContextProvider";
import { Navigate } from "react-router-dom";
import { Path } from "@navigation/routes";

function UnauthorizedView(props: { children: ReactNode }) {
    const userState = useContext(UserContext)

    if (userState?.success) {
        return <Navigate to={Path.Home} replace={true}/>
    }
    return props.children
}

export default UnauthorizedView;