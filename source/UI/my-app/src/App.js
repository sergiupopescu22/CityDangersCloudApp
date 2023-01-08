import './App.css';
import { useState, useMemo, useEffect } from "react";
import Map from './components/Map';
import PlacesAutocomplete from "./components/PlaceFinder";
// import { useState } from "react";
import NewProblemForm from './components/NewProblemForm';
import GetData from './components/GetData';


function App() {

  const [center, setCenter] = useState({ lat: 45.75, lng: 21.23  });
  const [city, setCity] = useState({ lat: null, lng: null  });
  const [selected, setSelected] = useState({ lat: null, lng: null  });
  const [isShown, setIsShown] = useState(false);
  const onClick = (event) =>{
    setIsShown(current => !current);
  };

  const [waited, setWaited] = useState(0);
  useEffect(() => {
    const timer = setTimeout(() => setWaited(1), 1000);
    return () => clearTimeout(timer);
  }, []);

  return (
    <div className="container">
      <div className="formular-container">
        <div>
          <h3 style={{"text-align": "center"}}>City of interest:</h3>
          {(waited == 1 )? <PlacesAutocomplete setSelected={setSelected} setCenter={setCenter} selected={selected}/> : <p>Loading...</p>} 
          {/* <PlacesAutocomplete setSelected={setSelected} setCenter={setCenter} selected={selected}/> */}
          
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
        <Map center={center} setCenter={setCenter} setSelected={setSelected}/>
      </div> 
      
    </div>
  );
}

export default App;
