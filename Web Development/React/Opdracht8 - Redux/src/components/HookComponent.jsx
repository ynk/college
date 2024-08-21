import React from 'react'
import { useDispatch, useSelector } from 'react-redux';
import {  DeleteValue,increaseQuantity,decreaseQuantity } from '../store/test/slice';
import { Button, Card } from 'react-bootstrap';

// Dispatchen van actions en het terugkrijgen van de state door gebruik te maken van de hooks 
const HookComponent = () => {
    const dispatch = useDispatch();
    const counterState = useSelector(state => state.counter);
    const testState = useSelector(state => state.test);

    let totalPrice = 0;


    testState.forEach(r => {
        totalPrice +=(r.quantity * r.price)
    });



    return (
        <div>
            <h2>Using Hooks</h2>
            <p>{counterState.value}</p>


            <p>Cart</p>
            <p>
                {
                    testState.map((r, index) =>
                        <Card style={{ width: '18rem' }}>
                            <Card.Body>
                                <Card.Title>{r.name}</Card.Title>
                                
                                <Card.Subtitle className="mb-2 text-muted">{r.description}</Card.Subtitle>
                                <Card.Text>per item: <b>{r.price}$</b> | total({r.quantity}) = <b>{r.price*r.quantity}$</b></Card.Text>
                                <Button onClick={() => dispatch(increaseQuantity(r))} variant="outline-info">+1</Button>
                                <Button onClick={() => dispatch(decreaseQuantity(r))} variant="outline-danger">-1</Button>
                                <Button onClick={() => dispatch(DeleteValue(r))} variant="outline-danger">Delete</Button>
                              
                            </Card.Body>
                        </Card>
                    )
                }
            </p>
            <b>Total Price: {totalPrice}</b>
        </div>
    )
}

export default HookComponent
