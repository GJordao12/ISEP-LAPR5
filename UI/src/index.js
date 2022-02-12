import React from 'react';
import ReactDOM from 'react-dom';
import './ResponderPedidos/ResponderPedido.css';
import reportWebVitals from './reportWebVitals';
import InitialPage from "./Componente/PageConstructor/InitialPage";
import {
    BrowserRouter
} from 'react-router-dom';
import {
    Routes,
    Route
} from 'react-router';
import Home from './Home/Home';
import CreateAccount from './CreateAccount/CreateAccount';
import NotSupportedYet from "./Componente/NotSupportedYet/NotSupportedYet";
import Login from "./Login/Login";
import TagsFromUsers from "./Tags/TagsFromUsers";
import Perfil from "./Perfil/Perfil";
import EditarRelacionamento from "./EditarRelacionamentos/EditarRelacionamento";
import ResponderPedido from "./ResponderPedidos/ResponderPedido";
import AceitarERejeitar from "./PedidosDeLigacao/AceitarERejeitar";
import UtilizadorObjetivo from "./UtilizadorObjetivo/UtilizadorObjetivo";
import PedidosLigacao from "./VisualizarPedidosLigacaoPendentes/PedidosLigacao";
import PedidosIntroducao from "./PedidosIntroducao/PedidosIntroducao";
import EditarEstadoDeHumor from "./EditarEstadoDeHumor/EditarEstadoDeHumor";
import RenderGraph from "./Graph/GraphNetwork";
import Tamanho from "./TamanhoRedeUtilizadores/Tamanho";
import Caminhos from "./Caminhos/Caminhos";
import TagsFromUser from "./Tags/TagsFromUser";
import TagsFromRelacoesTable from "./Tags/TagsFromRelacoesTable";
import TagsFromRelacoesPropio from "./Tags/TagsFromRelacoesPropio";
import RenderGraph3D from "./Graph3D/Graph3DNetwork";
import NetworkDimension from "./NetworkDimension/NetworkDimension";
import Post from "./Post/Post";
import Ligacoes from "./Connection&RelationshipStrength/Ligacoes"
import LeaderBoardNoAccounts from "./LeaderBoardNoAccount/LeaderBoardNoAccounts";
import LeaderBoards from "./LeaderBoard/LeaderBoards";
import TagsFromRelacoes from "./Tags/TagsFromRelacoes";
import FeedPost from "./FeedPost/FeedPost";
import Search from "./Search/Search";
import AboutUs from "./AboutUs/AboutUs";
import Groups from "./Groups/ShowGroups"
import FeedComentario from "./FeedPost/FeedComentario";




const styleLink = document.createElement("link");
styleLink.rel = "stylesheet";
styleLink.href = "https://cdn.jsdelivr.net/npm/semantic-ui/dist/semantic.min.css";
document.head.appendChild(styleLink);

ReactDOM.render(
    <React.StrictMode>
        <BrowserRouter>
            <Routes>
                <Route path='/' element={<Home/>}/>
                <Route path='/Home' element={<Home/>}/>
                <Route path='/AboutUs' element={<AboutUs/>}/>
                <Route path='/Login' element={<Login/>}/>
                <Route path='/CreateAccount' element={<CreateAccount/>}/>
                <Route path='/TagsFromUsers' element={<TagsFromUsers/>}/>
                <Route path='/TagsFromRelacoes' element={<TagsFromRelacoes/>}/>
                <Route path='/TagsFromRelacoesTable' element={<TagsFromRelacoesTable/>}/>
                <Route path='/TagsFromRelacoesPropio' element={<TagsFromRelacoesPropio/>}/>
                <Route path='/NotSupportedYet' element={<NotSupportedYet/>}/>
                <Route path='/Perfil' element={<Perfil/>}/>
                <Route path='/EditarRelacionamento' element={<EditarRelacionamento/>}/>
                <Route path='/ResponderPedido' element={<ResponderPedido/>}/>
                <Route path='/AceitarERejeitar' element={<AceitarERejeitar/>}/>
                <Route path='/UtilizadorObjetivo' element={<UtilizadorObjetivo/>}/>
                <Route path='/PedidosLigacao' element={<PedidosLigacao/>}/>
                <Route path='/PedidosIntroducao' element={<PedidosIntroducao/>}/>
                <Route path='/TagsFromUser' element={<TagsFromUser/>}/>
                <Route path='/Ligacoes' element={<Ligacoes/>}/>
                <Route path='/Search' element={<Search/>}/>
                <Route path='/InitialPage' element={<InitialPage/>}/>
                <Route path='/EditarEstadoDeHumor' element={<EditarEstadoDeHumor/>}/>
                <Route path='/Graph' element={<RenderGraph/>}/>
                <Route path='/Tamanho' element={<Tamanho/>}/>
                <Route path='/Paths' element={<Caminhos/>}/>
                <Route path='/Graph3D' element={<RenderGraph3D/>}/>
                <Route path='/NetworkDimension' element={<NetworkDimension/>}/>
                <Route path='/Post' element={<Post/>}/>
                <Route path='/ShowLeaderBoardsNoAccount' element={<LeaderBoardNoAccounts/>}/>
                <Route path='/LeaderBoards' element={<LeaderBoards/>}/>
                <Route path='/FeedPost' element={<FeedPost/>}/>
                <Route path='/Groups' element={<Groups/>}/>
                <Route path='/FeedComentario' element={<FeedComentario/>}/>


            </Routes>
        </BrowserRouter>
    </React.StrictMode>,
    document.getElementById('root')
);

reportWebVitals();
