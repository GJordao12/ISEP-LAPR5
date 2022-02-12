import './Post.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import React from "react";
import Footer from "../Componente/PageConstructor/Footer";
import Header2 from "../Componente/PageConstructor/Header2";

function Post() {

    function communicationWithAPI() {
        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);

        let flag = true;

        let text = document.getElementById("text");
        let textError = document.getElementById("textError");
        let tagsText = document.getElementById("tags");

        if (text.value === null || text.value === "") {
            textError.style.visibility = "visible";
            flag = false;
        } else {
            textError.style.visibility = "hidden";
        }

        if (flag) {
            let tags = getTags();
            fetch('/posts/post', {
                method: 'POST',
                headers: {
                    'Content-type': 'application/json; charset=UTF-8' // Indicates the content
                },
                body: JSON.stringify({
                "idUser": userToken,
                "texto": text.value,
                "listTags": tags})
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
    }

    function getTags() {
        let tagsText = document.getElementById("tags");

        if (tagsText.value === null || tagsText.value === "") {
            return [];
        }

        let tags = tagsText.value.split(/(\s+)/).filter(function (e) {
            return e.trim().length > 0;
        });

        let aux = [];

        for (let i = 0; i < tags.length; i++) {
            if ((i === tags.length - 1) || ((i !== tags.length - 1) && (!(tags.slice(i + 1, tags.length).includes(tags[i]))))) {
                aux.push(tags[i])
            }
        }

        return aux;
    }

    function showSuccessPopUp() {
        let modal = document.getElementById("modal")
        let closeBtn = document.getElementById("closeButton")
        let popUpText = document.getElementById("popUpText")

        modal.style.display = "block";
        popUpText.innerText = "✅ - Post Published Successfully";

        closeBtn.addEventListener("click", () => {
            modal.style.display = "none"
        })
    }

    return (
        <div className="editMood">
            <Header2/>
            <div className="postsBackground">
                <div className="centerPost">
                    <header className="PageTitlePost"><strong style={{color: "white"}}>Create Post</strong></header>
                    <div className="post">
                        <textarea className="textPost" id="text" placeholder="What are you thinking?"/>
                        <p id="textError" style={{visibility: "hidden", color: "red"}}>Please enter a valid text.</p>
                        <header style={{color: "white"}}>Your Post Tags:</header>
                        <textarea className="tagsPost" id="tags"
                                  placeholder="Put your tags here separate with spaces (e.g. ronaldo manchester)"/>
                    </div>
                    <div className="PublicButtonDiv">
                        <button onClick={communicationWithAPI} className="PublicButton">Public</button>
                    </div>
                </div>
            </div>
            <Footer/>
            <div className="modal" id="modal">
                <div className="modal_content">
                    <span className="close" id="closeButton">&times;</span>
                    <p id="popUpText"/>
                </div>
            </div>
        </div>
    );
}

export default Post;