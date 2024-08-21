import React from 'react'

import { connect } from 'react-redux';
import HookComponent from './HookComponent';

// OUDE MANIER OM STATE OP TE VRAGEN OF ACTIONS MET DISPATCH OP TE ROEPEN
// WE GAAN GEBRUIK MAKEN VAN DE HOOKS OM DIT NIET TE HOEVEN DOEN

const ShowValue = (props) => {

    // STATE OPVRAGEN VIA SUBSCRIBE - NIET DE GOEDE MANIER
    // Lokale state maken om de state op te vangen vanuit de store
    // const [showValue, setShowValue] = useState(0);

    // Met een useEffect gaan we subscriben naar de store om de state te kunnen terugkrijgen en de updates
    // natuurlijk ook. Als we dit zonder useEffect zouden doen dan krijgen we maar een keer de state terug
    // maar niet als de state wijzigt. Deze useEffect maakt gebruik van een cleanup dit omdat we subscriben naar iets
    // useEffect(() => {
    //     const subscribe = store.subscribe(() => 
    //         setShowValue(store.getState().counter.value)
    //     )
    //     return () => subscribe
    // }, [])

    // Met de connect methode en de mapStateToProps konden we de state opvragen en meegeven aan onze JSX
    const {counterState} = props;

    return (
        <div style={{borderStyle: "solid", borderWidth: 4, padding: 10, margin: 10}}>
            <h2>Show value</h2>
            <p style={{fontWeight: 'bolder', fontSize: 24}}>{counterState.value}</p>
            <HookComponent />
        </div>
    )
}


const mapStateToProps = state => ({
    counterState: state.counter
})

const mapDispatchToProps = dispatch   =>({
 
})

export default connect(mapStateToProps, mapDispatchToProps)(ShowValue);
