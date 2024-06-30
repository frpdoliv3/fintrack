import {Form} from "react-bootstrap";
import NumberInput from "@components/NumberInput.tsx";
import {ChangeEvent, CSSProperties, useEffect, useState} from "react";
import {useFocus} from "@utils/hooks.ts";
import {isNumeric} from "@utils/validation.ts";

interface ZipCodeFieldProps {
    onChange(e: ChangeEvent<HTMLInputElement>): void,
    name: string | undefined,
    value: string | undefined
} 

function filterComponents(props: ZipCodeFieldProps) {
    if (!props.value || props.value == "") {
        return ["", ""];
    }
    const splitPlace = props.value.indexOf("-")
    if (splitPlace >=0) {
        const codeValue = props.value.slice(0, splitPlace)
        const postalZoneValue = props.value.slice(splitPlace + 1)
        if (!isNumeric(codeValue) || ! isNumeric(postalZoneValue) || codeValue[0] == "0") {
            console.error("Invalid zip code value. It is a really bad idea to enforce a ZipCode value when using the custom component")
            return ["", ""];
        }
        return [codeValue, postalZoneValue]
    } else {
        console.error("Invalid zip code value. It is a really bad idea to enforce a ZipCode value when using the custom component")
        return ["", ""];
    }
}

function ZipCodeField(props: ZipCodeFieldProps) {
    const [postalDesignation, setPostalDesignation] = useState("")
    const [codeRef, setCodeFocus] = useFocus()
    const [postalZoneRef, setPostalZoneFocus] = useFocus()
    const [code, postalZone] = filterComponents(props);
    
    const setCode = (e: ChangeEvent<HTMLInputElement>) => {
        e.target.value = `${e.target.value}-${postalZone}`
        props.onChange(e)
    }
    const setPostalZone = (e: ChangeEvent<HTMLInputElement>) => {
        e.target.value = `${code}-${e.target.value}`
        props.onChange(e)
    }
    
    const onChangeCode = (e: ChangeEvent<HTMLInputElement>) => {
        const newValue = e.target.value
        if (newValue.length == 1 && newValue[0] == '0') {
            e.target.value = ""
            setCode(e);
            return;
        }
        if (newValue.length <= 4) {
            setCode(e);
        }
        if (newValue.length == 4) {
            setPostalZoneFocus();
        }
        if (newValue.length > 4) {
            setPostalZoneFocus()
            e.target.value = postalZone + newValue[newValue.length - 1]
            onChangePostalZone(e)
        }
    }
    
    const onChangePostalZone = (e: ChangeEvent<HTMLInputElement>) => {
        const newValue = e.target.value
        if (newValue.length <= 3) {
            setPostalZone(e);
        }
        if (newValue.length == 0) {
            setCodeFocus();
        }
    }
    
    const codeStyle: CSSProperties = {
        width: "4rem"
    }
    
    const postalZoneStyle: CSSProperties = {
        width: "3.5rem"
    }

    useEffect(() => {
        if (code.length == 4 && postalZone.length == 3) {
            fetchPostalDesignation()
        } else {
            setPostalDesignation("")
        }
    }, [code, postalZone]);
    
    const fetchPostalDesignation = async () => {
        /*try {
            const result = await axios
                .get(`https://json.geoapi.pt/codigo_postal/${code}-${postalZone}`)
            if (result.status != 200) {
                return;
            }
            if (result.data['Designação Postal']) {
                setPostalDesignation(result.data['Designação Postal'])
            }
        } catch (e: any) {
            console.error(e)
            return;
        }*/
        setPostalDesignation("LEIRIA")
    }
    
    return (<Form.Group>
        <Form.Label>Zip Code:</Form.Label>
        <div className="d-flex">
            <div style={{marginRight: "12px"}}>
                <NumberInput
                    ref = {codeRef}
                    value={code}
                    style={codeStyle}
                    name={props.name}
                    onChange={onChangeCode} />
            </div>
            <div style={{marginRight: "12px"}}>
                <NumberInput
                    ref = {postalZoneRef}
                    value={postalZone}
                    style={postalZoneStyle}
                    name={props.name}
                    onChange={onChangePostalZone} />
            </div>
            <Form.Control
                value={postalDesignation}
                disabled />
        </div>
    </Form.Group>)    
}

export default ZipCodeField;
