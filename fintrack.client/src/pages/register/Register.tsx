import FullPageScaffold from "@components/FullPageScaffold.tsx";
import styles from "./Register.module.css";
import {Formik} from "formik";
import {Button, Form} from "react-bootstrap";
import ZipCodeField from "@components/ZipCodeField.tsx";

function Register() {
    const handleRegister = (values: any) => {
        console.log(values)
    }
    
    return (<FullPageScaffold>
        <div id={styles.mainDiv}>
            <Formik 
                initialValues={{
                    email: "",
                    password: "",
                    addressFirstLine: "",
                    addressSecondLine: "",
                    zipCode: ""
                }} 
                onSubmit={handleRegister}
                validateOnChange={false}
                validateOnBlur={true}>
                {({ handleSubmit, handleChange, values, errors }) => (
                    <Form onSubmit={handleSubmit} noValidate>
                        <fieldset className={styles.formSection}>
                            <legend className="float-none w-auto px-1">Credentials</legend>
                            <Form.Group>
                                <Form.Label>Email Address:</Form.Label>
                                <Form.Control
                                    type="email"
                                    placeholder="Enter email"
                                    name="email"
                                    value={values.email}
                                    onChange={handleChange}/>
                            </Form.Group>
                            <Form.Group>
                                <Form.Label>Password</Form.Label>
                                <Form.Control
                                    type="password"
                                    placeholder="Enter password"
                                    name="password"
                                    value={values.password}
                                    onChange={handleChange}/>
                            </Form.Group>
                        </fieldset>
                        <fieldset className={styles.formSection}>
                            <legend className="float-none w-auto px-1">Address</legend>
                            <Form.Group>
                                <Form.Label>Line 1</Form.Label>
                                <Form.Control
                                    type="text"
                                    placeholder="Enter the address first line"
                                    name="addressFirstLine"
                                    value={values.addressFirstLine}
                                    onChange={handleChange}/>
                            </Form.Group>
                            <Form.Group>
                                <Form.Label>Line 2</Form.Label>
                                <Form.Control
                                    type="text"
                                    placeholder="Enter the address second line"
                                    name="addressSecondLine"
                                    value={values.addressSecondLine}
                                    onChange={handleChange}/>
                            </Form.Group>
                            <ZipCodeField 
                                name="zipCode"
                                value={values.zipCode}
                                onChange={handleChange} />
                        </fieldset>
                        <div className="d-flex justify-content-end pt-2">
                            <Button type="submit" className="px-3">Submit</Button>
                        </div>
                    </Form>
                )}
            </Formik>
        </div>
    </FullPageScaffold>)
}

export default Register;