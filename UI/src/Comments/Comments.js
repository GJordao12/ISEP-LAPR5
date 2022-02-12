import Header from '../Componente/PageConstructor/Header'
import Body from "../Componente/PageConstructor/Body";
import 'bootstrap/dist/css/bootstrap.min.css';


import React, {Component, useEffect, useState} from "react";
import Card from "react-bootstrap/Card";
import {Button} from "reactstrap";
import Footer from "../Componente/PageConstructor/Footer";
import Header2 from "../Componente/PageConstructor/Header2";

function Comments(){

    function postPerfil(texto, listaTags) {

        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);

        fetch('api/comentario', {
            method: 'POST',
            headers: {
                'Content-type': 'application/json; charset=UTF-8' // Indicates the content
            },
            body: JSON.stringify(
                {
                    "idUser": userToken,
                    "texto": texto,
                    "listaTags": listaTags,

                })
        })
            .then(response => response.json())
            .then(data => console.log(data))
            .catch(err => console.log(err))
    }
    function saveAccount(texto, listaTags) {
        //COMMUNICATION WITH API
        postPerfil(texto, listaTags);

    }
    function dataVerification() {
        let id1 = 'text';
        let id2 = 'tag';



        let texto = document.getElementById(id1).data;
        let listaTags = document.getElementById(id2).data;




        let flag = true;
        if (flag) {
            saveAccount(texto, listaTags)
        }
    }
    return (
        <div>
            <Header2/>
            <div className="postsBackground">
            <Card className='me-lg-4'
                  style={{width: '18rem', backgroundColor: '#dbb587', textAlign: 'center'}}>
                <Card.Body>
                    <h2>Your Coments</h2>
                    <div className="ms-3 mb-3">
                        <Card.Text> <h4>Text:</h4> </Card.Text>
                        <div className="form-group shadow-textarea">
                            <input type="email" className="form-control" id="text"
                                   aria-describedby="emailHelp" placeholder="Enter Comment"/>
                        </div>
                    </div>
                    <div className="ms-3 mb-3">
                        <Card.Text> <h4>Tags:</h4> </Card.Text>
                        <div className="form-group shadow-textarea">
                            <input type="email" className="form-control" id="tag"
                                   aria-describedby="emailHelp" placeholder="Enter Tags"/>
                        </div>
                    </div>

                    <div  onClick={dataVerification}>
                        <Button style={{width: '100%'}}>Coment</Button>
                    </div>

                </Card.Body>
            </Card>
            <Footer/>
            </div>
        </div>


    )
}
export default Comments