import React, { Component } from 'react'
import Table from "react-bootstrap/Table";
import {Button} from "reactstrap";

export default class TableWithSearchResults extends Component {


    render() {
        let change = (e) => {
            this.props.onChange(e);
        }
        return (<div>
                <Table >
                    <thead>
                    <tr>
                        <th style={{color:"white"}}>Email</th>
                        <th style={{color:"white"}}>Username</th>
                    </tr>
                    </thead>
                    <tbody>
                    {Array.from(this.props.users).map(function(item) {
                        return (<tr>
                            <td style={{color:"white"}}>{item.email}</td>
                                <td style={{color:"white"}}>{item.username}
                                    <Button onClick={() => change(item)} className="fa fa-paper-plane"  outline
                                            color="success">Send Connection Request </Button>
                                </td>
                        </tr>)
                    })
                    }
                    </tbody>
                </Table>
            </div>
        )
    }
}