import React, {useEffect} from "react";
import HomeLeftMenu from "../Componente/HomeLeftMenu/HomeLeftMenu";
import '../NetworkDimension/NetworkDimension.css'

function NetworkDimension() {

    useEffect(() => {
        getAmountOfUsers();
    }, []);

    let amount = 0;

    const getAmountOfUsers = async () => {
        await fetch("api/User/")
            .then(async res => {
                await res.json().then(
                    result => amount = Object.keys(result).length)
            })
        let amountOfPlayers = document.getElementById('amountOfPlayers');
        amountOfPlayers.innerText = amount;
    }

    return (
        <div className="networkDimension">
            <HomeLeftMenu/>
            <div className="networkDimensionPresentation">
                <div className="networkDimensionInfo">
                    <div className="container">
                        <div className="row">
                            <div className="col-md-3 col-sm-6">
                                <div className="counter">
                                    <div className="counter-icon">
                                        <i className="fa fa-user"/>
                                    </div>
                                    <span id="amountOfPlayers" className="counter-value"/>
                                    <h3>PLAYERS</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <footer className="rodapeTagsFromRelacoes"  style={{color:"white"}}>© 2021 Copyright: G4S - Social Network Game</footer>
            </div>
        </div>
    );
}

export default NetworkDimension;
