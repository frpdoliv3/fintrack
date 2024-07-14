import AuthorizedView from "@components/AuthorizedView.tsx"
import Avatar from "@components/Avatar";

function Home() {
    return (
        <AuthorizedView>
            <div>
                <Avatar email="frpdoliv@gmail.com" name="Francisco Oliveira"/>
            </div>
            <main>

            </main>
        </AuthorizedView>
    );
}

export default Home;