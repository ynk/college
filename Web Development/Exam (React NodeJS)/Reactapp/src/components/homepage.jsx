
import { Container, Alert } from "react-bootstrap"


const Homepage = () => {
    return (
           
        <Container style={{ marginTop: 10 }}>
            <Alert variant="success">
                <Alert.Heading>Hey, nice to see you</Alert.Heading>
                <p>
                    Welcome to my React webstore made with redux and a RestAPI
                </p>
            </Alert>
        </Container>
        
    )
}

export default Homepage