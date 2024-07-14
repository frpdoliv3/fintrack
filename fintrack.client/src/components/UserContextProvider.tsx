import { makeFailure, makeSuccess, Result } from "@utils/result";
import axios from "axios";
import { createContext, ReactNode, useEffect, useState } from "react";
import { User, UserContextState } from "src/types";

export const UserContext = createContext<UserContextState | null>(null);

function UserContextProvider(props: { children: ReactNode }) {
    const updateAuthenticationStatus = async () => {
        try {
            const response = await axios.get("/api/account/ping-auth");
            if (response.status == 200) {
                const userData = response.data
                setUserState(makeUserState(makeSuccess({
                    email: userData.email,
                    userName: userData.userName
                })))
            }
        } catch (error: any) {
            setUserState(makeUserState(makeFailure(error)))
            console.error(error)
        }
    }
    const makeUserState = (user: Result<User, Error> | null) => ({
        user: user,
        updateUser: updateUser
    })
    const updateUser = () => {
        setUserState(noUserState)
        updateAuthenticationStatus()
    }
    const noUserState = {
        user: null,
        updateUser: updateUser
    }
    
    const [user, setUserState] = useState<UserContextState>(noUserState);

    useEffect(() => {
        updateAuthenticationStatus();
    }, [])

    return (<UserContext.Provider value={user}>
        {props.children}
    </UserContext.Provider>)
}

export default UserContextProvider;