import FullPageScaffold from "@components/FullPageScaffold.tsx";
import styles from "./Register.module.css";
import {Formik} from "formik";
import {Button, Form} from "react-bootstrap";
import axios from "axios";
import {useNavigate} from "react-router-dom";
import {z} from "zod";
import {isInvalid, makeValidationFunction} from "@utils/validation.ts";
import {Path} from "@navigation/routes.tsx";
import UnauthorizedView from "@components/UnauthorizedView";

function Register() {
    const navigator = useNavigate();
    const registerSchema = z.object({
        email: z.string().email("Must be a valid email address"),
        password: z.string(),
        username: z.string()
    })
    type FormValues = z.infer<typeof registerSchema>;
    const validationFn = makeValidationFunction(registerSchema);
    const handleRegister = async (values: FormValues) => {
        try {
            const response = await axios.post("/api/account/register", {
                email: values.email,
                password: values.password,
                userName: values.username
            })
            if (response.status == 200) {
                navigator(Path.Home)
            }
        } catch (e: any) {
            console.error(e)
        }
    }
    
    return (<UnauthorizedView>
        <FullPageScaffold>
            <div id={styles.mainDiv}>
                <Formik 
                    initialValues={{
                        email: "",
                        password: "",
                        username: ""
                    }} 
                    onSubmit={handleRegister}
                    validate={validationFn}
                    validateOnChange={false}
                    validateOnBlur={true}>
                    {({ handleSubmit, handleChange, values, errors }) => (
                        <Form onSubmit={handleSubmit} noValidate>
                            <fieldset className={styles.formSection}>
                                <legend className="float-none w-auto px-1">Credentials</legend>
                                <Form.Group>
                                    <Form.Label>Username:</Form.Label>
                                    <Form.Control
                                        type="text"
                                        placeholder="Enter username"
                                        name="username"
                                        value={values.username}
                                        onChange={handleChange}
                                        isInvalid={isInvalid(errors.username)} />
                                    <Form.Control.Feedback type="invalid">
                                        {errors.username}
                                    </Form.Control.Feedback>
                                </Form.Group>
                                <Form.Group>
                                    <Form.Label>Email Address:</Form.Label>
                                    <Form.Control
                                        type="email"
                                        placeholder="Enter email"
                                        name="email"
                                        value={values.email}
                                        onChange={handleChange} 
                                        isInvalid={isInvalid(errors.email)} />
                                    <Form.Control.Feedback type="invalid">
                                        {errors.email}
                                    </Form.Control.Feedback>
                                </Form.Group>
                                <Form.Group>
                                    <Form.Label>Password</Form.Label>
                                    <Form.Control
                                        type="password"
                                        placeholder="Enter password"
                                        name="password"
                                        value={values.password}
                                        onChange={handleChange}
                                        isInvalid={isInvalid(errors.password)} />
                                    <Form.Control.Feedback type="invalid">
                                        {errors.password}
                                    </Form.Control.Feedback>
                                </Form.Group>
                            </fieldset>
                            <div className="d-flex justify-content-end pt-2">
                                <Button type="submit" className="px-3">Submit</Button>
                            </div>
                        </Form>
                    )}
                </Formik>
            </div>
        </FullPageScaffold>
    </UnauthorizedView>)
}

export default Register;