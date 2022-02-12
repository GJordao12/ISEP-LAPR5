import React from "react";
import './PedidosIntroducao.css'
import ScrollableTable from "./ScrollableTable";
import Header2 from "../Componente/PageConstructor/Header2";

function PedidosIntroducao() {
    return (
        <div>
            <Header2/>
            <body className="pedidoIntroducaoBackground" >
            <ScrollableTable/>
            </body>
        </div>
    )
}

export default PedidosIntroducao;