import './EditarEstadoDeHumor.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import MoodTable from "./MoodTable";
import React from "react";
import Footer from "../Componente/PageConstructor/Footer";
import Header2 from "../Componente/PageConstructor/Header2";

function EditarEstadoDeHumor() {

    return (
        <div className="editMood">
            <Header2/>
            <div className="mooodTableBackground">
                <div className="pageHeaderHumor">
                    <header><strong style={{color:"white"}}>Edit Mood</strong></header>
                </div>
                <div className="editMoodSpace">
                    <MoodTable/>
                </div>
            </div>
            <Footer/>
        </div>
    );
}

export default EditarEstadoDeHumor;