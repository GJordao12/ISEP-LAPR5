import 'bootstrap/dist/css/bootstrap.min.css';
import './FeedPost.css';

import React, {useEffect, useState} from "react";
import Footer from "../Componente/PageConstructor/Footer";
import Header2 from "../Componente/PageConstructor/Header2";
import {Button} from "reactstrap";
import {useNavigate} from "react-router";

function FeedPost() {
    useEffect(() => {
        getPosts();
    }, []);

    const [listapost, setpost] = useState(null);


    function DTO(post, username, lista) {
        this.id = post.id;
        this.idUser = post.idUser;
        this.userName = username;
        this.likes = post.likes;
        this.dislikes = post.dislikes;
        this.texto = post.texto;
        this.listTags = post.listTags;
        this.listTagsNomes = lista;
        this.data = post.data;
        this.listComentarios = post.listComentarios;
    }

    const getPosts = async () => {
        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);
        let posts = null;
        await fetch('/posts/post/users/' + userToken)
            .then(async function (response) {

                posts = await response.json();
            })
            .catch(function (error) {
                console.log(error);
            });
        let listapost = [];
        for (const l of posts) {
            let lista = [];
            for (let tag of l.listTags) {
                fetch("api/Tag/TagById/".concat(tag), {})
                    .then(async res => await res.json())
                    .then(res => lista.push(res.nome));
            }
            let username = null;
             await fetch("api/User/".concat(l.idUser), {})
                .then( res => res.json())
                .then(result => username = result)

            listapost.push(new DTO(l, username, lista));

        }

        await setpost(listapost);

    }


    function like(item) {
        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);
        console.log(item)
        fetch('posts/post/gostos', {
            method: 'PUT',
            headers: {
                'Content-type': 'application/json; charset=UTF-8' // Indicates the content
            },
            body: JSON.stringify({
                "postId": item.id,
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
        fetch('posts/post/gostos', {
            method: 'PUT',
            headers: {
                'Content-type': 'application/json; charset=UTF-8' // Indicates the content
            },
            body: JSON.stringify({
                "postId": item.id,
                "status": 'dislike',
                "userId": userToken

            })
        })
            .then(response => response.json())
            .then(data => console.log(data))
            .catch(err => console.log(err))
    }

    const navigate = useNavigate();

    const toComponentB = (dados) => {
        navigate('/FeedComentario', {state: dados});
    }

    if (listapost === null) {
        return (<div>
            <Header2/>

            <Footer/>
        </div>)
    }

    return (
        <div>
            <Header2/>
            <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet"/>
            <div className="pedidoIntroducaoBackground">
            <div className="container bootstrap snippets bootdey ">
                <div className="row">
                    {listapost.map(function (item, index) {
                        return (
                            <div className="post-list mt-4 mb-4">
                                <div className="row" style={{backgroundColor:"white",borderRadius:"5px"}}>
                                    <div className="col-sm-2">
                                        <div className="picture">
                                            <img className="img-account-profile rounded-circle mb-2 mt-2"
                                                 style={{width: "75%"}}
                                                 src="http://bootdey.com/img/Content/avatar/avatar1.png" alt=""/>
                                        </div>
                                    </div>
                                    <div className="col-sm-6">
                                        <h4>
                                            <a style={{color: "#269abc"}}
                                               className="username">{item.userName.username + " "}</a>
                                            <label className="label label-info">{"#"+item.listTagsNomes.join(' #')}</label>
                                        </h4>
                                        <h5>
                                            <i className="fa fa-calendar"/>
                                            {" " + item.data.substring(0, 10) + " " + item.data.substring(11, 19)}
                                        </h5>
                                        <p className="description">{item.texto}</p>
                                        <Button className="fa fa-thumbs-up g-pos-rel g-top-1 g-mr-3"
                                                onClick={like.bind(this, item)}>{item.likes.length}</Button>
                                        <Button className="fa fa-thumbs-down g-pos-rel g-top-1 g-mr-3"
                                                onClick={dislike.bind(this, item)}>{item.dislikes.length}</Button>
                                    </div>
                                    <div className="col-sm-4 " data-no-turbolink="" style={{paddingTop:"60px"}}>

                                        <a className="btn btn-info btn-download btn-round pull-right makeLoading"
                                           onClick={() => {
                                               toComponentB(item)
                                           }} href="/FeedComentario">

                                            <i className="fa fa-share "/> View
                                        </a>
                                    </div>
                                </div>
                            </div>)
                    })}


                </div>
            </div>
            </div>
            <Footer/>

        </div>
    )


}

export default FeedPost;