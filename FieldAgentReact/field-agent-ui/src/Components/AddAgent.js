import { Link } from "react-router-dom";
import {useState} from 'react';

function AddAgent(){
    return (
        <div className="container">
            <h2>Add An Agent</h2>
            <br></br>
            <div className="row">
            <div className="col-6">
                <img src="AgentsPhoto.png" width="80%"></img>
            </div>
            <div className="col-6">
            <form>
                <div className="form-group">
                    <label for="agentFirstName">First Name</label>
                    <input type="text" className="form-control" id="agentFirstName" name="agentFirstName"></input>
                </div>
                <div className="form-group">
                    <label for="agentLastName">Last Name</label>
                    <input type="text" className="form-control" id="agentLastName" name="agentLastName"></input>
                </div>
                <div className="form-group">
                    <label for="birthDate">Date of Birth</label>
                    <input type="date" className="form-control" id="birthDate" name="birthDate"></input>
                </div>
                <div className="form-group">
                    <label for="height">Height (cm)</label>
                    <input type="number" className="form-control" id="height" name="height"></input>
                </div>
            </form>
            </div>
            </div>
            
        </div>
        
    )
}

export default AddAgent;