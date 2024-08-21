import React from 'react';

import { BrowserRouter as Router, Route } from "react-router-dom"
import Menu from "./components/menu"
import Footer from "./components/footer"
import Itempage from "./components/itempage"
import 'bootstrap/dist/css/bootstrap.min.css';
import Orderpage from './components/orderpage';
import Contactpage from './components/contactpage';
import Confirmation from './components/confirmation';
import Homepage from './components/homepage';
import "../src/index.css"


function App() {
  return (
    <div className="App">
      <Router>
        <Menu />
        <Route exact path={"/"}>
          <Homepage />
        </Route>
        <Route path={"/store"}>
          <Itempage />
        </Route>
        <Route path={"/cart"}>
          <Orderpage />
        </Route>
        <Route path={"/order"}>
          <Contactpage />
        </Route>
        <Route path={"/confirmation"}>
          <Confirmation />
        </Route>
        <Footer />
      </Router>
    </div>
  );
}

export default App;
