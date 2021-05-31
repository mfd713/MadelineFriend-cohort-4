import { Link } from "react-router-dom";
import {useState, useEffect} from 'react';

function Home(props) {

    const handleDelete = (evt) =>{
        const toDeleteId = evt.target.id.substring(12);
        const result = window.confirm(`About to delete agent with ID ${toDeleteId}. Is this ok?`);
        if(result){
            props.onDelete(toDeleteId);
        }
    }

    return(
        <div className="container"> 
            <h1>All Field Agents</h1>
            <br />
            <div id="addBtnDiv">
                <Link to="/add">
                    <button className="btn btn-primary" id="addButton">Add New Agent</button>
                </Link>
            </div>
            <hr />
            <div className="row">
                {props.list.map(agent => 
                    <div className="col-3" key={`card${agent.agentId}`}>
                    <div className="card" >
                        <div className="card-header"><img src="AgentsPhoto.png" width="40" height="40"/></div>
                        <div className="card-body">
                            <p className="card-text">{agent.agentId}</p>
                            <div id="expandCardText" className="card-text">
                                <div id="hiddenText">
                                    <p className="card-text">{agent.firstName} {agent.lastName  }</p>
                                    <p className="card-text">
                                        <Link to={`edit/${agent.agentId}`}>
                                        Details/Edit
                                        </Link>
                                    </p>
                                    <button id={`deleteButton${agent.agentId}`} className="btn btn-danger" onClick={handleDelete}>Delete</button>
                                </div>
                            </div>
                        </div> 
                    </div>
                    </div>)}
                
            </div>
        </div>
            
    )
}

export default Home;