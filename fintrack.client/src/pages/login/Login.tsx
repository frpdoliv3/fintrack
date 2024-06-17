import axios, {AxiosError} from "axios";
import FullPageScaffold from "@components/FullPageScaffold.tsx";
import {Button, Form} from "react-bootstrap";
import {useNavigate} from "react-router-dom";
import styles from "./Login.module.css";
import {Formik} from "formik";
import {z} from "zod";
import {isInvalid, makeValidationFunction} from "@utils/validation.ts";
import {ChangeEvent, useState} from "react";

function Login() {
    const navigate = useNavigate()
    const loginSchema = z.object({
        email: z.string().email("Must be a valid email address"),
        password: z.string()
    })
    const [loginError, setLoginError] = useState("");
    type FormValues = z.infer<typeof loginSchema>;
    const onLogin = async (values: FormValues) => {
        try {
            const response = await axios.post("/api/login?useSessionCookies=true", values)
            if (response.status == 200) {
                navigate("/");
            }
        } catch (e: any) {
            if (e instanceof AxiosError) {
                if (e.response?.status == 401) {
                    setLoginError("Invalid email/password, please try again!");
                }
            } else {
                console.error(e)
            }
        }
    }
    const validationFn = makeValidationFunction(loginSchema)
    type HandleChangeCb = (e: ChangeEvent<any>) => void
    const handleChangeWrapper = (handleChange: HandleChangeCb) => {
        return (e: ChangeEvent<any>) => {
            setLoginError("");
            handleChange(e)
        }
    }
    
    return (<FullPageScaffold>
        <div id={styles.mainDiv}>
            <Formik
                initialValues = {{
                    email: "",
                    password: ""
                }}
                validate={validationFn}
                onSubmit={onLogin}
                validateOnChange={false}
                validateOnBlur={true}
            >
                {({ handleSubmit, handleChange, values, errors }) => (
                    <Form onSubmit={handleSubmit} noValidate>
                        <Form.Group>
                            <Form.Label>Email Address:</Form.Label>
                            <Form.Control
                                type="email"
                                placeholder="Enter email"
                                name="email"
                                value={values.email}
                                onChange={handleChangeWrapper(handleChange)}
                                isInvalid={isInvalid(errors.email)} />
                            <Form.Control.Feedback type="invalid">
                                {errors.email}
                            </Form.Control.Feedback>
                        </Form.Group>
                        <Form.Group className="pt-2">
                            <Form.Label>Password:</Form.Label>
                            <Form.Control
                                type="password"
                                placeholder="Enter password"
                                name="password"
                                value={values.password}
                                onChange={handleChangeWrapper(handleChange)} />
                        </Form.Group>
                        <Form.Control.Feedback 
                            type="invalid" 
                            style={{display: loginError.length > 0 ? "block": "none"}}>
                            {loginError}
                        </Form.Control.Feedback>
                        <div className="pt-2 text-end">
                            <Button 
                                variant="outline-light"
                                onClick={() => navigate("/register")}>
                                
                                Register
                            </Button>
                            <Button variant="primary" type="submit">Login</Button>
                        </div>
                    </Form>
                )}
            </Formik>
        </div>
    </FullPageScaffold>)
}

export default Login;
