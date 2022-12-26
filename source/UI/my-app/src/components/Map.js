import { useState, useMemo } from "react";
import { GoogleMap, useLoadScript, Marker} from "@react-google-maps/api";
import usePlacesAutocomplete, {
    getGeocode,
    getLatLng,
} from "use-places-autocomplete";
import {
  Combobox,
  ComboboxInput,
  ComboboxPopover,
  ComboboxList,
  ComboboxOption,
} from "@reach/combobox";
import "@reach/combobox/styles.css";
import { Wrapper, Status } from "@googlemaps/react-wrapper";

export default function Map() {
//   const center = useMemo(() => ({ lat: 45.75, lng: 21.23  }), []);
  const [center, setCenter] = useState({ lat: 45.75, lng: 21.23  });
  const [selected, setSelected] = useState(null);
  const [markers, setMarkers] = useState([]);

  const { isLoaded } = useLoadScript({
    googleMapsApiKey: "AIzaSyB2xlEHHa5jwKB8mNCewScaqTtjwmLVCI4",
    libraries: ["places"],
  });


  if (!isLoaded) return <div>Loading...</div>;

  return (
    <>
        <PlacesAutocomplete setSelected={setSelected} setCenter={setCenter}/>
        <GoogleMap
        zoom={12}
        center={center}
        mapContainerClassName="map-container"
        onClick={(event)=>{
          setMarkers((current)=>[
            {
              lat:event.latLng.lat(),
              lng:event.latLng.lng(),
              time: new Date(),
            },
          ]);
        }}
        >
        {/* {selected && <Marker position={selected} />} */}
        {markers.map((marker) => (<Marker  position={{lat: marker.lat, lng: marker.lng}}/>))}
        </GoogleMap>
    </>
  );
}

function PlacesAutocomplete({ setSelected, setCenter })  {
    const {
      ready,
      value,
      setValue,
      suggestions: { status, data },
      clearSuggestions,
    } = usePlacesAutocomplete();
  
    const handleInput = (e) => {
      setValue(e.target.value);
    };
  
    const handleSelect = async (address) => {
      setValue(address, false);
      clearSuggestions();
      const results = await getGeocode({address});
      const {lat, lng} = await getLatLng(results[0]);
      setSelected({lat,lng});
      setCenter({lat,lng});
    };
  
    return (
      <Combobox onSelect={handleSelect}>
        <ComboboxInput 
            value={value} 
            onChange={handleInput} 
            disabled={!ready} 
            className="combobox-input"
            placeholder="Select a city"
        />
        <ComboboxPopover>
          <ComboboxList>
            {status === "OK" &&
              data.map(({ place_id, description }) => (
                <ComboboxOption key={place_id} value={description} />
              ))}
          </ComboboxList>
        </ComboboxPopover>
      </Combobox>
    );
  };