import React, {Component, useEffect, useState} from 'react'
import Table from 'react-bootstrap/Table'
import './TagsFromUser.css'
import Footer from "../Componente/PageConstructor/Footer";

function TagsFromUserTable() {

    useEffect(() => {
        getTags();
    }, [])

    const [listOjbects, setObjects] = useState([]);


    const getTags = async () => {
        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);
        const items = await fetch("api/Tag/" + userToken, {})
        const tags = await items.json();

        setObjects(tags)

        return tags;
    }

    return (
        <div className="tagsFromUserBackGround">
            <div className="tabelaTagsUser">
            <center><h3 style={{color:"white"}}>Your Tag Cloud:</h3></center>
            <Table striped bordered hover>
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
            <Footer/>
        </div>
    )
}

export default TagsFromUserTable;