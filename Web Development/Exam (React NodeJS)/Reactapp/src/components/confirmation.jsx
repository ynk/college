

import { useHistory } from "react-router-dom";

import { Form, Row, Col,Alert,  Container } from 'react-bootstrap';

const Confirmation = () => {
    let history = useHistory();

    if (!history.location.state) {
        return (
            <div>
                <p></p>

                <div class="container">
                    <div class="alert alert-danger" role="alert">
                        <h4 class="alert-heading">Well done!</h4>
                        <p>You broke it, normally you would have a history location state but where is yours?</p>

                    </div>
                </div>
            </div>
        )
    }
    //Clear Statetuss
   // dispatch(destroy(cart))


    let data = history.location.state[0]
    console.log(data)
    return (

        <Container style={{ marginTop: 10 }}>
         
            <Alert variant="info">
            <b> Order Complete! </b>    
            <Container>
             
                <Form>
                 <Form.Group as={Row} controlId="">
                        <Form.Label column sm="2">
                            Order URL(Debug):
                    </Form.Label>
                        <Col sm="10">
                            <Form.Control plaintext readOnly value={`http://127.0.0.1:3001/order/`+data.user.id} />
                        </Col>
                    </Form.Group> 
                    <Form.Group as={Row} controlId="">
                        <Form.Label column sm="2">
                            Order ID:
                    </Form.Label>
                        <Col sm="10">
                            <Form.Control plaintext readOnly value={data.user.id} />
                        </Col>
                    </Form.Group>
                    <Form.Group as={Row} controlId="">
                        <Form.Label column sm="2">
                            First Name:
                    </Form.Label>
                        <Col sm="10">
                            <Form.Control plaintext readOnly value={data.user.firstName} />
                        </Col>
                    </Form.Group>
                    <Form.Group as={Row} controlId="">
                        <Form.Label column sm="2">
                            Last Name:
                    </Form.Label>
                        <Col sm="10">
                            <Form.Control plaintext readOnly value={data.user.lastName} />
                        </Col>
                    </Form.Group>
                    <Form.Group as={Row} controlId="">
                        <Form.Label column sm="2">
                            Street:
                    </Form.Label>
                        <Col sm="10">
                            <Form.Control plaintext readOnly value={data.user.street} />
                        </Col>
                    </Form.Group>
                    <Form.Group as={Row} controlId="">
                        <Form.Label column sm="2">
                            Street Number:
                    </Form.Label>
                        <Col sm="10">
                            <Form.Control plaintext readOnly value={data.user.number} />
                        </Col>
                    </Form.Group>
                    <Form.Group as={Row} controlId="">
                        <Form.Label column sm="2">
                            Postal Code:
                    </Form.Label>
                        <Col sm="10">
                            <Form.Control plaintext readOnly value={data.user.postalCode} />
                        </Col>
                    </Form.Group>
                    <Form.Group as={Row} controlId="">
                        <Form.Label column sm="2">
                            City:
                    </Form.Label>
                        <Col sm="10">
                            <Form.Control plaintext readOnly value={data.user.city} />
                        </Col>
                    </Form.Group>
                    <Form.Group as={Row} controlId="">
                        <Form.Label column sm="2">
                            Telephone:
                    </Form.Label>
                        <Col sm="10">
                            <Form.Control plaintext readOnly value={data.user.telephone} />
                        </Col>
                    </Form.Group>
                    <Form.Group as={Row} controlId="">
                        <Form.Label column sm="2">
                            Total Price:
                    </Form.Label>
                        <Col sm="10">
                            <Form.Control plaintext readOnly value={`${data.user.totalPrice}$`} />
                        </Col>
                    </Form.Group>
                </Form>
              
                <ul>{data.products.map(r => <li>{r.name} - Quantity: {r.quantity} - Total: {(r.quantity * r.price)}$ </li>)}</ul>
               
            </Container>
            </Alert>
        </Container>
    
    );
};

export default Confirmation;