import {ChangeEvent, FormEvent, useState} from "react";
import axios from "axios";

function Login() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    
    const updateEmail = (e: ChangeEvent<HTMLInputElement>) => setEmail(e.currentTarget.value)
    const updatePassword = (e: ChangeEvent<HTMLInputElement>) => setPassword(e.currentTarget.value)
    const onLogin = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault()
        await axios.post("/api/login?useSessionCookies=true", {
            email,
            password,
        })
    }
    
    return (<main>
        <form onSubmit={onLogin}>
            <input value={email} onChange={updateEmail}/>
            <input value={password} onChange={updatePassword}/>
            <button>Login</button>
        </form>
    </main>)
}

export default Login;
