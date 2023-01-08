import { useState, useMemo, useEffect } from "react";
import { GoogleMap, useLoadScript, Marker} from "@react-google-maps/api";
import axios from 'axios'

export default function Map(props) {
//   const center = useMemo(() => ({ lat: 45.75, lng: 21.23  }), []);

  const [markers, setMarkers] = useState([]);
  const [posts, setPosts] = useState([])

  const { isLoaded } = useLoadScript({
    googleMapsApiKey: "AIzaSyB2xlEHHa5jwKB8mNCewScaqTtjwmLVCI4",
    libraries: ["places"],
  });
  useEffect(()=>{
    // axios.get('https://localhost:7281/issuecontroller')
    axios.get('http://40.89.243.139/issuecontroller')
    .then(res=>{console.log(res)
    setPosts(res.data)})
    .catch((err=>{
        console.log(err)
    }))
  },[])

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
      {posts.map((post)=>(<Marker  key={post.rowKey} position={{lat: post.latitudine, lng: post.longitudine}} />))}
      
      </GoogleMap>
    </>
  );
}