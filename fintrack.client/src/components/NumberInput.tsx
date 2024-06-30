import {ChangeEvent, forwardRef, WheelEvent} from "react";
import {Form, FormControlProps} from "react-bootstrap";
import {isString} from "@utils/validation.ts";

const NumberInput = forwardRef((props: FormControlProps, ref) => {
    const {
        onChange,
        className,
        onWheel,
        type,
        ...otherProps
    } = props;

    const noScrollOnWheel = (e: WheelEvent<HTMLInputElement>) => {
        if (e.target instanceof HTMLElement) {
            e.target.blur();
        }
    }

    const onChangeFiltered = (e: ChangeEvent<HTMLInputElement>) => {
        const newValue = e.target.value;
        const newestChar = newValue[newValue.length - 1]
        let filteredValue = ""
        if (newestChar < '0' || newestChar > '9') {
            filteredValue = newValue.slice(0, newestChar.length - 2)
        } else {
            filteredValue = newValue
        }
        if (onChange) {
            e.target.value = filteredValue
            onChange(e)
        }
    }

    if (isString(props.value)) {
        const value: string = props.value as string
        for (const c of value) {
            if (c < '0' || c > '9') {
                throw new Error(`Invalid ${value} must be number`)
            }
        }
    }
    return (<Form.Control
        type="text"
        ref={ref}
        className={className}
        onChange={onChangeFiltered}
        onWheel={noScrollOnWheel}
        {...otherProps} />)
})

export default NumberInput;
