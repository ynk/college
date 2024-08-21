import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { addOne, removeOne } from '../store/test/slice';
// Importeren van de asyncThunk action creator
import { Card, Button,Container } from 'react-bootstrap';

import { fetchRepos } from '../store/repos/slice';

const ReposList = () => {

    const dispatch = useDispatch();
    const reposState = useSelector(state => state.repos);

    useEffect( () =>{
        dispatch(fetchRepos()) // on page load
        
    }, [])
    const { repos } = reposState;

    return (
        <div>
            <Container>
            {repos.map(r => 
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
            )}
        </Container>
        </div>
    )
}

export default ReposList
