import {useState, useEffect} from 'react';
import {Switch, Route} from 'react-router-dom';
import AddAgent from './AddAgent';
import Home from './Home';
import EditAgent from './EditAgent';

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
        }, [agents]);

        const bearerToken = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE2MjI1MDM0NjcsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6MjAwMCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6MjAwMCJ9.nhvp31cQNb7RKDiYdo0NEr9eKk9j8ErJbZUl3eWT8Hc"
        const handleNewAgent = (agent) => {
            const init = {
                method: "POST",
                headers: {
                    "Content-Type":"application/json",
                    "Accept":"application/json",
                    "Authorization": bearerToken
                },
                body: JSON.stringify(agent)
            }

            fetch("https://localhost:44383/api/agents", init)
                .then(response =>{
                    if(response.status !== 201){
                        return Promise.reject(`Rejected with code ${response.status}`);
                    }
                    return response.json();
                })
                .then(json => setAgents([...agents,json]))
                .catch(console.log);
        }

        const handleEdit = (agent) =>{
            const init = {
                method: "Put",
                headers: {
                    "Content-Type":"application/json",
                    "Accept":"application/json",
                    "Authorization": bearerToken
                },
                body: JSON.stringify(agent)
            }

            fetch(`https://localhost:44383/api/agents/${agent.agentId}`, init)
                .then(response =>{
                    if(response.status !== 200) {
                        return Promise.reject(`Rejected with code ${response.status}`);
                    }
                    return response.json();
                })
                .then(json => setAgents([...agents,json]))
                .catch(console.log);
        }

        const handleDelete = (toDeleteId) =>{
            const init = {
                method: "DELETE",
                headers: {
                    "Authorization": bearerToken
                }
            }

            fetch(`https://localhost:44383/api/agents/${toDeleteId}`, init)
                .then(response =>{
                    if(response.status === 200) {
                        setAgents(agents.filter(agent => agent.agentId !== toDeleteId));
                    }else if(response.status === 404){
                        return Promise.reject("Agent not found");
                    }else {
                        return Promise.reject(`Delete failed with status: ${response.status}`);
                    }
                })
                .catch(console.log);
        }

        return(
        <Switch> {/* The Switch decides which component to show based on the current URL.*/}
            <Route exact path='/' render={(props) => (
                <Home {...props} list={agents} onDelete={handleDelete}/>
            )}></Route>
            <Route exact path='/add' render={(props) => (
                <AddAgent {...props} onNewAgent ={handleNewAgent}/>
            )}></Route>
            <Route path='/edit/:toEditId' render={(props) =>(
                <EditAgent {...props} list={agents} onEdit={handleEdit}/>
            )}></Route>
        </Switch>
        )
        }

        export default MainView;

