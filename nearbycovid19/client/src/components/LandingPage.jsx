import React from "react";
import { Button } from "semantic-ui-react";
import "../App.css";
import { Link } from "react-router-dom";

const LandingPage = () => {
  return (
    <header className="App-header">
      <div>
        <Button size="massive">
          <Link to="/victim" color="red">
            Victim
          </Link>
        </Button>
        <Button size="massive">
          <Link to="/user" color="red">
            User
          </Link>
        </Button>
      </div>
    </header>
  );
};

export default LandingPage;
