import { makeFailure, makeSuccess, State } from "@model/state";
import User from "@model/user";
import axios from "axios";
import { createContext, ReactNode, useEffect, useState } from "react";

export const UserContext = createContext<State<User, Error> | null>(null);

function UserContextProvider(props: { children: ReactNode }) {
    const [user, setUserState] = useState<State<User, Error> | null>(null);

    useEffect(() => {
        const updateAuthenticationStatus = async () => {
            try {
                const response = await axios.get("/api/account/ping-auth");
                if (response.status == 200) {
                    const userData = response.data
                    console.log("User data: ", userData)
                    setUserState(makeSuccess({
                        email: userData.email,
                        userName: userData.userName
                    }))
                }
            } catch (error: any) {
                setUserState(makeFailure(error))
                console.error(error)
            }
        }
        updateAuthenticationStatus();
    }, [])

    return (<UserContext.Provider value={user}>
        {props.children}
    </UserContext.Provider>)
}

export default UserContextProvider;