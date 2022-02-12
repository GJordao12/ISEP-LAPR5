import './Tamanho.css';
import Card from "react-bootstrap/Card";
import {Button} from "reactstrap";
import React, {Component} from 'react'
import axios from 'axios'
import Footer from "../Componente/PageConstructor/Footer";
import Header2 from "../Componente/PageConstructor/Header2";

class Tamanho extends Component {

    constructor(props) {
        super(props);
        this.state = {
            resultado: 0
        }
    }

    render() {
        return (
            <div>
                <Header2/>
                <div className="pedidoIntroducaoBackground">
                <div className="titulo">
                    <h2 style={{color:'white'}}> NetWork Size: </h2>
                </div>
                <div className='container containerCard'>
                    <div className='row' >
                        <div className="containerTamanho">
                            <Card style={{width: '18rem', backgroundColor: 'white', textAlign: 'left'}}>
                                <Card.Body>
                                    <div className="mb-3 ms-3">
                                        <Card.Text style={{color:'red'}}> Choose The LVL: </Card.Text>
                                        <input style={{width: '30%'}} type="number" className="form-control"
                                               id="numberboxTamanho"
                                               aria-describedby="emailHelp" min="0" max="100"/>
                                        <br/>
                                        <Card.Text style={{color:'red'}}> Network Size: {this.state.resultado}</Card.Text>
                                    </div>
                                </Card.Body>
                                <Button className="ms-4 mb-3" style={{
                                    marginBottom: '0.5rem',
                                    width: '80%',
                                    color: 'black',
                                    backgroundColor: '#ffffff',
                                    borderColor: '#7A4419'
                                }} onClick={() => this.getTamanho()}>Get Size</Button>
                            </Card>
                        </div>
                    </div>
                </div>
                    <Footer/>
                </div>
            </div>
        )
    }

    async getTamanho() {
        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);

        let nivel = document.getElementById('numberboxTamanho').value;

        try {
            await axios.get('/prolog/base_conhecimento');  //request para UPDATE da base de conhecimento
        } catch (e) {

        }
        const response = await axios.get('/prolog/tamanho_rede?id='.concat(userToken) + '&nivel='.concat(nivel));
        let array = Array.from(response.data);

        let size = array.length;

        this.setState({resultado: size});
    }
}

export default Tamanho;
