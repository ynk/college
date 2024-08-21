import { createSlice } from '@reduxjs/toolkit';

// DIT IS DE MANIER WAAROP WE STEEDS VANAF NU ONZE REDUCERS GAAN DEFINIEREN EN GEBRUIKEN

// Slices genereren automatisch een action creator, action types en een reducer
const counterSlice = createSlice({
    // De naam van de slice, ook de prefix voor de gegenereerde action types
    // BVB. 'counter.increment' is een action type 
    name: 'counter',
    // De initiële state van deze reducer
    initialState: { value: 0},
    // De reducers die de state zullen wijzigigen
    // OPGELET, hierbij wordt de state niet op een immutable manier aangepast en dit komt
    // omdat achter de schermen dit voor ons geregeld wordt zodat we hier geen rekenening 
    // mee moeten houden.
    // !! ENKEL BIJ createSlice mutable NOOIT op de oude manier !!
    reducers: {
        increment: state => state.value + 1,
        incrementByValue: (state, action) => {
            const {payload} = action;
            return state.value + payload; 
        },
        decrement: state => state.value - 1,
        
        decrementByValue: (state, action) => {
            const {payload} = action;
            return state.value + payload; 
        },
        // MAAK EEN decrementByValue AAN WAARBIJ JE DE WAARDE VAN DE STATE VERMINDERD 
        // MET DE WAARDE UIT DE PAYLOAD
    }
})


export const { actions, reducer } = counterSlice;
export const { increment, decrement, incrementByValue,decrementByValue } = actions;