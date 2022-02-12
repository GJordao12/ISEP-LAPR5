import './NotSupportedYet.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import React from 'react';
import HomeLeftMenu from '../HomeLeftMenu/HomeLeftMenu'

function NotSupportedYet() {
    return (
        <div className="notSupportedYet">
            <HomeLeftMenu/>
            <div className="page">
                <div className="NotSupportedYetSpace">
                    <p>Not Supported Yet</p>
                </div>

            </div>
        </div>
    );
}

export default NotSupportedYet;