import React from "react"

const Apis = (props) =>{
    return (
        <ul>
            {
            props.data.map((obj,id) => <li key={id}><a href={"api/"+id}>{obj.API}</a></li>)
            }
        </ul>
    );
}; 

export default Apis;