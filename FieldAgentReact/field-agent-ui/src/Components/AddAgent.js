import { Link, Redirect } from "react-router-dom";
import {useState} from 'react';

function AddAgent(props){

    const [newAgent, setNewAgent] = useState({firstName:"",lastName:"",dateOfBirth:"",height:0});

    const handleSubmit = function(evt){
        evt.preventDefault();
        newAgent.firstName = evt.target[0].value;
        newAgent.lastName =evt.target[1].value;
        newAgent.dateOfBirth = evt.target[2].value +"T00:00:00";
        newAgent.height = evt.target[3].value;
        setNewAgent(newAgent);
        props.onNewAgent(newAgent);
        setNewAgent({});
        console.log("addition successful");
        return <Redirect to="/" />
    }
    return (
        <div className="container">
            <h2>Add An Agent</h2>
            <br></br>
            <div className="row">

            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label for="agentFirstName">First Name</label>
                    <input type="text" className="form-control" id="agentFirstName" name="agentFirstName" required maxLength="50"></input>
                </div>
                <div className="form-group">
                    <label for="agentLastName">Last Name</label>
                    <input type="text" className="form-control" id="agentLastName" name="agentLastName" required maxLength="50"></input>
                </div>
                <div className="form-group">
                    <label for="birthDate">Date of Birth</label>
                    <input type="date" className="form-control" id="birthDate" name="birthDate" required></input>
                </div>
                <div className="form-group">
                    <label for="height">Height (cm)</label>
                    <input type="number" className="form-control" id="height" name="height" required></input>
                </div>
                    <button className="btn btn-primary" type="submit" id="addNewAgent">Add Agent</button>
            </form>

            </div>
            
        </div>
        
    )
}

export default AddAgent;