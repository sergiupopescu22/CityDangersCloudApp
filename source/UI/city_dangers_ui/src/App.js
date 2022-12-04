// import { Map, GoogleApiWrapper } from 'google-maps-react';
// import { MapContainer } from './components/Map';
// import Map from './components/Map';

import { GoogleMap, useLoadScript} from "@react-google-maps/api";



function App() {

  const { isLoaded } = useLoadScript({
    googleMapsApiKey: "AIzaSyDZGjpMJAwwhKrMcage3StKjWARlN9R9ZY",
  });

  if (!isLoaded) 
    return <div>Loading...</div>;

  return (

  <div className='container'>
    <h1>SA MOARA REACTUL CIPRIANE CA IMI PUSCA CAPUL</h1>
    <div>
      <GoogleMap 
      zoom={10} 
      center={{lat:45.757086, lng:21.228389}} 
      mapContainerClassName="map-container">

      </GoogleMap>
    </div>
  </div>
  );

}

export default App;
