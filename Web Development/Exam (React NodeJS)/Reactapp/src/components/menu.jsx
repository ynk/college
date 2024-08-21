
import { Nav, Navbar } from 'react-bootstrap';

const Menu = () => {

    return (
        <div>
            <Navbar bg="light" expand="lg">
                <Navbar.Brand href="/">Item Store</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="mr-auto">
                        <Nav.Link href="/store">Store</Nav.Link>
                        <Nav.Link href="/cart">Cart</Nav.Link>
                        <Nav.Link href="/order">Order</Nav.Link>
                        <Nav.Link href="http://127.0.0.1:3001/products">View API Products</Nav.Link>
                        <Nav.Link href="http://127.0.0.1:3001/orders">View API Orders</Nav.Link>
                    </Nav>
                </Navbar.Collapse>
            </Navbar>
        </div>
    );
};

export default Menu;