

import  { React} from 'react'

import { useDispatch, useSelector } from 'react-redux'


import {  DeleteValue,  increaseQuantity, decreaseQuantity } from '../store/control/slice';
import { Card, Button, Container, Row, Col, Alert } from 'react-bootstrap';


const Orderpage = () => {


    const dispatch = useDispatch();
    const cart = useSelector(state => state.cart);

    let totalPrice = 0;


    if (cart.length == 0) {
        return (

            <div class="container">
                <p></p>
                <div class="alert alert-danger" role="alert">
                    <p>Orders are empty  <a href="/store">Go back</a></p>
                </div>
            </div>
        )
    } else {
        cart.forEach(r => {
            totalPrice += (r.quantity * r.price)
        });
    }

    return (
        <Container>
   
            <Row>
                {cart.map((r,index) => (
                    <Col xs="4">
                        <Card style={{ width: '18rem' }}>
                            <Card.Body>
                                <Card.Title>{r.name}</Card.Title>

                                <Card.Subtitle className="mb-2 text-muted">{r.description}</Card.Subtitle>
                                <Card.Text>
                                    {r.price}$
                    </Card.Text>
                                <Card.Title>{r.name}</Card.Title>

                                <Card.Text>per item: <b>{r.price}$</b> | ({r.quantity}) = <b>{r.price * r.quantity}$</b></Card.Text>
                                <Button onClick={() => dispatch(increaseQuantity(r))} variant="outline-success">+1</Button>
                                <Button onClick={() => dispatch(decreaseQuantity(r))} variant="outline-warning">-1</Button>
                                <Button onClick={() => dispatch(DeleteValue(r))} variant="outline-danger">Delete</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                ))}
            </Row>
            <Alert variant="warning">
                <p>Total Price: {totalPrice}$ -  Forgot something? <a href="/store">Go back</a></p>
            </Alert>


            <Alert variant="success">
                <p>Ready? Let's <a href="/order">Order</a>!</p>
            </Alert>
            
        </Container>

         
    );
};

export default Orderpage;