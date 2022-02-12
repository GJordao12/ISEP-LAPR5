import React, {Component} from 'react'
import Header from "../Componente/PageConstructor/Header";
import './Caminhos.css'
import Footer from "../Componente/PageConstructor/Footer";

class Caminhos extends Component {

    async componentDidMount() {
        let info;
        const tokenID = sessionStorage.getItem('token');
        const userTokenID = JSON.parse(tokenID);
        await fetch('api/RedesConexoes/'.concat(userTokenID) + '/niveis=0')
            .then(async res => info = await res.json());

        let myMap = new Map(Object.entries(info));
        let usernames = [];

        for (let niveis = 1; niveis <= myMap.size; niveis++) {
            for (let i = 0; i < myMap.get('' + niveis).length; i++) {
                let username = myMap.get('' + niveis)[i].secundario.username;
                if (!usernames.includes(username)) {
                    usernames.push(username);
                }
            }
        }

        let possibleUsers = document.getElementById('possibleUsers');
        for (let i = 0; i < usernames.length; i++) {
            let opt = usernames[i];
            let el = document.createElement('option');
            el.text = opt;
            el.value = opt;
            possibleUsers.appendChild(el);
        }

        let paths = ["shortest", "strongest", "safest"];
        let possiblePaths = document.getElementById('possiblePaths');
        for (let j = 0; j < paths.length; j++) {
            let opt = paths[j];
            let el = document.createElement('option');
            el.text = opt;
            el.value = opt;
            possiblePaths.appendChild(el);
        }

        await fetch("/prolog/base_conhecimento");
    }

    render() {

        function check() {
            let possiblePaths = document.getElementById('possiblePaths');
            let valueSafest = document.getElementById('valueSafest');
            valueSafest.readOnly = possiblePaths.value !== "safest";
        }

        async function getPath() {
            const tokenUserName = sessionStorage.getItem('userName');
            const userTokenUserName = JSON.parse(tokenUserName);

            let possiblePaths = document.getElementById('possiblePaths');
            let possibleUsers = document.getElementById('possibleUsers');
            let valueSafest = document.getElementById('valueSafest');
            let pathText = document.getElementById('pathText');

            let path;

            if (possiblePaths.value === "shortest") {
                await fetch("/prolog/caminhoMaisCurto?nome=" + userTokenUserName + "&nome1=" + possibleUsers.value)
                    .then(async res => path = await res.json());
            } else if (possiblePaths.value === "safest") {
                await fetch("/prolog/caminho_mais_seguro?from=" + userTokenUserName + "&to=" + possibleUsers.value + "&value=" + valueSafest.value)
                    .then(async res => path = await res.json());
            } else if (possiblePaths.value === "strongest") {
                await fetch("/prolog/caminho_mais_forte?nome1=" + userTokenUserName + "&nome2=" + possibleUsers.value)
                    .then(async res => path = await res.json());
            }

            let pathString = "";

            if(path.length === 0){
                pathString = "No Path Found!"
            }else{
                for(let i = 0; i < path.length; i++){
                    if(i !== (path.length -1) ){
                        pathString = pathString + path[i] + " -> ";
                    }else{
                        pathString = pathString + path[i];
                    }
                }
            }
            pathText.innerText = pathString;
        }

        return (
            <div>
                <Header/>
                <div className="pagePaths">
                    <div className="pageHeaderPaths">
                        <header><strong>Social Network Paths</strong></header>
                    </div>
                    <div className="PathsSpace">
                        <div className="possiblePathsSpace">
                            <span>Select The Path Type:   </span>
                            <select id='possiblePaths' onChange={check}>
                            </select>
                        </div>
                        <div className="possibleUsersSpace">
                            <span>Select One User:   </span>
                            <select id='possibleUsers'>
                            </select>
                        </div>
                        <div className="valueSpace">
                            <span id="textSafest">Minimum Connection Strength:  </span>
                            <input readonly="true" id="valueSafest" style={{width: '22%'}} type="number"
                                   className="form-control"
                                   aria-describedby="emailHelp" min="-10" max="10" defaultValue="0"/>
                        </div>
                    </div>
                    <div className="btnPaths">
                        <button onClick={getPath}>GET PATH</button>
                    </div>
                    <div className="pathText2">
                        <p id="pathText">

                        </p>
                    </div>
                </div>
                <div>
                    <Footer/>
                </div>
            </div>
        )

    }
}

export default Caminhos;
