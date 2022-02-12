import React, {useEffect, useState} from 'react'
import Table from 'react-bootstrap/Table'
import './TagsFromRelacoes.css'

function TagsFromRelacoesTable() {

    useEffect(() => {
        getTags();
    }, [])

    const [listOjbects, setObjects] = useState([]);


    const getTags = async () => {
        const items = await fetch("api/Tag/CloudAllConnections", {})
        const tags = await items.json();

        setObjects(tags)

        return tags;
    }

    return (
        <div >
            <div className="tabelaTagsRelacoes">
                <div className="tabelaTagsRelacoesTitle">
                    <center><h3 style={{color:"white"}}>Tag Cloud (Relations):</h3></center>
                </div>
                <Table striped bordered hover>
                    <thead>
                    <tr>
                        <th style={{color:"white"}}>Tag Names</th>
                        <th style={{color:"white"}}>Occurrences</th>
                    </tr>
                    </thead>
                    <tbody>
                    {Object.entries(listOjbects).map(([key,value])=> {
                        return (<tr>
                                <td style={{color:"white"}}>
                                    {key}
                                </td>
                                <td style={{color:"white"}}>
                                    {value}
                                </td>
                            </tr>
                        )
                    })}
                    </tbody>
                </Table>
            </div>
            <footer className="rodapeTagsFromRelacoes"  style={{color:"white"}}>© 2021 Copyright: G4S - Social Network Game</footer>
        </div>
    )
}

export default TagsFromRelacoesTable;