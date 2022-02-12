import React, { Component } from 'react'
import {Form} from "react-bootstrap";

export default class SearchBars extends Component {

    changeEmail = (e) => {
        this.props.onChangeEmail(e.target.value);
    }
    changeName= (e) => {
        this.props.onChangeName(e.target.value);
    }
    changeTag = (e) => {
        this.props.onChangeTag(e.target.value);
    }

    render() {
        if (this.props.email=== true&&this.props.tag===true && this.props.username===true) {
            return (<div ><Form className="formForEmailNameOrTag" value={this.props.email} onChange={this.changeEmail.bind(this)} >
                <Form.Group  controlId="formText">
                    <Form.Control type="text" placeholder="Enter the email of the user"/>
                </Form.Group></Form>
                <Form className="formForEmailNameOrTag" value={this.props.username} onChange={this.changeName.bind(this)}>
                    <Form.Group controlId="formText">
                        <Form.Control type="text" placeholder="Enter the username of the user"/>
                    </Form.Group></Form>
                <Form className="formForEmailNameOrTag" value={this.props.tag} onChange={this.changeTag.bind(this)}>
                    <Form.Group  controlId="formText">
                        <Form.Control type="text" placeholder="Enter the tag of the user"/>
                    </Form.Group></Form>
            </div>)
        }
        if (this.props.email=== true&&this.props.tag===true) {
            return (<div ><Form className="formForEmailNameOrTag" value={this.props.email} onChange={this.changeEmail.bind(this)} >
                <Form.Group  controlId="formText">
                    <Form.Control type="text" placeholder="Enter the email of the user"/>
                </Form.Group></Form>
                <Form className="formForEmailNameOrTag" value={this.props.tag} onChange={this.changeTag.bind(this)}>
                <Form.Group  controlId="formText">
                    <Form.Control type="text" placeholder="Enter the tag of the user"/>
                </Form.Group></Form>
            </div>)
        }
        if (this.props.email=== true&&this.props.username===true) {
            return (<div><Form className="formForEmailNameOrTag" value={this.props.email} onChange={this.changeEmail.bind(this)} >
                <Form.Group  controlId="formText">
                    <Form.Control type="text" placeholder="Enter the email of the user"/>
                </Form.Group></Form>
                <Form className="formForEmailNameOrTag" value={this.props.username} onChange={this.changeName.bind(this)}>
                    <Form.Group  controlId="formText">
                        <Form.Control type="text" placeholder="Enter the username of the user"/>
                    </Form.Group></Form>
            </div>)
        }
        if (this.props.username===true&&this.props.tag=== true) {
            return (<div>
                <Form className="formForEmailNameOrTag" value={this.props.username} onChange={this.changeName.bind(this)}>
                    <Form.Group  controlId="formText">
                        <Form.Control type="text" placeholder="Enter the username of the user"/>
                    </Form.Group></Form>
                <Form className="formForEmailNameOrTag" value={this.props.tag} onChange={this.changeTag.bind(this)}>
                    <Form.Group controlId="formText">
                        <Form.Control type="text" placeholder="Enter the tag of the user"/>
                    </Form.Group></Form>
            </div>)
        }
        if (this.props.email=== true) {
            return (<div ><Form className="formForEmailNameOrTag" value={this.props.email} onChange={this.changeEmail.bind(this)} >
                <Form.Group  controlId="formText">
                    <Form.Control type="text" placeholder="Enter the email of the user"/>
                </Form.Group></Form></div>)
        }
        if (this.props.username === true) {
            return (<div><Form className="formForEmailNameOrTag" value={this.props.username} onChange={this.changeName.bind(this)}>
                <Form.Group controlId="formText">
                    <Form.Control type="text" placeholder="Enter the username of the user"/>
                </Form.Group></Form></div>)
        }
        if (this.props.tag === true) {
            return (<div><Form className="formForEmailNameOrTag" value={this.props.tag} onChange={this.changeTag.bind(this)}>
                <Form.Group controlId="formText">
                    <Form.Control type="text" placeholder="Enter the tag of the user"/>
                </Form.Group></Form></div>)
        }
        else{
            return(null)
        }
    }
}