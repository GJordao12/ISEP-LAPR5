import Card from 'react-bootstrap/Card';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, {useEffect, useState} from "react";
import './EditarRelacionamento.css';
import {Button} from 'reactstrap';
import Header from "../Componente/PageConstructor/Header";
import Footer from "../Componente/PageConstructor/Footer";
import Header2 from "../Componente/PageConstructor/Header2";

function EditarRelacionamento() {

    useEffect(() => {
        getLigacoes();
    }, []);

    const [listOjbects, setObjects] = useState([]);

    function DTO(ligacao, user) {
        this.id = ligacao.id;
        this.to = user.username;
        this.tags = ligacao.listaTags;
        this.forca = ligacao.forcaLigacao;
    }

    const getLigacoes = async () => {

        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);

        const items = await fetch('api/Ligacao/user='.concat(userToken), {})
        const ligacoes = await items.json();

        let temp = [];
        for (const l of ligacoes) {
            let user = await fetch('api/User/'.concat(l.secundario.value), {})
            let userr = await user.json();
            temp.push(new DTO(l, userr))
        }

        setObjects(temp)
        return temp;
    }

    const submit = (item, index) => {

        let id1 = 'numberbox' + index.toString();
        let id2 = 'textareaRelacionamento' + index.toString();

        let strength = document.getElementById(id1).value
        let tags = document.getElementById(id2);

        let listNewTags = [];

        if (tags == null) {
            listNewTags = [];
        } else {
            listNewTags = tags.value.split('\n');
        }

        let tamanho = listNewTags.length;

        let textoFinal = '';
        let texto = '';

        for (let i = 0; i < tamanho; i++) {
            if (i !== tamanho - 1) {
                texto = '{"nome": "' + listNewTags[i] + '"},';
            } else {
                texto = '{"nome": "' + listNewTags[i] + '"}';
            }
            textoFinal = textoFinal + texto;
        }

        fetch('api/Ligacao/'.concat(item.id), {
            method: 'PUT',
            headers: {
                'Content-type': 'application/json; charset=UTF-8' // Indicates the content
            },

            body: JSON.stringify({
                "forcaLigacao": {
                    "forcaLigacao": Number(strength)
                },
                "listaTags": JSON.parse("[" + textoFinal + "]")
            })
        })
            .then(response => response.json())
            .then(data => console.log(data))
            .catch(err => console.log(err))
            .then(getLigacoes)
    }
    return (
        <div>
            <Header2/>
            <div className=" editarRelacionamentoBackground">
            <div style={{textAlign:"center",color:"white"}}>
                <h2> Your Connections: </h2>
            </div>
            <div className='container position-relative ms-lg-4'>
                <div className='row'>
                    {listOjbects.map(function (item, index) {
                        return (
                            <div className='col-sm mb-4 col-xs-12'>
                                <Card className='me-lg-4'
                                      style={{width: '18rem', backgroundColor: 'red', textAlign: 'left'}}>
                                    <Card.Body>
                                        <Card.Title style={{color:'blue'}}> Friend: <br/> </Card.Title>
                                        <Card.Text style={{color:'blue'}}> {item.to} </Card.Text>
                                        <Card.Title style={{color:'blue'}}> Tags: </Card.Title>
                                        {item.tags.map(function (items) {
                                            return (
                                                <ul>
                                                    <Card.Text style={{color:'blue'}}>
                                                        <li>{items.nome} </li>
                                                    </Card.Text>
                                                </ul>
                                            )
                                        })
                                        }
                                        <Card.Title style={{color:'blue'}}>
                                            Connection Strength:
                                        </Card.Title>
                                        <Card.Text style={{color:'blue'}}> {item.forca} </Card.Text>
                                    </Card.Body>
                                    <div>
                                        <div className="mb-3 ms-3">
                                            <Card.Text style={{color:'blue'}}> Edit Connection Strength: </Card.Text>
                                            <input style={{width: '30%'}} type="number" className="form-control"
                                                   id={("numberbox" + index.toString())}
                                                   aria-describedby="emailHelp" min="0" max="100"/>
                                        </div>
                                        <div className="ms-3 mb-3">
                                            <Card.Text style={{color:'blue'}}> Edit Connection Tags: </Card.Text>
                                            <div className="form-group shadow-textarea">
                                                <textarea style={{width: '90%'}} className="form-control z-depth-1"
                                                          id={("textareaRelacionamento" + index.toString())} rows="2"
                                                          placeholder="Insert your tags..."/>
                                            </div>
                                        </div>
                                    </div>
                                    <Button className="ms-4 mb-3" style={{
                                        marginBottom: '0.5rem',
                                        width: '80%',
                                        color: 'black',
                                        backgroundColor: '#ffffff',
                                        borderColor: '#000000'
                                    }} onClick={() => submit(item, index)}>Save</Button>
                                </Card>
                            </div>
                        )
                    })}
                </div>
                </div>
            </div>
            <Footer/>
        </div>
    )
}

export default EditarRelacionamento;