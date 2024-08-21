import React from 'react';

const List = (props) => {
    // id value
    return (

        <ul>
            {
            props.technologies.map((id,value) => <li key={id}>{value}</li>)
            }
        </ul>
    )
}

export default List;