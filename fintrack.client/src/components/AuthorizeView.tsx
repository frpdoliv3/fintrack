import {createContext, ReactNode, useEffect, useState} from "react";
import axios from "axios";
import {useNavigate} from "react-router-dom";

interface User {
    email: string
}

const UserContext = createContext({});

function AuthorizeView(props: { children: ReactNode }) {
    const navigate = useNavigate();
    const [authorized, setAuthorized] = useState<boolean>(false);
    const [loading, setLoading] = useState<boolean>(true);
    let emptyUser: User = { email: "" }; 
    
    const [user, setUser] = useState(emptyUser);
    
    const updateAuthorizationStatus = async () => {
        try {
            const response = await axios.get("/api/account/ping-auth");
            if (response.status == 204) {
                let userData = response.data
                setUser({ email: userData.email })
                setAuthorized(true);
            }
        } catch (error: any) {
            console.log(error)
        } finally {
            setLoading(false);
        }
    }
    
    useEffect(() => {
        updateAuthorizationStatus()
    }, []);
    if (loading) {
        return (<p>Loading...</p>)
    }
    if (authorized && !loading) {
        return <UserContext.Provider value={user}>{props.children}</UserContext.Provider>
    }
    navigate("/login")
    return <></>
}

export default AuthorizeView;
