import React, { useState, useEffect, Fragment } from "react";
import { Container, Header, Table, Input, Button } from "semantic-ui-react";
import axios from "axios";

const User = () => {
  const [currentVulnerableLocations, setCurrentVulnerableLocations] = useState(
    []
  );
  const [desiredVulnerableLocations, setDesiredVulnerableLocations] = useState(
    []
  );

  const [location, setLocation] = useState("");
  const [isSafe, setIsSafe] = useState(false);

  useEffect(() => {
    axios
      .post("http://localhost:5000/victim/visitedlocations", {
        location: "800 N Glebe Road",
        radius: 2
      })
      .then(data => {
        console.log(data);
        setCurrentVulnerableLocations(data.data);
      });
  }, [location]);

  const changeLocationHandler = (e, data) => {
    console.log(data.value);
    setLocation(data.value);
  };

  const onSubmitHandler = () => {
    axios
      .post("http://localhost:5000/victim/visitedlocations", {
        location,
        radius: 2
      })
      .then(data => {
        if (data.data.length === 0) {
          setIsSafe(true);
        } else {
          setIsSafe(false);
        }
        setDesiredVulnerableLocations(data.data);
      });
  };

  return (
    <Container fliud="true">
      <Header as="h1" color="blue" size="huge">
        User
      </Header>
      <p>List of Vulnerable locations based on Current Location</p>
      <Table celled>
        <Table.Header>
          <Table.Row>
            <Table.HeaderCell>Date</Table.HeaderCell>
            <Table.HeaderCell>Location</Table.HeaderCell>
          </Table.Row>
        </Table.Header>
        <Table.Body>
          {currentVulnerableLocations.map(vulnerableLocation => {
            return (
              <Table.Row key={vulnerableLocation.id}>
                <Table.Cell>{vulnerableLocation.visitedDate}</Table.Cell>
                <Table.Cell>{vulnerableLocation.address}</Table.Cell>
              </Table.Row>
            );
          })}
        </Table.Body>
      </Table>
      <div style={{ textAlign: "left" }}>
        <Input
          label="Desired Location"
          placeholder="Location.."
          onChange={changeLocationHandler}
        ></Input>
        <Button primary onClick={onSubmitHandler}>
          Submit
        </Button>
      </div>
      {isSafe && (
        <p style={{ fontWeight: "bold" }}>It is safe to visit this location!</p>
      )}
      {desiredVulnerableLocations.length > 0 && (
        <>
          {" "}
          <p>List of Vulnerable Locations based on Desired Location</p>
          <Table celled>
            <Table.Header>
              <Table.Row>
                <Table.HeaderCell>Date</Table.HeaderCell>
                <Table.HeaderCell>Location</Table.HeaderCell>
              </Table.Row>
            </Table.Header>
            <Table.Body>
              {desiredVulnerableLocations.map(vulnerableLocation => {
                return (
                  <Table.Row key={vulnerableLocation.id}>
                    <Table.Cell>{vulnerableLocation.visitedDate}</Table.Cell>
                    <Table.Cell>{vulnerableLocation.address}</Table.Cell>
                  </Table.Row>
                );
              })}
            </Table.Body>
          </Table>
        </>
      )}
    </Container>
  );
};

export default User;
