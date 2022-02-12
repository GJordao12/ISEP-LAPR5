import React from "react";
import TagsFromUsersTable from "./TagsFromUsersTable";
import HomeLeftMenu from "../Componente/HomeLeftMenu/HomeLeftMenu";

function TagsFromUsers(){
    return (
        <div className="container2">
            <HomeLeftMenu/>
            <div className="tagsFromRelacoesUserNoAccount">
            <TagsFromUsersTable/>
            </div>
        </div>
    )
}
export default TagsFromUsers;