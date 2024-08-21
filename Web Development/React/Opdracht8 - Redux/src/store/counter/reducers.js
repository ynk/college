// REDUCER FILE

// DE OUDE MANIER OM DIT TE DOEN - WIJ GAAN DIT NIET MEER GEBRUIKEN
// DIT IS OM TE SNAPPEN WAT ER ACHTER DE SCHERMEN GEBEURT

// Importeren van de action types om deze te kunnen gebruiken in onze reducers
import { INCREMENT, INCREMENT_BY_VALUE, DECREMENT, DECREMENT_BY_VALUE } from './actions';


const initialState = {
    value: 0
};


export const counterReducer = (state = initialState, action) => {
    switch (action.type) {
        case INCREMENT:

            return { ...state, value: state.value + 1 };

        case INCREMENT_BY_VALUE:

            return { ...state, value: state.value + action.payload };

        case DECREMENT:
            return { ...state, value: state.value - 1 };


        case DECREMENT_BY_VALUE:

            return { ...state, value: state.value - action.payload };
        default:
            return state;
    }

}