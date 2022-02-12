import './ResponderPedido.css'
import 'bootstrap/dist/css/bootstrap.min.css';
import React from "react";
import Card from 'react-bootstrap/Card';
import {Button} from 'reactstrap';
import Footer from "../Componente/PageConstructor/Footer";
import Header2 from "../Componente/PageConstructor/Header2";

class ResponderPedido extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            'items': []
        };
    }

    componentDidMount() {
        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);
        fetch("api/PedidoIntroducao/pendentes/intermediario=".concat(userToken))
            .then(res => res.json())
            .then(result => this.setState({'items': result})
            );
    }

    render() {
        function deny(id) {
            fetch('api/PedidoIntroducao/'.concat(id), {
                method: 'PUT',
                headers: {
                    'Content-type': 'application/json; charset=UTF-8' // Indicates the content
                },
                body: JSON.stringify({
                    "estadoPedidoIntroducao": "DESAPROVADO"
                })
            })
                .then(response => response.json())
                .then(data => console.log(data))
                .catch(err => console.log(err))
        }

        function accept(id) {
            fetch('api/PedidoIntroducao/'.concat(id), {
                method: 'PUT',
                headers: {
                    'Content-type': 'application/json; charset=UTF-8' // Indicates the content
                },
                body: JSON.stringify({
                    "estadoPedidoIntroducao": "APROVADO"
                })
            })
                .then(response => response.json())
                .then(data => console.log(data))
                .catch(err => console.log(err))
        }

        return (
            <div>
                <Header2/>
                <div className="pedidoIntroducaoPendenteBackground">
                <div className='texto'>
                    <h2 style={{color:"white"}}> Pending Introduction Requests</h2>
                </div>
                <div className='container position-relative ms-lg-4'>
                    <div>
                        {this.state.items.map(function (item) {
                                return (
                                    <div className='col-sm mb-4 col-xs-12'>
                                        <Card className='me-lg-4'
                                              style={{width: '18rem', backgroundColor: 'red'}}>
                                            <Card.Body>
                                                <Card.Text style={{color:'blue'}}
                                                    className="mb-2">Requested By: {item.remetente.username} </Card.Text>
                                                <Card.Text style={{color:'blue'}}
                                                    className="mb-2">Requesting Introduction
                                                    To: {item.destinatario.username}</Card.Text>
                                                <Card.Text style={{color:'blue'}}>
                                                    Presentation Text: <br/>
                                                    {item.apresentacao}
                                                </Card.Text>
                                                <Button style={{marginBottom: '0.5rem', width: '100%'}}
                                                        onClick={accept.bind(this, item.id)} outline
                                                        color="success">Approve</Button>
                                                <Button style={{width: '100%'}} onClick={deny.bind(this, item.id)} outline
                                                        color="danger">Disapprove</Button>
                                            </Card.Body>
                                        </Card>
                                    </div>);
                            }
                        )}
                    </div>
                </div>
                </div>
                <Footer/>
            </div>
        )
    }

    componentDidUpdate() {

        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);

        fetch("api/PedidoIntroducao/pendentes/intermediario=".concat(userToken))
            .then(res => res.json())
            .then(result => this.setState({'items': result})
            );
    }

}

export default ResponderPedido;