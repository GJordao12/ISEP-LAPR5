import 'bootstrap/dist/css/bootstrap.min.css';
import './Header.css'
import DropdownTriggerExample from "../PageConstructor/Dropdown";
import React from "react";

const Header2 = (props) => {
    return(
        <div>
            <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet"
                  id="bootstrap-css"/>
            <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
            <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>


            <nav className="navbar navbar-expand-md navbar-dark fixed-top" id="banner" style={{position:"absolute"}}>
                <div className="container">

                    <a className="navbar-brand mb-3" href="/AboutUs"><span>Graphs</span> 4<span>Social</span> </a>


                    <button className="navbar-toggler" type="button" data-toggle="collapse"
                            data-target="#collapsibleNavbar">
                        <span className="navbar-toggler-icon"></span>
                    </button>


                    <div  id="collapsibleNavbar"  >
                        <ul className="navbar-nav ml-auto " style={{paddingTop:"2%"}} >
                            <li className="nav-item corAmarela" ><a href="/Graph" className="nav-link corAmarela" style={{color:"white",fontSize:"13px"}}>Social Network</a></li>
                            <li className="nav-item corAmarela"><a href="/Graph3D" className="nav-link corAmarela" style={{color:"white",fontSize:"13px"}}>Social Network 3D</a></li>
                            <li className="nav-item corAmarela"><a href="/Tamanho" className="nav-link corAmarela" style={{color:"white",fontSize:"13px"}}>Network Size</a></li>
                            <li className="nav-item corAmarela"><a href="/LeaderBoards" className="nav-link corAmarela" style={{color:"white",fontSize:"13px"}}>LeaderBoards</a></li>
                            <li className="nav-item corAmarela"><a href="/Post" className="nav-link corAmarela" style={{color:"white",fontSize:"13px"}}>Post</a></li>
                            <li className="nav-item corAmarela"><a href="/FeedPost" className="nav-link corAmarela" style={{color:"white",fontSize:"13px"}}>FeedPost</a></li>
                            <li className="nav-item corAmarela"><a href="/Groups" className="nav-link corAmarela" style={{color:"white",fontSize:"13px"}}>Groups</a></li>
                            <li>
                                <a className="corAmarela" style={{paddingTop:"10%"}}>
                                    <DropdownTriggerExample/>
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
            <div className="banner">
                    <div className="banner-text1">



                    </div>
            </div>
        </div>
    );
}
export default Header2;