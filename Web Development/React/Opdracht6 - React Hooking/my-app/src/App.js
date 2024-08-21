import logo from './logo.svg';
import './App.css';
import Feedback from "./component/Feedback"

const App = () => {
  return (
    <div className="App">
      <Feedback head="good">Good</Feedback>
      <Feedback head="neutraal">Neutraal</Feedback>
      <Feedback head="bad">Bad</Feedback>
    </div>
  );
}

export default App;
