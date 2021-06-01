import { Link } from "react-router-dom";
import React from 'react';

function Nav() {
    return(

        <nav className="navbar navbar-dark bg-dark">
            <div className="container">
                <Link to="/">
                <a className="navbar-brand">Home</a>
            </Link>
            </div> 
        </nav>
    )
}

export default Nav;