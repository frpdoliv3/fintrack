import AuthorizedView from "@components/AuthorizedView.tsx"

function Home() {
    return (
        <AuthorizedView>
            <p>Home</p>
        </AuthorizedView>
    );
}

export default Home;