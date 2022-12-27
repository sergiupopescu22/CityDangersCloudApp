import './App.css';
import { useState, useMemo } from "react";
import Map from './components/Map';
import PlacesAutocomplete from "./components/PlaceFinder";
import NewProblemForm from './components/NewProblemForm';

function App() {

  const [center, setCenter] = useState({ lat: 45.75, lng: 21.23  });
  const [selected, setSelected] = useState({ lat: null, lng: null  });
  const [isShown, setIsShown] = useState(false);
  const onClick = (event) =>{
    setIsShown(current => !current);
  }

  return (
    <div className="container">
      <div className="formular-container">
        <div>
          <h3 style={{"text-align": "center"}}>City of interest:</h3>
          <PlacesAutocomplete setSelected={setSelected} setCenter={setCenter}/>
        </div>
      
        {!isShown && (
        <div class="button-div">
          <button class="button-24" onClick={onClick}>Report a new issue</button>
        </div>
        )}

        {isShown && (
        <div class="report-informations">
          <NewProblemForm  location={selected}/>
          <button class="button-24" onClick={onClick}>Abort report</button>
        </div>
        )}
      </div>
      
      <div className="map-container">
        <Map center={center} setSelected={setSelected}/>
      </div> 
    </div>
  );
}

export default App;
