import React from "react";
import HomeLeftMenu from "../Componente/HomeLeftMenu/HomeLeftMenu";
import TagsFromRelacoesTable from "./TagsFromRelacoesTable";

function TagsFromRelacoes(){
    return (
        <div className="container2">
            <HomeLeftMenu/>
            <div className="tagsFromRelacoesUserNoAccount">
            <TagsFromRelacoesTable/>
            </div>
        </div>
    )
}
export default TagsFromRelacoes;