import { createAsyncThunk, createSlice } from "@reduxjs/toolkit"
import axios from "axios";

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

    extraReducers: {
        [fetchRepos.pending]: (state, action) => {
            state.status = 'loading'
            state.repos = [];
        },
        [fetchRepos.fulfilled]: (state, action) => {
            state.status = 'succeeded';
            state.repos = action.payload.data ;
        },
        [fetchRepos.rejected]: (state, action) => {
            state.status = 'failed';
            state.repos = [];
        }
    }
});

export const { actions, reducer } = reposSlice;