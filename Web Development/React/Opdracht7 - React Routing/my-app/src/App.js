
import './App.css';
import {BrowserRouter as Router, Link,Route} from "react-router-dom"
import axios from "axios"
import { useEffect, useState } from 'react';

import Apis from "./componenten/Apis"
import Api from "./componenten/Api"
const App = () =>  {

  const [result,setResult] = useState([])
  useEffect(() =>{
    axios.get("https://api.publicapis.org/entries?category=development").then((response)=> setResult(response.data["entries"]))

    
    //fetch("https://api.publicapis.org/entries?category=development").then((response)=>{return response.json()}).then((data)=>console.log(data)).catch(err=>console.log(err));
  },[])
  console.log(result)

  return (

    <Router>
      <div>
      <Link  style={{padding:5}} to={"/"}>Home</Link>
        <Link  style={{padding:5}} to={"/apis"}>List api</Link>
      </div>
      <Route path={"*/apis*"}>
        <Apis data={result} />
      </Route>
      <Route path={"*/api/:id*"}>
        <Api data={result} />
      </Route>
    </Router>
  );
}

export default App;
