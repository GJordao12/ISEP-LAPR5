import 'bootstrap/dist/css/bootstrap.min.css';
import './Header.css'
import Header from '../PageConstructor/Header'

import React, { Component } from 'react';
import Body from "./Body";


class InitialPage extends Component {
    render() {
        return (
                <div>
                    <Body/>
                </div>
        );
    }
}
export default InitialPage;