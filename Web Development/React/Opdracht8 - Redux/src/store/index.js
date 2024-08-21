// STORE FILE

// NIEUWE MANIER OM TE GEBRUIKEN

import { combineReducers, configureStore } from '@reduxjs/toolkit';
import { counterReducer } from './counter/reducers';
import { loadState, saveState } from './localStorage';
import { reducer as testReducer } from './test/slice';
import { reducer as reposReducer } from './repos/slice';
// import { reducer as testReducer } from './counter/slice';

import { throttle } from 'lodash';


// Aanmaken van een store met een root reducer al in 
// We kunnen ook gebruik maken van meerdere reducers, 
// dan kunnen we gebruik maken van de combineReducers methode
 const rootReducer = combineReducers({
     counter: counterReducer,
     test: testReducer,
     repos: reposReducer
    // ...: ...
 });

 // De state uit de localStorage laden om daarachter aan de store te kunnen meegeven
 const loadedStateFromLocalStorage = loadState();


// Dit gaat onze store configuren met verschillen properties in het 
// configuratie object dat we meegeven als argument
export const store = configureStore({ 
    // De root reducer die we meegeven in onze store, dit kunnen zoals
    // in dit geval meerdere zijn die gecombineerd zijn met combineReducers
    reducer: rootReducer,
    // De preloaded state is de initiële state die we meegeven aan onze store
    // in dit geval is dit de state vanuit de localStorage die we meegeven als initiële state
    preloadedState: loadedStateFromLocalStorage,
});

// LADEN VAN DE STATE UIT DE LOCALSTORAGE
// Mag niet teveel aangeroepen worden en dit doet het wel zonder de throttle methode
// Er is een dure JSON.stringify methode die het werk moet doen dus daarom beter om
// de state in de localStorage maar per seconde te bewaren

// NIET DOEN VIA DEZE MANIER MAAR VIA THROTTLE VANUIT HET LODASH PACKAGE
// store.subscribe(() => saveState(store.getState()));

// De throttle methode helpt ons hierbij en zit in het lodash package, de welke je moest installeren via npm install

store.subscribe(throttle(() => {
saveState(store.getState());
}, 1000) );

// Je kan ook een deel van de state opslaan in de localStorage door middel van het object

// store.subscribe(throttle(() => {
//     saveState(store.getState().counter);
// }, 1000) );
