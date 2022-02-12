import React, {useEffect, useState} from 'react'
import Table from 'react-bootstrap/Table'
import './TagsFromUsers.css'

function TagsFromUsersTable() {

    useEffect(() => {
        getTags();
    }, [])

    const [listOjbects, setObjects] = useState([]);


    const getTags = async () => {
        const items = await fetch("api/Tag/CloudAllUsers", {})
        const tags = await items.json();

        setObjects(tags)

        return tags;
    }

    return (
        <div>
            <div className="tabelaTagsUsers">
                <div className="tabelaTagsUsersTitle">
                <center><h3 style={{color:"white"}}>Tag Cloud (Users):</h3></center>
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
            <footer className="rodapeTagsFromUsers"  style={{color:"white"}}>© 2021 Copyright: G4S - Social Network Game</footer>
        </div>
    )
}

export default TagsFromUsersTable;