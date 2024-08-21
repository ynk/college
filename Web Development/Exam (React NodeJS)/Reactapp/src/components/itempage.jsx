
import React, { useEffect } from 'react'

import { useDispatch, useSelector } from 'react-redux'
import { addOne, DeleteValue } from '../store/control/slice';
import { Card, Button, Container, Row, Col,Alert } from 'react-bootstrap';

import { fetchRepos } from '../store/repo/slice';

const Itempage = () => {

    const dispatch = useDispatch();
    const reposState = useSelector(state => state.repos);

    const cart = useSelector(state => state.cart);

    useEffect(() => {
        dispatch(fetchRepos()) // on page load

    }, [])

    let totalPrice = 0;

    if (!cart.length == 0) {
        cart.forEach(r => {
            totalPrice += (r.quantity * r.price)
        });
    }


    const { repos } = reposState;

    return (

        <Container>
        <Container style={{ marginTop: 10 }}>
        <Alert variant="warning">
      
        <p>Current Shopping Cart: </p>
        <ul>{cart.map(r => <li>{r.name} - Quantity: {r.quantity} - Total: {(r.quantity * r.price)}$    <Button onClick={() => dispatch(DeleteValue(r))} size="sm" variant="outline-danger">Delete</Button> </li>)}</ul>
        <p>Total cart: <b>{totalPrice}</b>$ </p>
        <Button href="/cart" variant="outline-info ">View cart detail</Button> 
        </Alert>
         </Container>

            
                <Container>
                    <Row>
                        {repos.map(r => (
                            <Col  xs="4">
                                <Card style={{ width: '18rem' }}>
                                    <Card.Body>
                                        <Card.Title>{r.name}</Card.Title>

                                        <Card.Subtitle className="mb-2 text-muted">{r.description}</Card.Subtitle>
                                        <Card.Text>
                                            {r.price}$
                                        </Card.Text>
                                        <Button onClick={() => dispatch(addOne(r))} variant="outline-success">Add</Button>
                                    </Card.Body>
                                </Card>
                            </Col>
                        ))}
                    </Row>
                </Container>
                </Container>
           
 
    )
};

export default Itempage;