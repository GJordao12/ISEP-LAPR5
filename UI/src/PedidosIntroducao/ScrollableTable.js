import React, {Component, useState} from 'react'
import Table from 'react-bootstrap/Table'
import './ScrollableTable.css'
import {Button} from "reactstrap";
import Footer from "../Componente/PageConstructor/Footer";

class ScrollableTable extends Component {
    constructor(props) {
        super(props); //since we are extending class Table so we have to use super in order to override Component class constructor
        this.state = { //state is by default an object
            'data': [],
            'ids': [],
            'names':[]
        }

    }

     async componentDidMount() {
         await fetch("/prolog/base_conhecimento");
         const tokenString = sessionStorage.getItem('token');
         const userToken = JSON.parse(tokenString);
         await fetch("/prolog/utilizador_objetivo?tag=" + 1 + "&id='"+userToken+"'")
             .then( async  res =>  await res.json())
             .then( result => this.setState({'data': result}));
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
         document.getElementById("textarea").style.visibility = "hidden";
         document.getElementById("textarea1").style.visibility = "hidden";
         document.getElementById("tabelaIntermediarios").style.visibility = "hidden";
         document.getElementById("btnSubmit").style.visibility = "hidden";
     }


    render() {
        let userNameSelected = '', idUserSelected = '', userIntermediary = '';
        let user = null;
        let intermediaries = [];

        async function getIntermediaries(selected) {
            document.getElementById("tabelaIntermediarios").style.visibility = "visible";
            document.getElementById("textarea1").style.visibility = "visible";
            document.getElementById("btnSubmit").style.visibility = "visible";
            await fetch("/prolog/base_conhecimento");
            const tokenString = sessionStorage.getItem('token');
            const userToken = JSON.parse(tokenString);

            await fetch("api/User/name/" + selected)
                .then(async res => {
                    if (res.ok) {
                        await res.json().then(result => user = result)
                    } else if (res.status === 404) {
                        await res.json().then(user = null)
                    }
                })

            await fetch("api/PedidoIntroducao/intermediarios/x="+userToken+"/z="+user.id+"")
                        .then( async res => intermediaries = await res.json());
            console.log(intermediaries)
            let x = document.getElementById("segundaTabela");
            for(let i = 0; i < x.children.length; i++) x.removeChild(x.children[i]);
            for(let i of intermediaries) {
                let el = document.createElement('td');
                let btn = document.createElement('button');
                btn.style.color = 'green';
                btn.onclick = function(){
                    userIntermediary = i.username;
                };
                el.innerText = i.username;
                el.id = i.username;
                el.value = i.username;
                el.style.color = "white";

                x.appendChild(el);
                x.appendChild(btn);
            }
        }



        function clearData() {
            userNameSelected = '';
            idUserSelected = '';
            userIntermediary = '';
            document.getElementById('textarea').value = '';
            document.getElementById('textarea1').value = '';
        }

        function showSuccessPopUp() {
            let modal = document.getElementById("modal")
            let closeBtn = document.getElementById("closeButton")

            modal.style.display = "block";
            closeBtn.addEventListener("click", () => {
                modal.style.display = "none"
            })
        }

        async function sendIntroductionRequest() {
            const tokenString = sessionStorage.getItem('userName');
            const username = JSON.parse(tokenString);
            if (userNameSelected !== '' && userNameSelected !== null && userIntermediary !== '' && userIntermediary !== null
                && document.getElementById('textarea').value !== '' && document.getElementById('textarea').value !== null
                && document.getElementById('textarea1').value !== '' && document.getElementById('textarea1').value !== null) {
                fetch('api/PedidoIntroducao/', {
                    method: 'POST',
                    headers: {
                        'Content-type': 'application/json; charset=UTF-8' // Indicates the content
                    },
                    body: JSON.stringify({
                        "userNameRem": username,
                        "userNameInt": userIntermediary,
                        "userNameDest": userNameSelected,
                        "apresentacao": document.getElementById('textarea1').value,
                        "apresentacaoLigacao": document.getElementById('textarea').value
                    })
                })
                    .then(response => response.json())
                    .then(data => console.log(data))
                    .catch(err => console.log(err))

                showSuccessPopUp();
            }
            clearData();
        }

        function click(item, option) {
            if (option === 1) {
                userNameSelected = item;
                document.getElementById("textarea").style.visibility = "visible";
                getIntermediaries(userNameSelected);
            }
            if (option === 2) {
                userIntermediary = item.username;
            }
        }

        return (
            <div>
                <Table class="table-wrapper-scroll-y my-custom-scrollbar" id = 'tabelaUtilizadoresObjetivo' striped bordered hover>
                    <thead>
                    <center><h3 style={{color:"white"}}>Table of possible objective users</h3></center>
                    </thead>
                    <tbody>
                    <center><h5 style={{color:"white"}}>Username:</h5></center>
                    {this.state.names.map(function (item, index) {
                        return (<tr>
                                <td style={{color:"white"}}>{"Username: "+ item}
                                </td>
                                <Button className="buttonFirstTable"
                                        onClick={() => click(item, 1)} outline
                                        color="success"> &nbsp; </Button>
                            </tr>
                        )

                    })}
                    </tbody>
                </Table>
                <div className="form-group shadow-textarea">
                                                <textarea style={{width: '80%'}} className="form-control z-depth-1"
                                                          id="textarea" rows="2"
                                                          placeholder="Please, type a presentation you want the user selected to receive..."/>
                </div>
                <Table class="table-wrapper-scroll-y my-custom-scrollbar" id = 'tabelaIntermediarios' striped bordered hover>
                    <thead>
                    <center><h3 style={{color:"white"}}>Table of possible intermediary users:</h3></center>
                    </thead>
                    <tbody>
                    <center><h5 style={{color:"white"}}>Username:</h5></center>
                    <tr id = 'segundaTabela' className="secTable">
                    </tr>
                    </tbody>
                </Table>
                <div className="form-group shadow-textarea">
                                                <textarea style={{width: '80%'}} className="form-control z-depth-1"
                                                          id="textarea1" rows="2"
                                                          placeholder="Please, type a presentation you want the user selected to receive..."/>
                </div>
                <Button id = "btnSubmit" className="buttonSubmit"
                        onClick={sendIntroductionRequest.bind(this)} outline
                        color="success"> Submit Introduction Request </Button>
                <div className="modal" id="modal">
                    <div className="modal_content">
                        <span className="close" id="closeButton">&times;</span>
                        <p style={{color:"white"}}>Introduction Request Registered With Success! 😃</p>
                    </div>
                </div>
                <div className="modal1" id="modal1">
                    <div className="modal_content1">
                        <span className="close1" id="closeButton1">&times;</span>
                        <p style={{color:"white"}}>not possible</p>
                    </div>
                </div>
                <Footer/>
            </div>
        )
    }
}

export default ScrollableTable;