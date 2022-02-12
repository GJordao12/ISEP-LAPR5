import 'bootstrap/dist/css/bootstrap.min.css';
import './Perfil.css';
import React, {useEffect, useState} from "react";
import Footer from "../Componente/PageConstructor/Footer";
import Header2 from "../Componente/PageConstructor/Header2";

function EditarPerfil() {

    const [perfil, setperfil] = useState(null);
    const tokenString = sessionStorage.getItem('userName');
    const userToken = JSON.parse(tokenString);
    const emailString = sessionStorage.getItem('email');
    const emailToken = JSON.parse(emailString);

    function DTO(perfil, estado) {
        this.id = perfil.id;
        this.estadoid = estado.id;
        this.description = estado.description;
        this.data = perfil.perfilDataDeNascimento.data;
        this.facebook = perfil.perfilFacebook.linkFacebook;
        this.linkedin = perfil.perfilLinkedin.linkLinkedin;
        const nome = perfil.perfilNome.nomePerfil;
        const myArray = nome.split(" ");
        this.firstName = myArray[0];
        this.lastName = "";
        if (myArray.length !== 1) {
            for (let i = 1; i < myArray.length; i++) {
                if (i !== (myArray.length - 1)) {
                    this.lastName += myArray[i] + " ";
                } else {
                    this.lastName += myArray[i];
                }
            }
        }
        this.telefone = perfil.perfilNTelefone.nTelefono;
        let listTags = [];
        for (let i = 0; i < perfil.listaTags.length; i++) {
            listTags.push(perfil.listaTags[i].nome);
        }
        this.tags = listTags;
    }

    useEffect(async () => {
        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);

        const items = await fetch('api/Perfil/userId='.concat(userToken), {})
        const json = await items.json();

        const humor = await fetch('/api/EstadosDeHumor/'.concat(json.estadoDeHumorId.value), {})
        const estadoDeHumor = await humor.json();

        const perfilDTO = await new DTO(json, estadoDeHumor);

        setperfil(perfilDTO);
    }, []);

    async function submit() {

        let firstName = document.getElementById('inputFirstName');
        let firstNameError = document.getElementById("firstNameError");
        let lastName = document.getElementById('inputLastName');
        let lastNameError = document.getElementById("lastNameError");
        let facebook = document.getElementById('inputFaceLink');
        let faceLinkError = document.getElementById("faceLinkError");
        let linkedIn = document.getElementById('inputLinkedInLink');
        let linkedInLinkError = document.getElementById("linkedInLinkError");
        let telefone = document.getElementById('inputPhone');
        let phoneError = document.getElementById("phoneError");
        let birthday = document.getElementById('inputBirthday');
        let birthdayError = document.getElementById("birthdayError");
        let tags = document.getElementById('inputTags');

        let flag = true;

        if (firstName.value.length === 0 || firstName.value.indexOf(' ') >= 0) {
            firstNameError.style.visibility = "visible";
            flag = false;
        } else {
            firstNameError.style.visibility = "hidden";
        }

        if (lastName.value.length === 0 || lastName.value.indexOf(' ') >= 0) {
            lastNameError.style.visibility = "visible";
            flag = false;
        } else {
            lastNameError.style.visibility = "hidden";
        }

        if (facebook.value.length === 0 || facebook.value.indexOf(' ') >= 0) {
            faceLinkError.style.visibility = "visible";
            flag = false;
        } else {
            faceLinkError.style.visibility = "hidden";
        }

        if (linkedIn.value.length === 0 || linkedIn.value.indexOf(' ') >= 0) {
            linkedInLinkError.style.visibility = "visible";
            flag = false;
        } else {
            linkedInLinkError.style.visibility = "hidden";
        }

        if (telefone.value.length === 0 || telefone.value.indexOf(' ') >= 0) {
            phoneError.style.visibility = "visible";
            flag = false;
        } else {
            phoneError.style.visibility = "hidden";
        }

        if (birthday.value.length === 0 || birthday.value.indexOf(' ') >= 0) {
            birthdayError.innerText = "Please enter a valid Birthday Date";
            birthdayError.style.visibility = "visible";
            flag = false;
        } else {
            let date = birthday.value.split("-");
            if (Number(date[0]) > 2004) {
                birthdayError.innerText = "You must be over 18 years old";
                birthdayError.style.visibility = "visible";
                flag = false;
            } else {
                birthdayError.style.visibility = "hidden";
            }
        }

        if (flag) {

            let listNewTags;

            if (tags.value.length === 0) {
                listNewTags = [];
            } else {
                listNewTags = tags.value.split(/(\s+)/).filter(function (e) {
                    return e.trim().length > 0;
                });
            }

            let textoFinal = '';
            let texto = '';
            let aux = [];

            for (let i = 0; i < listNewTags.length; i++) {
                if ((i === listNewTags.length - 1) || ((i !== listNewTags.length - 1) && (!(listNewTags.slice(i + 1, listNewTags.length).includes(listNewTags[i]))))) {
                    if (i !== listNewTags.length - 1) {
                        texto = '{"nome": "' + listNewTags[i] + '"},';
                    } else {
                        texto = '{"nome": "' + listNewTags[i] + '"}';
                    }
                    aux.push(listNewTags[i]);
                    textoFinal = textoFinal + texto;
                }
            }

            await fetch('api/Perfil/' + perfil.id + '/editarPerfil', {
                method: 'PUT',
                headers: {
                    'Content-type': 'application/json; charset=UTF-8'
                },
                body: JSON.stringify({
                    "estadoDeHumor": {
                        "Id": perfil.estadoid,
                        "Description": perfil.description
                    },
                    "perfilDataDeNascimento": birthday.value,
                    "perfilFacebook": facebook.value,
                    "perfilLinkedin": linkedIn.value,
                    "perfilNome": firstName.value + " " + lastName.value,
                    "perfilNTelefone": telefone.value,
                    "listaTags": JSON.parse("[" + textoFinal + "]")
                })
            })
                .then(response => response.json())
                .then(data => console.log(data))
                .catch(err => console.log(err))

            let modal = document.getElementById("modal")
            let closeBtn = document.getElementById("closeButton")
            let popUpText = document.getElementById("popUpText")

            modal.style.display = "block";
            popUpText.innerText = "✅ - Profile Saved With Success";

            closeBtn.addEventListener("click", () => {
                modal.style.display = "none"
            })

            tags.value = aux.join(' ')
        }
    }

    if (perfil === null) {
        return (
            <div>
                <Header2/>
                <div className="pedidoIntroducaoBackground">
                    <div>
                        <h2 style={{color: "white", textAlign: "center"}}>EDIT PROFILE</h2>
                    </div>
                </div>
                <Footer/>
            </div>
        )
    }

    return (
        <div>
            <Header2/>
            <div className="pedidoIntroducaoBackground">
                <div>
                    <h2 style={{color: "white", textAlign: "center"}}>EDIT PROFILE</h2>
                </div>
                <div className="container-xl px-4 mt-5">
                    <div className="mt-0 mb-4">
                        <div className="row">
                            <div className="col-xl-4">
                                <div className="card mb-4 mb-xl-0">
                                    <div className="card-header">Profile Picture</div>
                                    <div className="card-body text-center">
                                        <img className="img-account-profile rounded-circle mb-2"
                                             src="http://bootdey.com/img/Content/avatar/avatar1.png" alt=""/>
                                    </div>
                                </div>
                            </div>
                            <div className="col-xl-8">
                                <div className="card mb-4">
                                    <div className="card-header">Account Details</div>
                                    <div className="card-body" >
                                        <form style={{backgroundColor:"white",borderColor:"white",color:"black"}}>
                                            <div className="mb-3">
                                                <label className="small mb-1" htmlFor="inputUsername" style={{color:"black"}}>Username (how
                                                    your name will appear to other users on the site)</label>
                                                <input className="form-control" id="inputUsername" type="text"
                                                       placeholder="Enter your username" value={userToken} readOnly/>
                                            </div>
                                            <div className="row gx-3 mb-3">
                                                <div className="col-md-6">
                                                    <label className="small mb-1" htmlFor="inputFirstName" style={{color:"black"}}>First
                                                        name</label>
                                                    <input className="form-control" id="inputFirstName" type="text"
                                                           placeholder="Enter your first name"
                                                           defaultValue={perfil.firstName}/>
                                                    <p id="firstNameError"
                                                       style={{visibility: "hidden", color: "red"}}>Please enter a valid
                                                        First Name</p>
                                                </div>
                                                <div className="col-md-6">
                                                    <label className="small mb-1" htmlFor="inputLastName" style={{color:"black"}}>Last
                                                        name</label>
                                                    <input className="form-control" id="inputLastName" type="text"
                                                           placeholder="Enter your last name"
                                                           defaultValue={perfil.lastName}/>
                                                    <p id="lastNameError"
                                                       style={{visibility: "hidden", color: "red"}}>Please enter a valid
                                                        Last Name</p>
                                                </div>
                                            </div>
                                            <div className="row gx-3 mb-3">
                                                <div className="col-md-6">
                                                    <label className="small mb-1" htmlFor="inputFaceLink" style={{color:"black"}}>Facebook
                                                        Link</label>
                                                    <input className="form-control" id="inputFaceLink" type="text"
                                                           placeholder="Enter your facebook link"
                                                           defaultValue={perfil.facebook}/>
                                                    <p id="faceLinkError"
                                                       style={{visibility: "hidden", color: "red"}}>Please enter a valid
                                                        Facebook Link</p>
                                                </div>
                                                <div className="col-md-6">
                                                    <label className="small mb-1"
                                                           htmlFor="inputLinkedInLink" style={{color:"black"}}>LinkedIn Link</label>
                                                    <input className="form-control" id="inputLinkedInLink" type="text"
                                                           placeholder="Enter your linkedIn link"
                                                           defaultValue={perfil.linkedin}/>
                                                    <p id="linkedInLinkError"
                                                       style={{visibility: "hidden", color: "red"}}>Please enter a valid
                                                        LinkedIn Link</p>
                                                </div>
                                            </div>
                                            <div className="mb-3">
                                                <label className="small mb-1" htmlFor="inputEmailAddress" style={{color:"black"}}>Email
                                                    address</label>
                                                <input className="form-control" id="inputEmailAddress" type="email"
                                                       placeholder="Enter your email address" value={emailToken}
                                                       readOnly/>
                                            </div>
                                            <div className="row gx-3 mb-3">
                                                <div className="col-md-6">
                                                    <label className="small mb-1" htmlFor="inputPhone" style={{color:"black"}}>Phone
                                                        number</label>
                                                    <input className="form-control" id="inputPhone" type="tel"
                                                           placeholder="Enter your phone number"
                                                           defaultValue={perfil.telefone}/>
                                                    <p id="phoneError"
                                                       style={{visibility: "hidden", color: "red"}}>Please enter a valid
                                                        Phone Number</p>
                                                </div>
                                                <div className="col-md-6">
                                                    <label className="small mb-1"
                                                           htmlFor="inputBirthday" style={{color:"black"}}>Birthday</label>
                                                    <input className="form-control" id="inputBirthday" type="date"
                                                           name="birthday" placeholder="Enter your birthday"
                                                           defaultValue={perfil.data}/>
                                                    <p id="birthdayError"
                                                       style={{visibility: "hidden", color: "red"}}>Please enter a valid
                                                        Birthday Date</p>
                                                </div>
                                            </div>
                                            <div className="mb-3">
                                                <label className="small mb-1" htmlFor="inputTags" style={{color:"black"}}>Your
                                                    Tags (Separate With Spaces)</label>
                                                <textarea className="form-control" id="inputTags"
                                                          placeholder="Enter your tags" style={{height: "2px"}}
                                                          defaultValue={perfil.tags.join(' ')}/>
                                            </div>
                                            <section>
                                                <button className="btn btn-primary" type="button" onClick={submit}>Save
                                                    changes
                                                </button>
                                                <div style={{display: "inline", paddingLeft: "10px"}}>
                                                    <a style={{backgroundColor: "red", color: "white"}}
                                                       href="/EditarEstadoDeHumor"
                                                       className="btn"
                                                       type="button">Change Mood
                                                    </a>
                                                </div>
                                            </section>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <Footer/>
            </div>
            <div className="modal" id="modal">
                <div className="modal_content">
                    <span className="close" id="closeButton">&times;</span>
                    <p id="popUpText"/>
                </div>
            </div>
        </div>
    )
}

export default EditarPerfil;




