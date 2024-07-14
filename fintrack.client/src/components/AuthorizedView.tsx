import {ReactNode, useContext} from "react";
import {Navigate} from "react-router-dom";
import { UserContext } from "./UserContextProvider";

function AuthorizedView(props: { children: ReactNode }) {
    const userState = useContext(UserContext);
    if (userState!.user == null) {
        return (<p>Loading...</p>)
    }
    if (userState!.user.success) {
        return props.children
    }
    return <Navigate to="/login" replace={true} />
}

export default AuthorizedView;
