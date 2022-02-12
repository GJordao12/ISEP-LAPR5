import React from "react";
import ScrollableTable from "./ScrollableTable";
import '../Componente/PageConstructor/Footer.css'
import Footer from "../Componente/PageConstructor/Footer";
import Header2 from "../Componente/PageConstructor/Header2";

function UtilizadorObjetivo() {
    return (
        <div >
            <Header2 />

            <ScrollableTable/>

            <Footer/>
        </div>
    )
}

export default UtilizadorObjetivo;