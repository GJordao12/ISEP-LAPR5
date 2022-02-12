import React, {Component, useEffect, useState} from 'react'
import Table from 'react-bootstrap/Table'
import './TagsFromRelacoesPropio.css'
import Footer from "../Componente/PageConstructor/Footer";
import Header2 from "../Componente/PageConstructor/Header2";

function TagsFromRelacoesPropio() {

    useEffect(() => {
        getTags();
    }, [])

    const [listOjbects, setObjects] = useState([]);


    const getTags = async () => {
        const items = await fetch("api/Tag/Connections/{id}", {})
        const tags = await items.json();

        setObjects(tags)

        return tags;
    }

    return (
        <div>
            <Header2/>
            <div className="tabelaTagsRelacoesBackground">
            <div className="tabelaTagsRelacoesPropio" >
                    <center><h3 style={{color:"white"}}>Tag Cloud(Relations):</h3></center>
                <Table striped bordered hover >
                    <thead>
                    <tr>
                        <th style={{color:"white"}}>Tag Names</th>
                    </tr>
                    </thead>
                    <tbody>
                    {listOjbects.map(function (item) {
                        return (<tr>
                                <td style={{color:"white"}}>
                                    {item.nome}
                                </td>
                            </tr>
                        )
                    })}
                    </tbody>
                </Table>
            </div>
                </div>
            <Footer/>
        </div>
    )
}

export default TagsFromRelacoesPropio;