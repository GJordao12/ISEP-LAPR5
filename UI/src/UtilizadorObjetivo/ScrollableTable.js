import React, {Component} from 'react'
import './ScrollableTable.css'
import Table from 'react-bootstrap/Table'
import {Button} from 'reactstrap';
import {Form} from "react-bootstrap";
import 'font-awesome/css/font-awesome.min.css';

class ScrollableTableUtilizadorObjetivo extends Component {
    constructor(props) {
        super(props);
        this.state = {
            'data': [],
            'ids': [],
            'names': [],
            visibleTable: false,
            visibleForm: false,
            nTags: 1,
            textToSend: '',
            id: ''
        };
    }

    async getUsers(tags) {
        await fetch("/prolog/base_conhecimento");
        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);
        await fetch("/prolog/utilizador_objetivo?tag=" + tags + "&id='" + userToken + "'")
            .then(async res => await res.json())
            .then(async result => this.setState({'data': result})
            );
        let listName = []
        let listIds = []
        for (let i = 0; i < this.state.data.length; i++) {
            if (i % 2 === 0) {
                listIds.push(this.state.data[i]);
            } else {
                listName.push(this.state.data[i]);
            }
        }
        this.setState({ids: listIds});
        this.setState({names: listName});
        console.log(this.state.ids)
    }

    render() {
        let change = (item, index) => {
            this.setState({visibleForm: true, id: this.state.ids[index]})

        }

        let change2 = () => {
            this.setState({visibleForm: false});
            sendConnectionRequest(this.state.id, this.state.textToSend);
        }

        let change3 = (tags) => {
            this.getUsers(tags);
            this.setState({visibleTable: !this.state.visibleTable});
        }


        function sendConnectionRequest(id, textToSend) {
            const tokenString = sessionStorage.getItem('token');
            const userToken = JSON.parse(tokenString);
            console.log(userToken)
            console.log(id)
            console.log(textToSend)
            fetch('api/PedidoLigacao/', {
                method: 'POST',
                headers: {
                    'Content-type': 'application/json; charset=UTF-8' // Indicates the content
                },
                body: JSON.stringify({
                    "remetente": userToken,
                    "destinatario": id,
                    "texto": textToSend
                })
            })
                .then(response => response.json())
                .then(data => console.log(data))
                .catch(err => console.log(err))
        }

        return (
            <div className="container1">
                    <div className = "formNumTagsObjectiveUser">
                        <Form value={this.state.nTags}
                              onChange={event => this.setState({nTags: event.target.value})}>
                            <Form.Group controlId="formNumber">
                                <Form.Label  style={{color:"white"}}>Number of tags </Form.Label>
                                <Form.Control type="number"
                                              placeholder="Number of tags (By default the number of tags is 1)"/>
                            </Form.Group>
                        </Form>

                        {this.state.visibleTable ? (
                            <div >
                                < Button className="fa fa-users"
                                         onClick={() => this.getUsers(this.state.nTags)} outline
                                         color="success">Get Objective Users</Button></div>) :
                            (<div>< Button className="fa fa-users"
                                      onClick={() => change3(this.state.nTags)} outline
                                      color="success">Get Objective Users</Button></div>)
                        }
                    </div>
                    <div className="tableObjectiveUser">
                        {this.state.visibleTable ? (
                            <Table >
                                <thead>
                                <tr>
                                    <th style={{color:"white"}}>Username</th>
                                </tr>
                                </thead>
                                <tbody >
                                {this.state.names.map(function (item, index) {
                                    return (<tr>
                                        <td style={{color:"white"}}>{item}{console.log(item)}
                                            <Button className="fa fa-paper-plane"
                                                    onClick={() => change(item, index)} outline
                                                    color="success">Connection Request </Button>
                                        </td>
                                    </tr>)
                                })
                                }
                                </tbody>
                            </Table>) : ''}
                    </div>
                {this.state.visibleForm ? (
                    <Form className="formObjectiveUser" value={this.state.textToSend}
                          onChange={event => this.setState({textToSend: event.target.value})}>
                        <Form.Group controlId="formText">
                            <Form.Label  style={{color:"white"}}>Connection request text</Form.Label>
                            <Form.Control type="text" placeholder="Enter the text to send"/>
                        </Form.Group>
                        < Button className="fa fa-paper-plane"
                                 onClick={() => change2()}
                                 outline
                                 color="success">Send Connection Request</Button>
                    </Form>
                ) : ''}

            </div>


        )
    }
}

export default ScrollableTableUtilizadorObjetivo;
