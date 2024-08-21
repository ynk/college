import React from "react";

const Paragraaf = (props) => {
    const {head,counter} = props
    return(
    <p>{head}:{counter}</p>
    )

};
export default Paragraaf;