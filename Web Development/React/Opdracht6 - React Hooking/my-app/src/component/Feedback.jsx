import Paragraaf from "./Paragraaf"

const { useState, useEffect } = require("react")

const Feedback = (props) => {
    const { button, head } = props


    const [counter, setCounter] = useState(0)

    useEffect( () => {
        console.log(counter);
    },[counter])

    const click = (event) => {
        setCounter(counter+ 1)
    }

    return (
        <div>
            <button onClick={click}>{head}</button>
            <Paragraaf head={head} counter={counter} ></Paragraaf>
        </div>
    );
};

export default Feedback;