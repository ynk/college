// ACTION FILE

// DE OUDE MANIER OM DIT TE DOEN - WIJ GAAN DIT NIET MEER GEBRUIKEN
// DIT IS OM TE SNAPPEN WAT ER ACHTER DE SCHERMEN GEBEURT

// Definiëren van onze action types:
// - Het verhogen van de state
// - Het verhogen van de state met een value
// - Het verlagen van de state
// - Het verlagen van de state met een value
export const INCREMENT = 'INCREMENT';
export const INCREMENT_BY_VALUE = 'INCREMENT_BY_VALUE';
export const DECREMENT = 'DECREMENT';
export const DELETE_VALUE = "DELETE_VALUE"

// SCHRIJF HIER DE ONTBREKENDE ACTION TYPE
export const DECREMENT_BY_VALUE = 'DECREMENT_BY_VALUE';
// 1ste Action creator -> Zorgt voor de increment van onze state.
// We geven geen payload mee omdat we in onze reducer de state gaan verhogen met 1
export const increment = () => {
    return { type: INCREMENT }
}


// 2de Action creator -> Zorgt voor de increment van onze state met een meegegeven value,
// We geven hierbij dus wel een waarde mee die in onze reducer de state zal veranderen
export const incrementByValue = (value) => {
    return { type: INCREMENT_BY_VALUE, payload: value }
}

// 3de Action creator -> Zorgt voor de decrement van onze state.
// We geven geen payload mee omdat we in onze reducer de state gaan verlagen met 1
export const decrement = () => {
    return { type: DECREMENT }
}

// 4de Action creator -> Zorgt voor de decrement van onze state met een meegegeven value,
// We geven hierbij dus een waarde mee die in onze reducer de state zal veranderen

// SCHRIJF DEZE ACTION ZELF ALS OEFENING
export const decrementByValue = (value) => {
    return { type: DECREMENT_BY_VALUE, payload: value }
}
