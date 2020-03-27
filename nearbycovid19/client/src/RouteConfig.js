import React from "react";
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";
import LandingPage from "./components/LandingPage";
import Victim from "./components/Victim";
import User from "./components/User";

const RouteConfig = () => {
  return (
    <Router>
      <Switch>
        <Route exact path="/" render={LandingPage} />
        <Route exact path="/victim">
          <Victim></Victim>
        </Route>
        <Route exact path="/user">
          <User />
        </Route>
      </Switch>
    </Router>
  );
};

export default RouteConfig;
