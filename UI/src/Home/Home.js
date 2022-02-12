import './Home.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import HomeLeftMenu from '../Componente/HomeLeftMenu/HomeLeftMenu'
import logo from './logo.png';

function Home() {
    return (
        <div className="home">
            <HomeLeftMenu/>
            <div className="presentationHome">
                <div className="pageTitleHome">
                    <header><strong style={{color:"white"}}>Social Network Game</strong></header>
                </div>
                <div className="gameInfoHome">
                    <p className="logotipoHome" style={{color:"white"}}>
                        Our game simulates a social network where the objective is to expand <br/>
                        your friendship network as much as possible, <br/>
                        with the objective of having the biggest and strongest social network possible. <br/>
                        You'll face many missions, of different levels, <br/>
                        where you can earn points and climb the leaderboard. <br/>
                        Join us!
                    </p>
                    <center><img className="logoHome" src={logo} alt="centered image"/></center>
                </div>
                <footer className="rodapeHome" style={{color:"white"}}>© 2021 Copyright: G4S - Social Network Game</footer>
            </div>
        </div>
    );
}

export default Home;
