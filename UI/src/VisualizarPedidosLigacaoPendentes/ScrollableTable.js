import React, {Component, useEffect, useState} from 'react'
import './ScrollableTable.css'
import Table from 'react-bootstrap/Table'
import Footer from "../Componente/PageConstructor/Footer";

function ScrollableTablePedidoLigacao() {

    useEffect(() => {
        getPedidos();
    }, [])

    const [listOjbects, setObjects] = useState([]);

    function DTO(pedido, user) {
        this.from = user.username;
        this.text = pedido.texto;
    }

    const getPedidos = async () => {
        console.log(sessionStorage.getItem('token'))
        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);
        const items = await fetch("api/PedidoLigacao/pendentes/" + userToken, {})
        const pedidos = await items.json();

        let temp = [];
        for (const l of pedidos) {
            let user = await fetch('api/User/'.concat(l.remetente), {})
            let userr= await user.json();
            temp.push(new DTO(l,userr))
        }
        setObjects(temp)
        return temp;
    }

        return (
            <div>
                <div className="titleTable">
                <center> <h3 style={{color:"white"}}>Pending connection requests:</h3> </center>
                </div>
                <div className="tab">
                <Table striped bordered hover >
                    <thead>
                    <tr>
                        <th style={{color:"white"}}>Sender</th>
                        <th style={{color:"white"}}>Presentation Text</th>
                    </tr>
                    </thead>
                    <tbody>
                    {listOjbects.map(function (item) {
                        return (<tr>
                                <td style={{color:"white"}}>
                                    {item.from}
                                </td>
                                <td style={{color:"white"}}>
                                    {item.text}
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

export default ScrollableTablePedidoLigacao;
