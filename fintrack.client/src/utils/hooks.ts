import {MutableRefObject, useRef} from "react";

export function useFocus(): [MutableRefObject<HTMLElement | null>, () => void] {
    const htmlElRef: MutableRefObject<HTMLElement | null> = useRef(null)
    const setFocus = () => {htmlElRef.current && htmlElRef.current.focus()}

    return [ htmlElRef, setFocus ]
}
