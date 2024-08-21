import React from "react"
import { useParams } from "react-router-dom";


const Api = (props) =>{
    const id = useParams().id
    console.log(id)
    console.log(props.data)
    return (
        <div>
        <b>{props.data[id].API}</b>
        <p>{props.data[id].Description}</p>
        <a href={props.data[id].Link}>{props.data[id].Link}</a>
      </div>
    );
}; 

export default Api;