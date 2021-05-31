import { Link, Redirect,useParams } from "react-router-dom";
import {useState, useEffect} from 'react';

function Edit(props) {

    let {toEditId} = useParams();
    const [toEdit, setToEdit] = useState(props.list.filter(agent => agent.agentId == toEditId)[0]);

    const handleSubmit = (evt) => {
        evt.preventDefault();
        console.log(toEdit);
        toEdit.firstName = evt.target[0].value;
        toEdit.lastName =evt.target[1].value;
       if(!(evt.target[2].value ==="")){
            toEdit.dateOfBirth = evt.target[2].value +"T00:00:00"
      }
        toEdit.height = parseFloat(evt.target[3].value);
        setToEdit(toEdit);
        props.onEdit(toEdit);
    }

    return(
        <div className="container">
            <h2>Edit An Agent</h2>
            <br></br>
            <div className="row">
            <div className="col-6">
                <img src="AgentsPhoto.png" width="80%"></img>
            </div>
            <div className="col-6">
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label for="agentFirstName">First Name</label>
                    <input type="text" className="form-control" id="agentFirstName" name="agentFirstName" defaultValue={toEdit.firstName}></input>
                </div>
                <div className="form-group">
                    <label for="agentLastName">Last Name</label>
                    <input type="text" className="form-control" id="agentLastName" name="agentLastName" defaultValue={toEdit.lastName} ></input>
                </div>
                <div className="form-group">
                    <label for="birthDate">Date of Birth (currently {toEdit.dateOfBirth.substring(0,10)})</label>
                    <input type="date" className="form-control" id="birthDate" name="birthDate"></input>
                </div>
                <div className="form-group">
                    <label for="height">Height (cm)</label>
                    <input type="number" className="form-control" id="height" name="height" defaultValue={toEdit.height}></input>
                </div>
                    <button className="btn btn-primary" type="submit" id="saveChanges">Save Changes</button>
            </form>
            </div>
            </div>
            
        </div>
    )
}

export default Edit;