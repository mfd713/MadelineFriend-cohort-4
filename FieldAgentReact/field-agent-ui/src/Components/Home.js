import { Link } from "react-router-dom";
import {useState, useEffect} from 'react';

function Home() {

    const[agents, setAgents] = useState([]);

        useEffect(() =>{
            fetch("https://localhost:44383/api/agents")
            .then(response =>{
              if(response.status !== 200){
                return Promise.reject("failed fetch")
              }
              return response.json();
            })
            .then(json => setAgents(json))
            .catch(console.log);
        }, []);

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
                {agents.map(agent => 
                    <div className="col-3">
                    <div className="card">
                        <div className="card-header"><img src="AgentsPhoto.png" width="40" height="40"/></div>
                        <div className="card-body">
                            <p className="card-text">{agent.agentId}</p>
                            <div id="expandCardText" className="card-text">
                                <div id="hiddenText">
                                <p className="card-text">{agent.firstName} {agent.lastName  }</p>

                                    <a href="">Details/Edit</a>
                                    <br />
                                    <button id="deleteButton" className="btn btn-danger">Delete</button>
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