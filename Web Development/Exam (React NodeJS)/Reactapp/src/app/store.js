import { combineReducers, configureStore } from '@reduxjs/toolkit';
import { loadState, saveState } from './store/localStorage';
import { reducer as testReducer } from './store/control/slice';
import { reducer as reposReducer } from './store/repo/slice';


import { throttle } from 'lodash';



 const rootReducer = combineReducers({
     test: testReducer,
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

