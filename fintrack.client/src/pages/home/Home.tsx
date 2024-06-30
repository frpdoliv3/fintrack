import AuthorizeView from "@components/AuthorizeView.tsx";

function Home() {
    return (
        <AuthorizeView>
            <p>Hello world!</p>
        </AuthorizeView>
    );
}

export default Home;