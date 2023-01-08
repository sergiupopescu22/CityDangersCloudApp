import React, {useState, useEffect} from 'react'
import axios from 'axios'

export default function GetData(){

    const [posts, setPosts] = useState([])

    useEffect(()=>{
        // axios.get('https://localhost:7281/issuecontroller')
        axios.get('http://40.89.243.139/issuecontroller')
        .then(res=>{console.log(res)
        setPosts(res.data)})
        .catch((err=>{
            console.log(err)
        }))
    },[])

    return(<div>
          <ul>
            {
                posts.map(post=>(<li key={post.partitionKey}>{post.issue}</li>))
            }
          </ul>
    </div>)
}