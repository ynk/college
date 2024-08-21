import React from 'react';
import { store } from '../store';
import { incrementByValue,decrementByValue } from '../store/counter/actions';
import ShowValue from './ShowValue';
import {  Button, } from 'react-bootstrap';

// In deze Child component wordt de waarde in de state verhoogd met een gegeven value nl. 5
// Dit wordt opnieuw gedaan door middel van een action incrementByValue te dispatchen naar de store

// MAAK ONDER DE BUTTON MET INCREMENTBYVALUE EEN NIEUWE BUTTON AAN MET DECREMENTBYVALUE 
// DISPATCH DAN OOK DE AANGEMAAKTE DECREMENTBYVALUE ACTION MET ALS WAARDE 5

const Child = () => {

    return (
        <div style={{borderStyle: "solid", borderWidth: 4, padding: 10, margin: 10}}>
            <h2>Child</h2>
            <Button onClick={() => store.dispatch(incrementByValue(5))} variant="outline-info">Increment with five</Button>
            <Button onClick={() => store.dispatch(decrementByValue(5))} variant="outline-info">Decrement with five</Button>
             <ShowValue />
        </div>
    )
}

export default Child
