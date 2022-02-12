import './Login.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import HomeLeftMenu from '../Componente/HomeLeftMenu/HomeLeftMenu'


function Login() {

    let user = null;

    (function (global) {

        if(typeof (global) === "undefined") {
            throw new Error("window is undefined");
        }

        var _hash = "!";
        var noBackPlease = function () {
            global.location.href += "#";

            // Making sure we have the fruit available for juice (^__^)
            global.setTimeout(function () {
                global.location.href += "!";
            }, 50);
        };

        global.onhashchange = function () {
            if (global.location.hash !== _hash) {
                global.location.hash = _hash;
            }
        };

        global.onload = function () {
            noBackPlease();

            // Disables backspace on page except on input fields and textarea..
            document.body.onkeydown = function (e) {
                var elm = e.target.nodeName.toLowerCase();
                if (e.which === 8 && (elm !== 'input' && elm  !== 'textarea')) {
                    e.preventDefault();
                }
                // Stopping the event bubbling up the DOM tree...
                e.stopPropagation();
            };
        }
        })(window);

    function myFunction() {
        let x = document.getElementById("password");
        if (x.type === "password") {
            x.type = "text";
        } else {
            x.type = "password";
        }
    }

    async function getUser(username) {
        await fetch("api/User/name/" + username)
            .then(async res => {
                if (res.ok) {
                    await res.json().then(result => user = result)
                } else if (res.status === 404) {
                    await res.json().then(user = null)
                }
            })
    }

    async function verification() {
        let username = document.getElementById("username");
        let usernameError = document.getElementById("userNameError");
        let password = document.getElementById("password");
        let passwordError = document.getElementById("passwordError");

        let flag = true;

        if (username.value.length === 0 || username.value.indexOf(' ') >= 0) {
            usernameError.innerText = "Please enter a valid User Name.";
            usernameError.style.visibility = "visible";
            flag = false;
        } else {
            usernameError.style.visibility = "hidden";
        }

        if (password.value.length === 0) {
            passwordError.innerText = "Please enter a valid Password.";
            passwordError.style.visibility = "visible";
            flag = false;
        } else {
            passwordError.style.visibility = "hidden";
        }

        if (flag) {
            await getUser(username.value)

            if (user == null) {
                usernameError.innerText = "User Name doesn't exist.";
                usernameError.style.visibility = "visible";
            }

            if (user != null && password.value.length > 0 && username.value.length > 0) {
                if (password.value === user.password) {
                    sessionStorage.setItem('token', JSON.stringify(user.id));
                    sessionStorage.setItem('userName', JSON.stringify(user.username));
                    sessionStorage.setItem('email',JSON.stringify(user.email));
                    window.location.href = "/InitialPage";
                } else {
                    passwordError.innerText = "Wrong Password.";
                    passwordError.style.visibility = "visible";
                }
            }
        }
    }

    return (
        <div className="login">
            <HomeLeftMenu/>
            <div className="pageLogin">
                <div className="pageHeaderLogin">
                    <header><strong>Login in G4S - Social Network Game</strong></header>
                </div>
                <div className="loginSpace">
                    <h3 style={{color:"black"}}>Username:</h3>
                    <input type="email" id="username" placeholder="userName"/>
                    <p id="userNameError" style={{visibility: "hidden", color: "red"}}>Please enter a valid User
                        Name.</p>
                    <h3 style={{color:"black"}}>Password:</h3>
                    <input className="mb-2" type="password" id="password" placeholder="password"/> <br/>
                    <input class="chbx" type="checkbox" onClick={myFunction}/>Show Password
                    <p id="passwordError" style={{visibility: "hidden", color: "red"}}>Please enter a valid
                        Password.</p>
                    <div className="btnLogin" onClick={verification}>
                        <button className="w-100 btn btn-lg btn-primary">Sign In</button>
                    </div>
                </div>
                <footer className="rodapeLogin" style={{color:"white"}}>© 2021 Copyright: G4S - Social Network Game</footer>
            </div>
        </div>
    );
}

export default Login;