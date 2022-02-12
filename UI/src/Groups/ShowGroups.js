import 'bootstrap/dist/css/bootstrap.min.css';
import React, {useEffect, useState} from "react";
import './ShowGroups.css';
import Footer from "../Componente/PageConstructor/Footer";
import Header2 from "../Componente/PageConstructor/Header2";
import Card from "react-bootstrap/Card";

function Groups() {

    useEffect(async () => {
        document.getElementById("cards").style.visibility = "hidden";
        document.getElementById("size").style.visibility = "hidden";
        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);
        const items = await fetch("api/Tag/" + userToken, {})
        const tags = await items.json();
        setTagsUser(tags);
        const x = document.getElementById("tags");
        let texto = x.placeholder;
        for(const t of tags) {
            texto += t.nome
        }
        x.placeholder = texto
        const y = document.getElementById("NTags");
        y.setAttribute("max", tags.length);
    }, [])

    const [listTagsUser, setTagsUser] = useState([]);

    const [listGroup, setGroup] = useState([]);

    const [listTags, setTags] = useState([]);

    function errorPopUp() {
        let flag = false, count = 0;
        const tagsText = document.getElementById("tags");
        const NTags = document.getElementById("NTags");
        const array = tagsText.value.split(",");

        if(NTags !== array.length){
            //do nothing
        }

        listTagsUser.forEach(function(i){
            if(array.includes(i.nome)) {
                flag = true;
                count++;
            }
        });

        if(count !== array.length) {
            flag = false;
        }

        if(flag === false) {
            let modal = document.getElementById("modal")
            let closeBtn = document.getElementById("closeButton")

            modal.style.display = "block";
            closeBtn.addEventListener("click", () => {
                modal.style.display = "none"
            })
        }

        return flag;
    }

    async function communicationWithAPI() {
        const tokenString = sessionStorage.getItem('userName');
        const userName = JSON.parse(tokenString);
        const nElem = document.getElementById("Nelements");
        const nTags = document.getElementById("NTags");
        const tagsText = document.getElementById("tags");

        const items = await fetch("/prolog/sugerir_grupos?nome=" + userName + "&numeroElementos=" + nElem.value +
            "&numeroTags=" + nTags.value + "&listaObrigatoria=" + tagsText.value, {})
        const info = await items.json();

        setGroup(info[0]);
        setTags(info[1]);

        let flag = errorPopUp();

        if(flag) {
            document.getElementById("cards").style.visibility = "visible";
            document.getElementById("size").style.visibility = "visible";
        } else {
            clear();
        }
    }

    function clear() {
        document.getElementById("cards").style.visibility = "hidden";
        document.getElementById("size").style.visibility = "hidden";
        document.getElementById("Nelements").value = "";
        document.getElementById("NTags").value = "";
        document.getElementById("tags").value = "";
    }

    return (
        <div className="editMood">
            <Header2/>
            <div className="postsBackground">
                <div className="centerPost">
                    <header className="PageTitlePost"><strong style={{color:"white"}}>Group Suggestion</strong></header>
                        <div className="tabelaNumeros">
                            <div className="tabElem">
                                <header style={{color:"white"}}>Minimum Number of Elements:</header>
                                <input className="tabElem" type="number" min={1} id="Nelements"/>
                            </div>
                            <div className="tabTags">
                                <header style={{color:"white"}}>Amount of Tags in common:</header>
                                <input className="tabTags" type="number" min={1} id="NTags"/>
                            </div>
                        </div>
                        <header style={{color:"white"}}>Tags:</header>
                        <textarea className="tagsPost" id="tags" placeholder="Put your tags here separate with commas. Your Tags are: "/>
                    <div className="ButtonDiv">
                        <button onClick={communicationWithAPI} className="Button">Suggest Group</button>
                    </div>
                    <div className="ButtonDiv">
                        <button onClick={clear} className="Button">Clear Input</button>
                    </div>
                    <div id="size" className="SizeGroupDiv">
                        <Card style={{width: '18rem', backgroundColor: 'white', textAlign: 'left'}}>
                            <Card.Body>
                                <Card.Text style={{color:'red'}}> Size of Your Group Suggestion: {listGroup.length}</Card.Text>
                            </Card.Body>
                        </Card>
                    </div>
                    <div id= "cards" className="Cards">
                    <Card className="CardGroup" style={{width: '18rem', backgroundColor: 'white', textAlign: 'left'}}>
                        <Card.Body>
                            <Card.Text style={{color:'red'}}> Your Group Suggestion: </Card.Text>
                            {listGroup.map(function (item) {
                                return (<ul className="card">
                                    <Card.Text>
                                        {item}
                                    </Card.Text>
                                </ul>)
                            })}
                            </Card.Body>
                    </Card>
                        <Card className="CardTags" style={{width: '18rem', backgroundColor: 'white', textAlign: 'left'}}>
                            <Card.Body>
                                <Card.Text style={{color:'red'}}> Tags in Common: </Card.Text>
                                {listTags.map(function (item) {
                                    return (<ul className="card">
                                        <Card.Text>
                                            {item}
                                        </Card.Text>
                                    </ul>)
                                })}
                            </Card.Body>
                        </Card>
                    </div>
                </div>
            </div>
            <div className="modal" id="modal">
                <div className="modal_content">
                    <span className="close" id="closeButton">&times;</span>
                    <p style={{color:"white"}}>Sorry, but we can´t suggest you a group with these parameters... :(</p>
                </div>
            </div>
            <Footer/>
        </div>
    );
}

export default Groups;