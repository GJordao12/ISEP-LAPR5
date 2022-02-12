import 'bootstrap/dist/css/bootstrap.min.css';
import './FeedPost.css';

import React, {useEffect, useState} from "react";
import Footer from "../Componente/PageConstructor/Footer";
import Header2 from "../Componente/PageConstructor/Header2";
import './FeedComentario.css';
import {useLocation} from "react-router";
import {Button} from "reactstrap";
import FeedPost from "./FeedPost";


function FeedComentario() {
     useEffect(async () => {
         await getPostById();
     }, []);

    const [comentario, setcomentario] = useState(null);
    const location = useLocation();


    function DTO(comentario,username,listaTags) {
        this.id = comentario.id;
        this.idUser = comentario.idUser;
        this.likes = comentario.likes;
        this.dislikes = comentario.dislikes;
        this.texto = comentario.texto;
        this.listTags = comentario.listTags;
        this.data = comentario.data;
        this.username=username;
        this.listTags=listaTags;
    }

    const getPostById = async () => {
        let post=null;
        await fetch('/posts/post/ById/' + location.state.id)
            .then(async function ( response) {

                post = await response.json();
            })
            .catch(function (error) {
                console.log(error);
            });

        console.log(post);

        let listacomentario = [];

        for(let c of post.listComentarios) {
            let username =null;
            let lista=[];
            await fetch("api/User/".concat(c.idUser), {})
                .then(res => res.json())
                .then(result => username=result);
            for (let tag of c.listTags) {
                fetch("api/Tag/TagById/".concat(tag), {})
                    .then(res => res.json())
                    .then(res => lista.push(res.nome));
            }
            listacomentario.push(new DTO(c,username,lista));
        }
        console.log(listacomentario)

        await setcomentario(listacomentario);
    }


    function like(item) {
        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);
        fetch('posts/comentario/gostos', {
            method: 'PUT',
            headers: {
                'Content-type': 'application/json; charset=UTF-8' // Indicates the content
            },
            body: JSON.stringify({
                "comentarioId": item.id,
                "status": 'like',
                "userId": userToken
            })
        })
            .then(response => response.json())
            .then(data => console.log(data))
            .catch(err => console.log(err))

    }


    function dislike(item) {
        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);
        fetch('posts/comentario/gostos', {
            method: 'PUT',
            headers: {
                'Content-type': 'application/json; charset=UTF-8' // Indicates the content
            },
            body: JSON.stringify({
                "comentarioId": item.id,
                "status": 'dislike',
                "userId": userToken

            })
        })
            .then(response => response.json())
            .then(data => console.log(data))
            .catch(err => console.log(err))
    }

    function showSuccessPopUp() {
        let modal = document.getElementById("modal")
        let closeBtn = document.getElementById("closeButton")
        let popUpText = document.getElementById("popUpText")

        modal.style.display = "block";
        popUpText.innerText = "✅ - Comment Published  Successfully";

        closeBtn.addEventListener("click", () => {
            modal.style.display = "none"
        })
    }

    function getTags() {
        let tagsText = document.getElementById("tag");

        if (tagsText.value === null || tagsText.value === "") {
            return [];
        }

        return tagsText.value.split(/(\s+)/).filter(function (e) {
            return e.trim().length > 0;
        });
    }

    function communicationWithAPI() {
        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);

        let text = document.getElementById("text");
        let tagsText = document.getElementById("tag");

            let tags = getTags();
            fetch('posts/comentario', {
                method: 'POST',
                headers: {
                    'Content-type': 'application/json; charset=UTF-8' // Indicates the content
                },
                body: JSON.stringify({
                    "idUser": userToken,
                    "idPost":location.state.id,
                    "texto": text.value,
                    "listTags": tags
                })
            })
                .then(function (response) {
                    console.log(response);
                })
                .catch(function (error) {
                    console.log(error);
                });

            showSuccessPopUp();
            text.value = "";
            tagsText.value = "";

    }
if (comentario===null){
    return(<div>
        <Header2/>
        <Footer/>
        </div>
        )
}
    return (
        <div>
            <Header2/>
            <div className="pedidoIntroducaoBackground">
            <nav className="navbar navbar-expand-sm navbar-dark"><img src="http://bootdey.com/img/Content/avatar/avatar1.png" width="20"
                                                                      height="20"
                                                                      className="d-inline-block align-top rounded-circle"
                                                                      alt=""/> <a className="navbar-brand ml-2" href="/FeedPost"
                                                                                  data-abc="true"
                                                                                  style={{color: "white"}}>{location.state.userName.username}</a>
                <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarColor02"
                        aria-controls="navbarColor02" aria-expanded="false" aria-label="Toggle navigation"><span
                    className="navbar-toggler-icon"/></button>

            </nav>

            <section>
                <div className="container">
                    <div className="row">
                        <div className="col-sm-5 col-md-6 col-12 pb-4">
                            <h2 style={{color: "white"}}>Comments</h2>
                            {comentario.map(function (item, index) {
                                return (
                                    <div className="comment mt-4 text-justify float-left"><img
                                    src="http://bootdey.com/img/Content/avatar/avatar1.png" alt="" className="rounded-circle" width="40"
                                    height="40"/>
                                    <h4 style={{color:"white"}} className="ms-3" >{item.username.username}</h4><br/> <br/><span style={{color: "white"}}>Date: {" "+item.data.substring(0,10)+" "+item.data.substring(11,19)}</span> <br/>
                                    <p style={{color: "white"}}>{item.texto}</p>
                                        <p style={{color: "white"}}>{"#"+item.listTags.join(" #")}</p>
                                        <Button  className="fa fa-thumbs-up g-pos-rel g-top-1 g-mr-3 mb-2" onClick={like.bind(this, item)}>{item.likes.length}</Button>
                                        <Button className="fa fa-thumbs-down g-pos-rel g-top-1 g-mr-3 mb-2 ms-1"onClick={dislike.bind(this, item)}>{item.dislikes.length}</Button>
                                </div>)
                            })}

                        </div>
                        <div className="col-lg-4 col-md-5 col-sm-4 offset-md-1 offset-sm-1 col-12 mt-4">
                            <form id="algin-form">
                                <div className="form-group">
                                    <h4 style={{color:"white"}}>Leave a comment</h4> <label htmlFor="message">Message</label> <textarea
                                    name="msg" id="text" msg cols="30" rows="5" className="form-control"
                                    style={{backgroundColor: "white",height:"5px"}}/>
                                    <br/><label htmlFor="message">Tags</label>
                                    <textarea
                                        name="msg" id="tag" msg cols="30" rows="5" className="form-control"
                                        style={{backgroundColor: "white",height:"5px"}}/>
                                </div>
                                <div className="form-group" style={{paddingLeft:"90px"}}>
                                    <button type="button" id="post" onClick={communicationWithAPI} className="btn">Post Comment</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </section>
            </div>
            <Footer/>
            <div className="modal" id="modal">
                <div className="modal_content">
                    <span className="close" id="closeButton">&times;</span>
                    <p id="popUpText"/>
                </div>
            </div>

        </div>
    )


}

export default FeedComentario;