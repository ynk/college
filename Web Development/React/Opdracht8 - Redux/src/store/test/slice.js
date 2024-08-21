/* eslint-disable */
const { createSlice } = require("@reduxjs/toolkit");

const testSlice = createSlice({
    name: 'test',
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
                let el = {};
                el["id"] = action.payload.id
                el["name"] = action.payload.name
                el["price"] = action.payload.price
                el["description"] = action.payload.description
                el["quantity"] = 1
                state.push(el)

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



        removeOne: (state, action) => {
            state.pop(action.payload)
        },
    }
});

export const { actions, reducer } = testSlice;
export const { addOne, DeleteValue, removeOne,increaseQuantity,decreaseQuantity } = actions;