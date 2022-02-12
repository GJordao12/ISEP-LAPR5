import './Search.css';
import React, {Component} from 'react'
import {Button} from "reactstrap";
import {Form} from "react-bootstrap";
import TableWithSearchResults from "./TableWithSearchResults";
import SearchBars from "./SearchBars";
import Footer from "../Componente/PageConstructor/Footer";
import Header2 from "../Componente/PageConstructor/Header2";


class Search extends Component {
    constructor(props) {
        super(props);
        this.state = {
            'data': [],
            visibleForm: false,
            searchFieldEmail: '',
            searchFieldName: '',
            searchFieldTag: '',
            textToSend: '',
            id: '',
            num: 1,
            searchFields: ["email", "tag", "username"],
            username: false,
            tag: false,
            email: false
        };
    }
    getUsersWithTagNameAndEmail() {
        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);
        fetch("api/Pesquisa/" + userToken + "/"+this.state.searchFieldName+"/" + this.state.searchFieldEmail + "/" +this.state.searchFieldTag)
            .then(res => res.json())
            .then(result => this.setState({'data': result}))
    }
    getUsersWithEmailAndTag() {
        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);
        fetch("api/Pesquisa/" + userToken + "/null/" + this.state.searchFieldEmail + "/" +this.state.searchFieldTag)
            .then(res => res.json())
            .then(result => this.setState({'data': result}))
    }

    getUsersWithEmailAndName() {
        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);
        fetch("api/Pesquisa/" + userToken + "/"+this.state.searchFieldName+"/"+this.state.searchFieldEmail+"/null")
            .then(res => res.json())
            .then(result => this.setState({'data': result}))
    }

    getUsersWithTagAndName() {
        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);
        fetch("api/Pesquisa/" + userToken + "/"+this.state.searchFieldName+"/null/"+this.state.searchFieldTag)
            .then(res => res.json())
            .then(result => this.setState({'data': result}))
    }

    getUsersWithEmail() {
        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);
        fetch("api/Pesquisa/" + userToken + "/null/" + this.state.searchFieldEmail + "/null")
            .then(res => res.json())
            .then(result => this.setState({'data': result}))

    }

    getUsersWithName() {
        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);
        fetch("api/Pesquisa/" + userToken + "/" + this.state.searchFieldName + "/null/null")
            .then(res => res.json())
            .then(result => this.setState({'data': result})
            );
    }

    getUsersWithTag() {
        const tokenString = sessionStorage.getItem('token');
        const userToken = JSON.parse(tokenString);
        fetch("api/Pesquisa/" + userToken + "/null/null/" + this.state.searchFieldTag)
            .then(res => res.json())
            .then(result => this.setState({'data': result})
            );
    }

    getButtonToShow() {
        if (this.state.tag === true && this.state.username === true && this.state.email===true) {
            return (<div>< Button className="fa fa-users"
                                  onClick={() => this.getUsersWithTagNameAndEmail()}
                                  outline
                                  color="success">Search for users</Button></div>)
        }
        if (this.state.tag === true && this.state.username === true) {
            return (<div>< Button className="fa fa-users"
                                  onClick={() => this.getUsersWithTagAndName()}
                                  outline
                                  color="success">Search for users</Button></div>)
        }
        if (this.state.email === true && this.state.username === true) {
            return (<div>< Button className="fa fa-users"

                                  onClick={() => this.getUsersWithEmailAndName()}
                                  outline
                                  color="success">Search for users</Button></div>)
        }
        if (this.state.email === true && this.state.tag === true) {
            return (<div>< Button className="fa fa-users"

                                  onClick={() => this.getUsersWithEmailAndTag()}
                                  outline
                                  color="success">Search for users</Button></div>)
        }
        if (this.state.email === true) {
            return (<div>< Button className="fa fa-users"

                                  onClick={() => this.getUsersWithEmail()}
                                  outline
                                  color="success">Search for users</Button></div>)
        }
        if (this.state.username === true) {
            return (<div>< Button className="fa fa-users"
                                  style={{

                                  }}
                                  onClick={() => this.getUsersWithName()}
                                  outline
                                  color="success">Search for users</Button></div>)
        }
        if (this.state.tag === true) {
            return (<div>< Button className="fa fa-users"
                                  style={{

                                  }}
                                  onClick={() => this.getUsersWithTag()}
                                  outline
                                  color="success">Search for users</Button></div>)
        }
    }


    onCheckboxClick(item) {
        if (item === 'username') {
            this.setState({username: !this.state.username});
        }
        if (item === 'tag') {
            this.setState({tag: !this.state.tag});
        }
        if (item === 'email') {
            this.setState({email: !this.state.email});
        }
    }

    change = (item) => (
        this.setState((state) => {
            return {visibleForm: true, id: item.id}
        })
    )
    changeSearchFieldEmail = (item) => (
        this.setState((state) => {
            return {searchFieldEmail: item}
        })
    )
    changeSearchFieldName = (item) => (
        this.setState((state) => {
            return {searchFieldName: item}
        })
    )
    changeSearchFieldTag = (item) => (
        this.setState((state) => {
            return {searchFieldTag: item}
        })
    )


    render() {
        let change2 = () => {
            this.setState({visibleForm: false});
            sendConnectionRequest(this.state.id, this.state.textToSend);
        }

        function sendConnectionRequest(id, textToSend) {
            const tokenString = sessionStorage.getItem('token');
            const userToken = JSON.parse(tokenString);
            fetch('api/PedidoLigacao/', {
                method: 'POST',
                headers: {
                    'Content-type': 'application/json; charset=UTF-8' // Indicates the content
                },
                body: JSON.stringify({
                    "remetente": userToken,
                    "destinatario": id,
                    "texto": textToSend
                })
            })
                .then(response => response.json())
                .then(data => console.log(data))
                .catch(err => console.log(err))
        }


        return (
            <div>
                <Header2 textoApresentar1={"Search for a friend"} textoApresentar2={"In this page you can search for a person that has an account in the game"}/>
                <div className="container1" >
                    <div className="checkboxSearchField">

                            <div style={{color:"white"}}>
                                Choose the field(s) to search by:
                            </div>
                            <div >
                                {this.state.searchFields.map(item => (
                                    <div >
                                        <input  id={item} onClick={() => this.onCheckboxClick(item)}
                                               type="checkbox"/>
                                        <label style={{color:"white"}}>
                                            {item}
                                        </label>
                                    </div>))
                                }
                            </div>
                    </div>
                    <div className="buttonGetUsersSearch">
                        {this.getButtonToShow()}
                    </div>

                    <div className ="searchBarsForEmailTagAndName"><SearchBars username={this.state.username}
                                                             email={this.state.email}
                                                             tag={this.state.tag}
                                                             onChangeEmail={this.changeSearchFieldEmail}
                                                             onChangeName={this.changeSearchFieldName}
                                                             onChangeTag={this.changeSearchFieldTag}
                    /></div>
                    <div className="tableSearchUser">
                        <TableWithSearchResults users={this.state.data}
                                                onChange={this.change}/></div>
                    {this.state.visibleForm ? (
                            <div className="formTextToSendInSearch"><Form value={this.state.textToSend}
                              onChange={event => this.setState({textToSend: event.target.value})}>
                            <Form.Group  controlId="formText">
                                <Form.Control type="text" placeholder="Enter the text to send"/>
                            </Form.Group>
                            < Button className="fa fa-paper-plane"
                                     onClick={() => change2()}
                                     outline
                                     color="success">Send Connection Request</Button>
                        </Form></div>
                    ) : ''}
                </div>
                <Footer/>
            </div>
        )

    }
}

export default Search;
