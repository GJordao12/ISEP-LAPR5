import React from "react";
import './HomeLeftMenu.css';
import 'bootstrap/dist/css/bootstrap.min.css';

function HomeLeftMenu() {
    return (
        <div className="homeLefMenu" style={{float: "left"}}>
            <div className="d-flex flex-column flex-shrink-0 p-3 text-white bg-dark"
                 style={{width: "60vh", minHeight: "100vh"}}>
                <ul className="nav nav-pills flex-column mb-auto">
                    <li className="corAzul">
                        <a href="/Home" className="center text-white letter corAzul" aria-current="page">Home</a>
                    </li>
                    <li className="corAzul">
                        <a href="/Login" className="center text-white letter corAzul" aria-current="page">Login</a>
                    </li>
                    <li className="corAzul" style={{paddingBottom:"2%"}}>
                        <a href="/CreateAccount" className="center text-white letter corAzul" aria-current="page">Create Account</a>
                    </li>
                    <li className="corAzul" style={{paddingBottom:"2%"}}>
                        <a href="/ShowLeaderBoardsNoAccount" className="center text-white letter corAzul" aria-current="page">LeaderBoard</a>
                    </li>
                    <li className="corAzul" style={{paddingBottom:"2%"}}>
                        <a href="/TagsFromUsers" className="center text-white letter corAzul" aria-current="page">Tag Cloud (Users)</a>
                    </li>
                    <li className="corAzul" style={{paddingBottom:"2%"}}>
                        <a href="/TagsFromRelacoes" className="center text-white letter corAzul" aria-current="page">Tag Cloud (Relations)</a>
                    </li>
                    <li className="corAzul">
                        <a href="/NetworkDimension" className="center text-white letter corAzul" aria-current="page">Network Dimension</a>
                    </li>
                </ul>
            </div>
        </div>
    );
}

export default HomeLeftMenu;
