import './CreateAccount.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, {useEffect, useState} from 'react';
import HomeLeftMenu from '../Componente/HomeLeftMenu/HomeLeftMenu'
import validator from 'validator'

function CreateAccount() {

    function communicationWithAPI(email, username, password) {
        fetch('api/User/', {
            method: 'POST',
            headers: {
                'Content-type': 'application/json; charset=UTF-8' // Indicates the content
            },
            body: JSON.stringify(
                {
                    "Email": email.value,
                    "Username": username.value,
                    "Password": password.value
                })
        })
            .then(response => response.json())
            .then(data => console.log(data))
            .catch(err => console.log(err))
    }

    function clearData(username, email, password, confirmPassword) {
        username.value = "";
        email.value = "";
        password.value = "";
        confirmPassword.value = "";
    }

    function showSuccessPopUp() {
        let modal = document.getElementById("modal_accept")
        let closeBtn = document.getElementById("closeButton")

        modal.style.display = "block";
        closeBtn.addEventListener("click", () => {
            modal.style.display = "none"
        })
    }

    function showTermsPopUp(){
        let modal = document.getElementById("modal_terms")
        let closeBtn = document.getElementById("closeButtonTerms")

        modal.style.display = "block";
        closeBtn.addEventListener("click",() =>{modal.style.display = "none"})
    }


    function saveAccount(username, email, password, confirmPassword) {
        //COMMUNICATION WITH API
        communicationWithAPI(email, username, password);

        //POPUP SUCCESS
        showSuccessPopUp();

        //CLEAR DATA
        clearData(username, email, password, confirmPassword);
    }

    function dataVerification() {

        let username = document.getElementById("username");
        let usernameError = document.getElementById("userNameError");
        let email = document.getElementById("email");
        let emailError = document.getElementById("emailError");
        let password = document.getElementById("password");
        let passwordError = document.getElementById("passwordError");
        let confirmPassword = document.getElementById("confirmPassword");
        let confirmPasswordError = document.getElementById("confirmPasswordError");
        let checkBox = document.getElementById("checkbox1");
        let checkBoxError = document.getElementById("checkBoxError");

        let flag = true;

        if (username.value.length === 0 || username.value.indexOf(' ') >= 0 ) {
            usernameError.innerText = "Enter a valid User Name.";
            usernameError.style.visibility = "visible";
            flag = false;
        } else {
            usernameError.style.visibility = "hidden";
        }

        if (email.value.length === 0 || !validator.isEmail(email.value)) {
            emailError.innerText = "Enter a valid Email.";
            emailError.style.visibility = "visible";
            flag = false;
        } else {
            emailError.style.visibility = "hidden";
        }

        if (password.value.length === 0) {
            passwordError.style.visibility = "visible";
            flag = false;
        } else {
            passwordError.style.visibility = "hidden";
        }

        if (confirmPassword.value !== password.value) {
            confirmPasswordError.style.visibility = "visible";
            flag = false;
        } else {
            confirmPasswordError.style.visibility = "hidden";
        }

        if(!checkBox.checked) {
            checkBoxError.innerText = "You have to accept our terms.";
            checkBoxError.style.visibility = "visible";
            flag = false;
        } else {
            checkBoxError.style.visibility = "hidden";
        }

        if (flag) {
            saveAccount(username, email, password, confirmPassword)
        }
    }

    const [isChecked, setIsChecked] = useState(false);

    const handleOnChange = () => {
        setIsChecked(!isChecked);
    };

    return (
        <div>
            <div className="createAccount">
                <HomeLeftMenu/>
                <div className="pageCreateAccount">
                    <div className="pageHeaderCreateAccount">
                        <header><strong style={{color:"white"}}>Create Account in G4S - Social Network Game</strong></header>
                    </div>
                    <div className="createAccountSpace">
                        <h3 style={{color:"white"}}>User Name:</h3>
                        <input type="userName" id="username" placeholder="user name"/>
                        <p id="userNameError" style={{visibility: "hidden", color: "red"}}>User Name already exists.</p>
                        <h3 style={{color:"white"}}>Email:</h3>
                        <input type="email" id="email" placeholder="email"/>
                        <p id="emailError" style={{visibility: "hidden", color: "red"}}>Email already exists.</p>
                        <h3 style={{color:"white"}}>Password:</h3>
                        <input type="password" id="password" placeholder="password"/>
                        <p id="passwordError" style={{visibility: "hidden", color: "red"}}>Enter a valid Password.</p>
                        <h3 style={{color:"white"}}>Confirm Password:</h3>
                        <input type="password" id="confirmPassword" placeholder="password"/>
                        <p id="confirmPasswordError" style={{visibility: "hidden", color: "red"}}>Passwords don't
                            match.</p>
                        <div>
                            <h4 style={{color:"white"}}> Here you can read our <button className="astext" onClick={showTermsPopUp}> Use Terms </button> </h4>
                        </div>
                        <div className="modal" id="modal_terms">
                            <div className="modal_content">
                                <span className="close" id="closeButtonTerms">&times;</span>

                                <h3>DADOS A SER RECOLHIDOS:</h3>

                                <p className="p_terms">
                                    <ul className="terms_ul">
                                        <li className="terms">Nome do utilizador a ser registado</li>
                                        <li className="terms">Data de Nascimento do utilizador</li>
                                        <li className="terms">Email do utilizador</li>
                                        <li className="terms">Número de telefone do utilizador</li>
                                        <li className="terms">Link do Facebook do utilizador</li>
                                        <li className="terms">Link do Linkedin do utilizador</li>
                                        <li className="terms">Estado Emocional do utilizador</li>
                                    </ul>
                                </p>

                                    <h3>INTUITO DO TRATAMENTO DOS DADOS RECOLHIDOS:</h3>
                                    <p className="p_terms">O intuito do tratamento dos dados recolhidos pela nossa aplicação relativo ao utilizador, como o nome, a data de nascimento, o email, o número de telefone, os links (Facebook & Linkedin) e o seu estado emocional,  servem exclusivamente para identificação do utilizador em aplicação, bem como para comunicação entre os diversos utilizadores da rede e eventuais anormalidades na conta do utilizador.</p>

                                    <p className="p_terms">O nome tem o propósito para a identificação e comunicação com outro utilizador da rede, a data de nascimento para assegurar a entidade responsável pela aplicação de que o utilizador tem a idade mínima para consentir o uso dos seus dados pessoais, o email e o número de telefone para eventual contacto caso haja acontecimentos inesperados, (a sua conta foi comprometida ou acessos questionáveis na conta do utilizador), links das redes sociais para que o utilizador consiga encontrar os seus amigos rapidamente e o estado emocional para representação do seu estado emotivo.</p>
                                    <p className="p_terms">Tendo porventura o direito ao esquecimento assim que o utilizador decidir eliminar a conta.</p>

                                    <h3>PESSOA RESPONSÁVEL PELO TRATAMENTO DOS DADOS RECOLHIDOS:</h3>
                                    <p className="p_terms">O responsável pelo tratamento dos dados recolhidos é o desenvolvedor da aplicação Fábio Fernandes, funcionário da entidade responsável Graph4Social, sediada no Porto.</p>

                            </div>
                        </div>
                        <h4 style={{color:"white"}}>Click on the check box to accept our terms</h4>
                            <div className="topping">
                                <input
                                    type="checkbox"
                                    id ="checkbox1"
                                    checked={isChecked}
                                    onChange={handleOnChange}
                                />
                                <p id="checkBoxError" style={{visibility: "hidden", color: "red"}}>Please accept our terms.</p>
                            </div>
                        <div className="btn2CreateAccount" onClick={dataVerification}>
                            <button className="w-100 btn btn-lg btn-primary">Sign Up</button>
                        </div>
                    </div>
                    <footer className="rodapeCreateAccount"  style={{color:"white"}}>© 2021 Copyright: G4S - Social Network Game</footer>
                </div>
            </div>
            <div className="modal" id="modal_accept">
                <div className="modal_content">
                    <span className="close" id="closeButton">&times;</span>
                    <p>Account Created With Success 😃</p>
                </div>
            </div>
        </div>
    );
}

export default CreateAccount;