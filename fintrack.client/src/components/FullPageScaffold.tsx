import {ReactNode} from "react";

function FullPageScaffold(props: { children: ReactNode }) {
    return (<>
        <header className="p-4 text-center brand-name">
            <h1>FinTrack</h1>
        </header>
        <main className="d-flex justify-content-center">{props.children}</main>
    </>)
}

export default FullPageScaffold;
