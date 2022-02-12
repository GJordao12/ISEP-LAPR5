import React from "react";
import ScrollableTable from "../VisualizarPedidosLigacaoPendentes/ScrollableTable";
import Header2 from "../Componente/PageConstructor/Header2";

function PedidosLigacao(){
    return (
        <div>
            <Header2/>
            <body className="pedidoIntroducaoBackground" style={{paddingBottom:"41%"}}>
            <ScrollableTable/>
            </body>
        </div>
    )
}
export default PedidosLigacao;