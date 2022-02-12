import React from 'react'
import {Dropdown} from 'semantic-ui-react'
import './Dropdown.css'
import axios from "axios";

const trigger = (
    <span style={{color: "white", fontFamily: 'Gothic A1, sans-serif', fontSize: "13px"}}>
        Hello, {getUsername()}
        <div className="modal" id="modal_delete_account">
                <div className="modal_content">
                    <span className="close" id="closeButtonDelete">&times;</span>
                    <p style={{'color': "white"}}>Are you sure you want to delete your account?<br/>
                        All data will be deleted and you will have to create a new account to be able to play again.
                    </p>
                    <div className="div-buttons">
                        <button id="btn-yes" className="btn-yes">Yes</button>
                        <button id="btn-no" className="btn-no">No</button>
                    </div>
                </div>
            </div>
  </span>
)

function getUsername() {
    const tokenString = sessionStorage.getItem('userName');
    return JSON.parse(tokenString);
}

function showPopUp() {
    let modal = document.getElementById("modal_delete_account")
    let closeBtn = document.getElementById("closeButtonDelete")
    let yesBtn = document.getElementById("btn-yes")
    let noBtn = document.getElementById("btn-no")

    modal.style.display = "block";

    closeBtn.addEventListener("click", () => {
        modal.style.display = "none"
    })

    noBtn.addEventListener("click", () => {
        modal.style.display = "none"
    })

    yesBtn.addEventListener("click", async () => {
        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);

        await fetch('api/User/userId=' + userToken, {
            method: 'DELETE',
        })
            .then(response => response.json())
            .then(data => console.log(data))
            .catch(err => console.log(err))

        await axios.delete('http://localhost:5001/api/post/' + userToken)
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            });

        await axios.delete('http://localhost:5001/api/comentario/' + userToken)
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            });

        window.location.href = "/Login";
    })
}

const options = [
    {
        key: 'nameUser',
        text: "Signed in as " + getUsername(),
        fontSize: "13px",
        fontFamily: 'Gothic A1, sans-serif',
        color: "black",
        disabled: true,
    },
    {
        key: 'Perfil',
        text: "Your profile",
        color: "black",
        fontFamily: 'Gothic A1, sans-serif',
        fontSize: "13px",
        className: "nav-link px-lg-0 link-dark",
        href: "/Perfil"
    },
    {
        key: 'Edit Mood',
        text: "Edit Mood",
        color: "black",
        fontFamily: 'Gothic A1, sans-serif',
        fontSize: "13px",
        className: "nav-link px-lg-0 link-dark",
        href: "/EditarEstadoDeHumor"
    },
    {
        key: 'Edit Connections',
        text: "Edit Connections",
        color: "black",
        fontFamily: 'Gothic A1, sans-serif',
        fontSize: "13px",
        className: "nav-link px-lg-0 link-dark",
        href: "/EditarRelacionamento"
    },
    {
        key: 'Answer Request',
        text: "Answer Introduction Request",
        color: "black",
        fontFamily: 'Gothic A1, sans-serif',
        fontSize: "13px",
        className: "nav-link px-lg-0 link-dark",
        href: "/ResponderPedido"
    },
    {
        key: 'aceitar o rejeitar',
        text: "Accept or Refuse",
        color: "black",
        fontFamily: 'Gothic A1, sans-serif',
        fontSize: "13px",
        className:"nav-link px-lg-0 link-dark",
        href: "/AceitarERejeitar"
    },
    {
        key: 'searchForUser',
        text: "Search For User",
        color: "black",
        fontFamily: 'Gothic A1, sans-serif',
        fontSize: "13px",
        className:"nav-link px-lg-0 link-dark",
        href: "/Search"
    },
    {
        key: 'objectiveUser',
        text: "Objective User",
        color: "black",
        fontFamily: 'Gothic A1, sans-serif',
        fontSize: "13px",
        className:"nav-link px-lg-0 link-dark",
        href: "/UtilizadorObjetivo"
    },
    {
        key: 'pedidosLigacao',
        text: "Connection Requests (Pending)",
        color: "black",
        fontFamily: 'Gothic A1, sans-serif',
        fontSize: "13px",
        className:"nav-link px-lg-0,  link-dark",
        href: "/PedidosLigacao"
    },
    {
        key: 'pedidosIntroducao',
        text: "Introduction Request",
        color: "black",
        fontFamily: 'Gothic A1, sans-serif',
        fontSize: "13px",
        className:"nav-link px-lg-0  link-dark",
        href: "/PedidosIntroducao"
    },
    {
        key: 'ligacoes',
        text: "Connection/Relationship Strength",
        color: "black",
        fontFamily: 'Gothic A1, sans-serif',
        fontSize: "13px",
        className:"nav-link px-lg-0  link-dark",
        href: "/Ligacoes"
    },
    {
        key: 'tagsUser',
        text: "Tags From User",
        color: "black",
        fontFamily: 'Gothic A1, sans-serif',
        fontSize: "13px",
        className:"nav-link px-lg-0  link-dark",
        href: "/TagsFromUser"
    },
    {
        key: 'tagsRelacoes',
        text: "Tags From Own Relations",
        color: "black",
        fontFamily: 'Gothic A1, sans-serif',
        fontSize: "13px",
        className:"nav-link px-lg-0  link-dark",
        href: "/TagsFromRelacoesPropio"
    },
    {
        key: 'Delete Account',
        text: "Delete Account",
        color: "black",
        fontFamily: 'Gothic A1, sans-serif',
        fontSize: "13px",
        onClick:showPopUp,
        className:"nav-link px-lg-0  link-dark"
    },
    {
        key: 'Sign-Out',
        text: "Sign Out",
        color: "black",
        fontFamily: 'Gothic A1, sans-serif',
        fontSize: "13px",
        className:"nav-link px-lg-0 link-dark",
        href: "/Login"
    },
]

const DropdownTriggerExample = () => (
    <Dropdown trigger={trigger} options={options}/>
)

export default DropdownTriggerExample;