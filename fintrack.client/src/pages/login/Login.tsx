import axios, {AxiosError} from "axios";
import FullPageScaffold from "@components/FullPageScaffold.tsx";
import {Button, Form} from "react-bootstrap";
import {useNavigate} from "react-router-dom";
import styles from "./Login.module.css";
import {Formik} from "formik";
import {z} from "zod";
import {isInvalid, makeValidationFunction} from "@utils/validation.ts";
import {ChangeEvent, useState} from "react";
import {Path} from "@navigation/routes.tsx";
import UnauthorizedView from "@components/UnauthorizedView";

function Login() {
    const navigate = useNavigate()
    const loginSchema = z.object({
        identity: z.string(),
        password: z.string()
    })
    const [loginError, setLoginError] = useState("");
    type FormValues = z.infer<typeof loginSchema>;
    const onLogin = async (values: FormValues) => {
        try {
            const response = await axios.post("/api/account/login", values)
            if (response.status == 200) {
                navigate(Path.Home);
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
    
    return (<UnauthorizedView>
        <FullPageScaffold>
            <div id={styles.mainDiv}>
                <Formik
                    initialValues = {{
                        identity: "",
                        password: ""
                    }}
                    validate={validationFn}
                    onSubmit={onLogin}
                    validateOnChange={false}
                    validateOnBlur={true}>
                    {({ handleSubmit, handleChange, values, errors }) => (
                        <Form onSubmit={handleSubmit} noValidate>
                            <Form.Group>
                                <Form.Label>Username or Email Address:</Form.Label>
                                <Form.Control
                                    type="text"
                                    placeholder="Enter identity"
                                    name="identity"
                                    value={values.identity}
                                    onChange={handleChangeWrapper(handleChange)}
                                    isInvalid={isInvalid(errors.identity)} />
                                <Form.Control.Feedback type="invalid">
                                    {errors.identity}
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
                            <div className="pt-4 text-end">
                                <Button 
                                    variant="outline-light"
                                    onClick={() => navigate(Path.Register)}
                                    className="me-2">
                                    Register
                                </Button>
                                <Button variant="primary" type="submit">Login</Button>
                            </div>
                        </Form>
                    )}
                </Formik>
            </div>
        </FullPageScaffold>
    </UnauthorizedView>)
}

export default Login;
