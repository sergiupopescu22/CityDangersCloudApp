import { useState, useMemo } from "react";
import { GoogleMap, useLoadScript, Marker} from "@react-google-maps/api";
import { Wrapper, Status } from "@googlemaps/react-wrapper";


export default function Map(props) {
//   const center = useMemo(() => ({ lat: 45.75, lng: 21.23  }), []);

  const [markers, setMarkers] = useState([]);

  const { isLoaded } = useLoadScript({
    googleMapsApiKey: "AIzaSyB2xlEHHa5jwKB8mNCewScaqTtjwmLVCI4",
    libraries: ["places"],
  });

  // props.setSelected(props.center);


  if (!isLoaded) return <div>Loading...</div>;

  return (
    <>
      <GoogleMap
      zoom={12}
      center = {props.center}
      mapContainerClassName="gmaps-container"
      onClick={(event)=>{
        setMarkers((current)=>[
          {
            lat:event.latLng.lat(),
            lng:event.latLng.lng(),
            time: new Date(),
          },
        ]);
        props.setSelected({lat:event.latLng.lat(), lng:event.latLng.lng()});
      }}
      >
      {markers.map((marker) => (<Marker  position={{lat: marker.lat, lng: marker.lng}} />)) }
      
      </GoogleMap>
    </>
  );
}