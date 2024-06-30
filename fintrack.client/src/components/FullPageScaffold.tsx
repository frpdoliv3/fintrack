import {ReactNode} from "react";
import {Link} from "react-router-dom";

function FullPageScaffold(props: { children: ReactNode }) {
    return (<>
        <header className="p-4 text-center brand-name">
            <h1><Link className="clean-link" to={"/"}>FinTrack</Link></h1>
        </header>
        <main className="d-flex justify-content-center">{props.children}</main>
    </>)
}

export default FullPageScaffold;
