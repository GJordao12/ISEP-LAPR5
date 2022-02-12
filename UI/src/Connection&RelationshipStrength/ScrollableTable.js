import React, {Component, useEffect, useState} from 'react'
import './ScrollableTable.css'
import './Ligacoes.css'
import Table from 'react-bootstrap/Table'
import Footer from "../Componente/PageConstructor/Footer";

function Ligacoes() {

    useEffect(() => {
        getLigacoes();
    }, [])

    const [listOjbects, setObjects] = useState([]);

    function DTO(ligacao, user2) {
        this.user2 = user2.username;
        this.fl = ligacao.forcaLigacao.valueOf();
        this.fr = ligacao.forçaRelacao.valueOf();
    }

    const getLigacoes = async () => {
        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);
        const items = await fetch("api/Ligacao/user=" + userToken, {})
        const ligacoes = await items.json();
        console.log(ligacoes.toString())

        let temp = [];
        for (const l of ligacoes) {
            let user2 = await fetch('api/User/'.concat(l.secundario.value), {})
            let userr2= await user2.json();
            console.log(l.forcaLigacao.value)
            temp.push(new DTO(l, userr2))
        }
        setObjects(temp)
        console.log(temp.at(0))
        return temp;
    }

        return (
            <div>
                <div className="tabelaLigacoes">
                <center><h3 style={{color:"white"}}>Connection and Relationship Strengths:</h3></center>
                <Table striped bordered hover>
                    <thead>
                    <tr>
                        <th style={{color:"white"}}>User you are connected to</th>
                        <th style={{color:"white"}}>Connection Strength</th>
                        <th style={{color:"white"}}>Relationship Strength</th>
                    </tr>
                    </thead>
                    <tbody>
                    {listOjbects.map(function (item) {
                        return (<tr>
                                <td style={{color:"white"}}>
                                    {item.user2}
                                </td>
                                <td style={{color:"white"}}>
                                    {item.fl}
                                </td>
                                <td style={{color:"white"}}>
                                    {item.fr}
                                </td>
                            </tr>
                        )
                    })}
                    </tbody>
                </Table>
                </div>
                <Footer/>
            </div>
        )
}

export default Ligacoes;
