import './LeaderBoards.css'
import React, {useEffect, useState} from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import Header from '../Componente/PageConstructor/Header'
import Footer from "../Componente/PageConstructor/Footer";
import Header2 from "../Componente/PageConstructor/Header2";

function ShowLeaderBoards(){

    useEffect( () => {
        getLeaderBoards();
    },[]);

    const [listFortaleza, setListaFortaleza] = useState([]);
    const [listDimensao, setListaDimensao] = useState([]);

    const getLeaderBoards = async () => {

        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);

        const items = await fetch('api/Ligacao/LeaderBoard/Fortaleza/'.concat(userToken));
        const fortaleza = await items.json();
        setListaFortaleza(fortaleza);

        const items2 = await fetch('api/Ligacao/LeaderBoard/Dimensao/'.concat(userToken));
        const dimensao = await items2.json();
        setListaDimensao(dimensao);
    }

    function changeColor(){

        const tokenString = sessionStorage.getItem('userName');
        const userToken = JSON.parse(tokenString);

        var style = document.createElement('style');
        style.innerHTML = `.` + userToken + `{ background-color: #808080;color:#000000 }`;
        document.head.appendChild(style);

    }

    return(
        <div>
            <Header2/>
            <div className="postsBackground">
            <div className="centrar">
            <div className="primeira_tabela">
                <center><h4 style={{color:"white"}}> LeaderBoard Network Strength</h4></center><br/>
                <table className="table table-borderless tabela">
                    <thead className="table-header">
                        <tr>
                            <th scope="col"> Position </th>
                            <th scope="col"> Username </th>
                            <th scope="col"> Total Strength </th>
                        </tr>
                    </thead>
                    <tbody>
                    {listFortaleza.map(function (item){
                        console.log(item.username);
                        return(
                            <tr className={""+item.username}>
                                <td style={{color:"white"}}> {item.posicao + " º"} </td>
                                <td style={{color:"white"}}> {item.username} </td>
                                <td style={{color:"white"}}> {item.valor} </td>
                            </tr>
                        )
                    })
                    }
                    {
                        changeColor()
                    }
                    </tbody>
                </table>
            </div>
            <div className="segunda_tabela">
                <center><h4 style={{color:"white"}}> LeaderBoard Network Size</h4></center><br/>
                <table className="table table-borderless tabela">
                    <thead className="table-header">
                    <tr >
                        <th scope="col" > Position </th>
                        <th scope="col"> Username </th>
                        <th scope="col"> Total Size </th>
                    </tr>
                    </thead>
                    <tbody>
                    {listDimensao.map(function (item){
                        console.log(item.username);
                        return(
                            <tr className={""+item.username} >
                                <td className="coluna" style={{color:"white"}}> {item.posicao + " º"} </td>
                                <td style={{color:"white"}}> {item.username} </td>
                                <td style={{color:"white"}}> {item.valor} </td>
                            </tr>
                        )
                    })
                    }
                    {
                        changeColor()
                    }
                    </tbody>
                </table>
            </div>
            </div>
            </div>
            <Footer/>
        </div>
    )

}
export default ShowLeaderBoards;