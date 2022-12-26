import './App.css';
import { useState, useMemo } from "react";
import Map from './components/Map';
import PlacesAutocomplete from "./components/PlaceFinder";
import NewProblemForm from './components/NewProblemForm';

function App() {

  const [center, setCenter] = useState({ lat: 45.75, lng: 21.23  });
  const [selected, setSelected] = useState(null);

  return (
    <div className="container">
      <div className="formular-container">
        <h3>Add the city you are intereseted in:</h3>
        <PlacesAutocomplete setSelected={setSelected} setCenter={setCenter}/>
        {/* <h3>Do you want to report a problem?</h3>
        <button>Yes</button> */}
        <NewProblemForm location={selected}/>
      </div>
      <div className="map-container">
        <Map center={center}/>
      </div> 
    </div>
  );
}

export default App;
