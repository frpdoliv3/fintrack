import {z, ZodError, ZodObject, ZodRawShape} from "zod";

export function makeValidationFunction<T extends ZodRawShape>(validationSchema: ZodObject<T>) {
    type FormValues = z.infer<typeof validationSchema>;
    return (values: FormValues) => {
        try {
            validationSchema.parse(values);
        } catch (error) {
            if (error instanceof ZodError) {
                return error.formErrors.fieldErrors;
            }
        }
    }
}

export function isInvalid(fieldError: string | undefined) {
    return fieldError != undefined && fieldError.length > 0
}

export function isString(value: any) {
    return Object.prototype.toString.call(value) === "[object String]"
}

export function isNumeric(value: string) {
    for (let c of value) {
        if (c < '0' || c > '9') {
            return false;
        }
    }
    return true;
}
