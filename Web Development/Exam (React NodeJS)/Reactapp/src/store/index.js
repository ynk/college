// STORE FILE

// NIEUWE MANIER OM TE GEBRUIKEN

import { combineReducers, configureStore } from '@reduxjs/toolkit';
import { loadState, saveState } from './localstorage';
import { reducer as testReducer } from './control/slice';
import { reducer as reposReducer } from './repo/slice';


import { throttle } from 'lodash';



 const rootReducer = combineReducers({
     cart: testReducer,
     repos: reposReducer
 });

 const loadedStateFromLocalStorage = loadState();


export const store = configureStore({ 
    reducer: rootReducer,
    preloadedState: loadedStateFromLocalStorage,
});



store.subscribe(throttle(() => {
saveState(store.getState());
}, 1000) );

