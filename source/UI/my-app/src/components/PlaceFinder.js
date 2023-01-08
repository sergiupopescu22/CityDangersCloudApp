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
import { useState, useEffect } from "react";

export default function PlacesAutocomplete({ setSelected, setCenter, selected })  {

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
      // setSelected({lat,lng});  
      setCenter({lat,lng});
    };
  
    return (
      <div>

      
      <Combobox onSelect={handleSelect}>
       <ComboboxInput 
            value={value} 
            onChange={handleInput}
            disabled={!ready} 
            className="combobox-input"
            placeholder="Timisoara"

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
      </div>
    );
  };