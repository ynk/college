import { useDispatch, useSelector } from 'react-redux'
import { Form, Row, Col, Button, Container, Alert } from 'react-bootstrap';
import { Formik } from "formik"
import { useHistory } from "react-router-dom";
import axios from "axios";
import { destroy } from '../store/control/slice';

const Contactpage = () => {
    const dispatch = useDispatch();
    let history = useHistory();
    const cart = useSelector(state => state.cart);
    let totalPrice = 0.00
    if (cart.length === 0) {
        return (

            <div class="container">
                <p></p>
                <div class="alert alert-danger" role="alert">
                    <p>Orders are empty! <a href="/store">Go back</a></p>
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
             <Container style={{ marginTop: 10 }}>





                <Alert variant="warning">
                    <Alert.Heading>Total Price: {totalPrice}$</Alert.Heading>
                  
                  
             

                    <p>Current Shopping Cart: </p>
                    <ul>{cart.map(r => <li>{r.name} - Quantity: {r.quantity} - Total: {(r.quantity * r.price)}$   </li>)}</ul>
                      <p>  Change order? <a href="/cart">Go to cart</a></p>

               

                </Alert>

                <Formik

                    initialValues={{

                        firstName: '',
                        lastName: '',
                        street: '',
                        number: '',
                        city: '',
                        postalCode: '',
                        telephone: '',
                        email: '',

                    }}

                    onSubmit={(values, actions) => {

                        axios({
                            method: 'post',
                            url: 'http://127.0.0.1:3001/order',
                            data: {
                                user: {
                                    "firstName": values.firstName.trim(),
                                    "lastName": values.lastName.trim(),
                                    "street": values.street.trim(),
                                    "number": values.number.trim(),
                                    "postalCode": values.postalCode.trim(),
                                    "city": values.city.trim(),
                                    "telephone": values.telephone.trim(),
                                    "email": values.email.trim(),
                                    "totalPrice": (totalPrice).toFixed(2),
                                },
                                products: cart

                            }
                        }).then(function (response) {
                            console.log(response)
                            let rec = JSON.parse(response.request.response) // Dit is wat we terug krijgen na onze redirect
                            console.log(rec)
                            // clear state
                            dispatch(destroy())
                             history.push(`/confirmation/${rec.user.id}`, [rec])
                        })
                            .catch(function (error) {
                                //
                                let errors = JSON.parse(error.response.request.response)["error"]
                                console.log(errors)
                                let response = `You have the following error(s):\n`
                                errors.forEach(e => {

                                    response += `${e.msg}\n`
                                })
                                alert(response)
                            });;

                        actions.setSubmitting(false);
                    }}

                >

                    {props =>
                    (
                        <Form onSubmit={props.handleSubmit}>


                            <Form.Group as={Row}>
                                <Form.Label column sm="2">
                                    First Name
                                 </Form.Label>
                                <Col sm="10">
                                    <Form.Control
                                        type="text"
                                        class="form-control"
                                        required
                                        placeholder="Dave"
                                        onChange={props.handleChange}
                                        onBlur={props.handleBlur}
                                        value={props.values.firstName}
                                        name="firstName" placeholder="Dave" />
                                </Col>
                            </Form.Group>

                            <Form.Group as={Row}>
                                <Form.Label column sm="2">
                                    Last Name
                                 </Form.Label>
                                <Col sm="10">
                                    <Form.Control
                                        type="text"
                                        class="form-control"
                                        required
                                        placeholder="Dave"
                                        onChange={props.handleChange}
                                        onBlur={props.handleBlur}
                                        value={props.values.lastName}
                                        name="lastName" placeholder="Stevens" />
                                </Col>
                            </Form.Group>
                            <Form.Group as={Row}>
                                <Form.Label column sm="2">
                                    Street
                                 </Form.Label>
                                <Col sm="10">
                                    <Form.Control
                                        type="text"
                                        class="form-control"
                                        required

                                        onChange={props.handleChange}
                                        onBlur={props.handleBlur}
                                        value={props.values.street}
                                        name="street" placeholder="Summoners Rift" />
                                </Col>
                            </Form.Group>
                            <Form.Group as={Row}>
                                <Form.Label column sm="2">
                                    Street Number
                                 </Form.Label>
                                <Col sm="10">
                                    <Form.Control
                                        type="text"
                                        required
                                        class="form-control"
                                        placeholder="Dave"
                                        onChange={props.handleChange}
                                        onBlur={props.handleBlur}
                                        value={props.values.number}
                                        name="number" placeholder="487" />
                                </Col>
                            </Form.Group>
                            <Form.Group as={Row}>
                                <Form.Label column sm="2">
                                    Postal Code
                                 </Form.Label>
                                <Col sm="10">
                                    <Form.Control
                                        type="text"
                                        required
                                        class="form-control"
                                        placeholder="Dave"
                                        onChange={props.handleChange}
                                        onBlur={props.handleBlur}
                                        value={props.values.postalCode}
                                        name="postalCode" placeholder="1337AQ" />
                                </Col>
                            </Form.Group>
                            <Form.Group as={Row}>
                                <Form.Label column sm="2">
                                    City
                                 </Form.Label>
                                <Col sm="10">
                                    <Form.Control
                                        type="text"
                                        required
                                        class="form-control"
                                        placeholder="Dave"
                                        onChange={props.handleChange}
                                        onBlur={props.handleBlur}
                                        value={props.values.city}
                                        name="city" placeholder="Rift   " />
                                </Col>
                            </Form.Group>
                            <Form.Group as={Row}>
                                <Form.Label column sm="2">
                                    Telephone
                                 </Form.Label>
                                <Col sm="10">
                                    <Form.Control
                                        type="text"
                                        class="form-control"

                                        required
                                        onChange={props.handleChange}
                                        onBlur={props.handleBlur}
                                        value={props.values.telephone}
                                        name="telephone" placeholder="+1234567890" />
                                </Col>
                            </Form.Group>
                            <Form.Group as={Row}>
                                <Form.Label column sm="2">
                                    Email
                                 </Form.Label>
                                <Col sm="10">
                                    <Form.Control
                                        type="email"
                                        class="form-control"
                                        required

                                        onChange={props.handleChange}
                                        onBlur={props.handleBlur}
                                        value={props.values.email}
                                        name="email" placeholder="dave@example.com" />
                                </Col>
                            </Form.Group>

                            <Button variant="outline-info" type="submit">Submit</Button>{' '}


                        </Form>

                    )}

                </Formik>

            </Container>
        </Container>
    );
};

export default Contactpage;