import React, {Component} from 'react'
import Table from 'react-bootstrap/Table'
import './MoodTable.css'

class MoodTable extends Component {
    constructor(props) {
        super(props);
        this.state = {
            'data': [],
        }
    }

    async componentDidMount() {
        //get estados de humor
        await fetch("api/EstadosDeHumor")
            .then(async res => await res.json())
            .then(result => this.setState({'data': result})
            );
    }

    render() {

        let atual = "-1";
        let anterior = "-1";
        let estadoDehumor = "";

        function editMood() {
            if (atual === "-1") {
                showSuccessPopUp(1);
            } else {
                showSuccessPopUp(2);
                communicationWithAPI();
            }
        }

        function communicationWithAPI() {
            const tokenString = sessionStorage.getItem('token');
            const userToken = JSON.parse(tokenString);
            fetch('api/Perfil/userId=' + userToken + '/EditarEstadoDeHumor', {
                method: 'PUT',
                headers: {
                    'Content-type': 'application/json; charset=UTF-8' // Indicates the content
                },
                body: JSON.stringify(
                    {
                        "id": atual,
                        "description": estadoDehumor
                    })
            })
                .then(response => response.json())
                .then(data => console.log(data))
                .catch(err => console.log(err))
        }

        function showSuccessPopUp(option) {
            let modal = document.getElementById("modal")
            let closeBtn = document.getElementById("closeButton")
            let popUpText = document.getElementById("popUpText")

            modal.style.display = "block";
            if (option === 1) {
                popUpText.innerText = "❗ - Please Select One Mood";
            } else {
                popUpText.innerText = "✅ - Mood Successfully Edited To \"" + estadoDehumor + "\"";
            }

            closeBtn.addEventListener("click", () => {
                modal.style.display = "none"
            })
        }

        function changeBackground(item) {
            if (atual === "-1") {
                atual = item.id;
                estadoDehumor = item.description;
                let element = document.getElementById(atual);
                element.style.backgroundColor = "darkgrey";
            } else {
                if (atual === item.id) {
                    let element2 = document.getElementById(atual);
                    element2.style.backgroundColor = "transparent";
                    atual = "-1";
                } else {
                    anterior = atual;
                    let element1 = document.getElementById(anterior);
                    element1.style.backgroundColor = "transparent";

                    atual = item.id;
                    estadoDehumor = item.description;
                    let element2 = document.getElementById(atual);
                    element2.style.backgroundColor = "darkgrey";
                }
            }
        }

        return (

            <div>
                <div className="tableSpaceHumor">
                    <Table id="myTable" class="myTableHumor table-wrapper-scroll-y my-custom-scrollbar">
                        <thead>
                        <tr>
                            <th style={{color:"white"}}>Available Moods (Select One)</th>
                        </tr>
                        </thead>
                        <tbody> {
                            this
                                .state
                                .data
                                .map(
                                    function (item) {
                                        return (<tr id={item.id + ""} onClick={() => changeBackground(item)}>
                                                <td className="humor" style={{color:"white"}}>{item.description}</td>
                                            </tr>
                                        )
                                    }
                                )
                        }
                        </tbody>
                    </Table>
                    <div className="btnHumor" >
                        <button onClick={editMood} className="w-100 btnHumor2 btn-primary">Edit</button>
                    </div>
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
}

export default MoodTable;