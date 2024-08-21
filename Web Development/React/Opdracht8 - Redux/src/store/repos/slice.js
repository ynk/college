import { createAsyncThunk, createSlice } from "@reduxjs/toolkit"
import axios from "axios";

// Asynchrone code die uitgevoerd wordt moet in een ander type van reducer
// Dit omdat de standaard reducers enkel maar synchrone code uitvoeren en dus 
// niet om kunnen met asyncrhone code
// Daarom gebruiken we createAsynchThunk, waarbij we een naam maken voor de action type ('repos/fetchRepos')
// en daarna dan asynchrone code mee te geven
// Dit maakt gebruik van de async await syntax

export const fetchRepos = createAsyncThunk('repos/fetchRepos', async () => {
    const response = await axios({
        method: 'GET',
        url: 'http://127.0.0.1:3001/products'
    })


    return response;
})

const reposSlice = createSlice({
    name: 'repos',
    initialState: {
        repos: [],
        status: 'idle',
        error: null
    },
    // Hier moeten we dan wel nog de extraReducers definieren waar we 
    // de acties gaan opvangen en de state dus wijzigen
    // een asyncThunkReducer heeft drie toestanden namelijk: 
    // pending, fulfilled en rejected die we dan kunnen opvangen zoals hieronder
    extraReducers: {
        [fetchRepos.pending]: (state, action) => {
            state.status = 'loading'
            state.repos = [];
        },
        [fetchRepos.fulfilled]: (state, action) => {
            state.status = 'succeeded';

            let data = []
            action.payload.data.forEach(element => {
                let x = element
                data.push(x)
            }
                
                );
            
            state.repos = data ;
        },
        [fetchRepos.rejected]: (state, action) => {
            state.status = 'failed';
            state.repos = [];
        }
    }
});

export const { actions, reducer } = reposSlice;