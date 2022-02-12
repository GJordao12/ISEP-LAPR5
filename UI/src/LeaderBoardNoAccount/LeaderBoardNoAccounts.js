import '../LeaderBoard/LeaderBoards.css'
import './LeaderBoardNoAccounts.css'
import React, {useEffect, useState} from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import HomeLeftMenu from "../Componente/HomeLeftMenu/HomeLeftMenu";

function ShowLeaderBoardsNoAccount() {

    useEffect(() => {
        getLeaderBoards();
    }, []);

    const [listFortaleza, setListaFortaleza] = useState([]);
    const [listDimensao, setListaDimensao] = useState([]);

    const getLeaderBoards = async () => {
        const items = await fetch('api/Ligacao/LeaderBoard/Fortaleza/-1');
        const fortaleza = await items.json();
        setListaFortaleza(fortaleza);

        const items2 = await fetch('api/Ligacao/LeaderBoard/Dimensao/-1');
        const dimensao = await items2.json();
        setListaDimensao(dimensao);
    }

    return (
        <div className="LeaderBoardNoAccount">
            <HomeLeftMenu/>
            <div className="LeaderboardNoAccount1">
            <div className="LeaderBoardNoAccountInside">
                <div className="primeira_tabelaNoAccount">
                    <center><h4 style={{color:"white"}}> LeaderBoard Network Strength</h4></center>
                    <br/>
                    <table className="table">
                        <thead className="table-dark">
                        <tr>
                            <th scope="col" style={{color:"white"}}> Position</th>
                            <th scope="col" style={{color:"white"}}> Username</th>
                            <th scope="col" style={{color:"white"}}> Total Strength</th>
                        </tr>
                        </thead>
                        <tbody className="cortabela">
                        {listFortaleza.map(function (item) {
                            return (
                                <tr>
                                    <td style={{color:"white"}}> {item.posicao + " º"} </td>
                                    <td style={{color:"white"}}> {item.username} </td>
                                    <td style={{color:"white"}}> {item.valor} </td>
                                </tr>
                            )
                        })
                        }
                        </tbody>
                    </table>
                </div>
            </div>
            <div className="LeaderBoardNoAccountInside" >
                <div className="primeira_tabelaNoAccount" >
                    <center><h4 style={{color:"white"}}> LeaderBoard Network Size</h4></center>
                    <br/>
                    <table className="table" >
                        <thead className="table-dark">
                        <tr>
                            <th scope="col" style={{color:"white"}}> Position</th>
                            <th scope="col" style={{color:"white"}}> Username</th>
                            <th scope="col" style={{color:"white"}}> Total Size</th>
                        </tr>
                        </thead>
                        <tbody className="cortabela">
                        {listDimensao.map(function (item) {
                            console.log(item.username);
                            return (
                                <tr>
                                    <td style={{color:"white"}}> {item.posicao + " º"} </td>
                                    <td style={{color:"white"}}> {item.username} </td>
                                    <td style={{color:"white"}}> {item.valor} </td>
                                </tr>
                            )
                        })
                        }
                        </tbody>
                    </table>
                </div>
            </div>
            </div>
            <footer className="rodapeLeaderBoard"  style={{color:"white"}}>© 2021 Copyright: G4S - Social Network Game</footer>
        </div>
    )

}

export default ShowLeaderBoardsNoAccount;