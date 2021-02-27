import React from "react";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import AdminPage from "./admin/admin";
import Login from './auth/login';
import Register from './auth/register';
import HomePage from "./Home/Home";
import CreateTemp from './staff/CreateTemp';
import history from './history'
import imageDisplay from "./images/imageDisplay";
import Public from "./Public/Public"
 function App(){
   return (
    <React.Fragment>
      <Router  history={history}>
        <Switch>
          <Route  path="/" exact component={HomePage} />
          <Route  path="/Home" exact component={HomePage} />
          <Route  path="/login" exact component={Login} />
          <Route  path="/register" exact component={Register} />
          <Route  path="/admin" component={AdminPage} />  
          <Route  path="/createTemperature" component={CreateTemp} />
          <Route  path="/imageDisplay" component={imageDisplay} />
          <Route  path="/Public" component={Public} />
          
        </Switch>
      </Router>
    </React.Fragment>
      );
}
export default App;