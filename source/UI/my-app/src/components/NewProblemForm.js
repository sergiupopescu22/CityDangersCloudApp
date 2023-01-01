import { useState, useEffect } from "react";
import axios from 'axios'

export default function NewProblemForm({location}){

    const [latitudine, setLatitudine] = useState(0);
    const [longitudine, setLongitudine] = useState(0);
    const [descriere, setDescriere] = useState('')

    const handleSubmit = async (e) =>{
        e.preventDefault();
        console.log({
        latitudine: location.lat, 
        longitudine: location.lng,
        issue: descriere,
        partitionKey: "5000000234456",
        rowKey: Date().toLocaleString(),
        });
        try{
            const resp = await axios.post('https://localhost:7281/issuecontroller', 
            {latitudine: location.lat, 
            longitudine: location.lng,
        issue: descriere,
        partitionKey: "5000000234456",
        rowKey: Date().toLocaleString(),
        })
        }catch(error){
            console.log(error.response);
        }
        window.location.reload(true)
    };

    return (
    <>
        <h3>Report informations</h3>
        <form>
            <label>Pick a location using the map</label>
            <br/>
            <br/>
            <label>
                Selected Latitude:
                <input 
                type="text" 
                value={location.lat}/>
            </label>
            <br/>
            <label>
                Selected Longitude:
                <input 
                type="text" 
                name="name" 
                value={location.lng}/>
            </label>
            <br/>
            <br/>
            <label>
                Description:
                <input 
                type="text" 
                onChange={(e)=>setDescriere(e.target.value)}/>
            </label>
            <br/>
            <button class="button-24-submit" onClick={handleSubmit}>Submit Issue</button>
        </form>
    </>);
};