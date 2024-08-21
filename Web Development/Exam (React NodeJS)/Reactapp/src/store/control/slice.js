/* eslint-disable */
const { createSlice } = require("@reduxjs/toolkit");

const testSlice = createSlice({
    name: 'itemStore',
    initialState: [],
    reducers: {

        addOne: (state, action) => {
            // check if element in array zit, if true increase quantity else add to arary
            let inArray = false
            state.forEach(element => {
                if (element.id === action.payload.id) {
                    element.quantity++
                    inArray = true;   
                }
            });
            if (!inArray) {
                //Append quantity
                let temp = {...action.payload, quantity:1}
                state.push(temp)

            }
        },

        DeleteValue: (state, action) => {
            for(let i = state.length - 1; i>=0; i--){
                if(state[i].id === action.payload.id)
                {
                    state.splice(i,1)
                }
            }

       
        },
        increaseQuantity: (state, action) => {
            state.forEach(product => {
                if(product.id == action.payload.id){
                    if(product.quantity <=10){
                        product.quantity++;
                    }
                }
            })
        },

        decreaseQuantity: (state, action) => {
            state.forEach(product => {
                if(product.id == action.payload.id){
                    if(product.quantity > 1){
                        product.quantity--;
                    }
                }
            })
        },

        destroy: (state,action) => {
            while (state.length !==0){
                state.pop()
            }
        },
    }
});

export const { actions, reducer } = testSlice;
export const { addOne, DeleteValue, removeOne,increaseQuantity,decreaseQuantity,destroy } = actions;