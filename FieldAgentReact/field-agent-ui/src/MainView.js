import {useState, useEffect} from 'react';

    function MainView() {
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
                <h2>All Field Agents</h2>
                <div className="row">
                    {agents.map(agent => 
                        <div className="card mb-3">
                            <div className="card-body">
                                <p className="card-text">{agent.firstName} {agent.lastName}</p>
                            </div>
                        </div>)}
                    
                </div>
            </div>
                
        )
        }

        export default MainView;

