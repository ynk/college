import './App.css';
import { decrement, increment } from './store/counter/actions';

import { store } from './store';
import Child from './components/Child';
import ReposList from './components/ReposList';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Button} from 'react-bootstrap';
// In deze App component wordt de state vermeerderd en verminderd via de buttons
// Dit wordt gedaan door middel van een increment/decrement action te dispatchen naar de store
function App() {


  return (
    <div className="App">
      <div className="App-header" style={{borderStyle: "dotted", borderWidth: 15,  padding: 5}}>
        <h1>App</h1>
        <Button onClick={() => store.dispatch(increment())} variant="outline-info">Increment</Button>
        <Button onClick={() => store.dispatch(decrement())} variant="outline-info">Decrement</Button>
        <Child />
      </div>
      <ReposList />
    </div>
  );
}

export default App;
