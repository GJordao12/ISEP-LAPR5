import 'bootstrap/dist/css/bootstrap.min.css';
import React from "react";
import './AceitarERejeitar.css';
import Card from "react-bootstrap/Card";
import {Button} from "reactstrap";
import Footer from "../Componente/PageConstructor/Footer";
import Header2 from "../Componente/PageConstructor/Header2";

class AceitarERejeitar extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            'items': [],
            'user1': '',
            'user2': ''
        };
    }

    componentDidMount() {

        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);

        fetch("api/PedidoLigacao/pendentes/".concat(userToken))
            .then(res => res.json())
            .then(result => this.setState({'items': result})
            );
    }

    render() {
        function deny(item) {
            fetch('api/PedidoLigacao/'.concat(item.id), {
                method: 'PUT',
                headers: {
                    'Content-type': 'application/json; charset=UTF-8' // Indicates the content
                },
                body: JSON.stringify({
                    "estado": "Rejeitado",
                    "remetente": item.remetente,
                    "destinatario": item.destinatario,
                    "texto": item.texto
                })
            })
                .then(response => response.json())
                .then(data => console.log(data))
                .catch(err => console.log(err))
        }

        function accept(item) {
            fetch('api/PedidoLigacao/'.concat(item.id), {
                method: 'PUT',
                headers: {
                    'Content-type': 'application/json; charset=UTF-8' // Indicates the content
                },
                body: JSON.stringify({
                    "estado": "Aceite",
                    "remetente": item.remetente,
                    "destinatario": item.destinatario,
                    "texto": item.texto

                })
            })
                .then(response => response.json())
                .then(data => console.log(data))
                .catch(err => console.log(err))
        }


        let changeRemetente = (item) => {

            fetch("api/User/".concat(item.remetente))
                .then(res => res.json())
                .then(result => this.setState({'user1': result.username})
                );
        }

        let changeDestinatario = (item) => {

            fetch("api/User/".concat(item.destinatario))
                .then(res => res.json())
                .then(result => this.setState({'user2': result.username})
                );
        }

        return (
            <div>
                <Header2/>
                <div className="pedidoIntroducaoPendenteBackground">
                <div className='texto'>
                    <h2 style={{color:"white"}}>Accept or Refuse Connection</h2>
                </div>
                <div className='container position-relative ms-lg-4'>
                    <div >
                        {this.state.items.map(item => (
                            <div>
                                {changeRemetente(item)}
                                {changeDestinatario(item)}
                                <div className='col-sm mb-4 col-xs-12'>
                                    <Card className='me-lg-4'
                                          style={{width: '18rem', backgroundColor: 'red'}}>
                                        <Card.Body>
                                            <Card.Title style={{color:"blue"}}>Request: </Card.Title>
                                            <div onLoad={() => this.changeRemetente(item)}>
                                                <Card.Subtitle
                                                    style={{color:"blue"}}> From: {this.state.user1} </Card.Subtitle>
                                            </div>
                                            <div onLoad={() => this.changeDestinatario(item)}>
                                                <Card.Subtitle
                                                   style={{color:"blue"}}>To: {this.state.user2}</Card.Subtitle>
                                            </div>
                                            <Card.Text style={{color:"blue"}}>
                                                Text: <br/>
                                                {item.texto}
                                            </Card.Text>
                                            <Button style={{marginBottom: '0.5rem', width: '100%'}}
                                                    onClick={accept.bind(this, item)} outline
                                                    color="success">Accept</Button>
                                            <Button style={{width: '100%'}} onClick={deny.bind(this, item)} outline
                                                    color="danger">Refuse</Button>
                                        </Card.Body>
                                    </Card>
                                </div>
                            </div>))}
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

        fetch("api/PedidoLigacao/pendentes/".concat(userToken))
            .then(res => res.json())
            .then(result => this.setState({'items': result})
            );
    }
}


export default AceitarERejeitar;
