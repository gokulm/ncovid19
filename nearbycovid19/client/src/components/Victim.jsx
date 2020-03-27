import React, { useState } from "react";
import { Container, Header, Table, Button, Input } from "semantic-ui-react";
import UUID from "uuid-js";
import axios from "axios";

const Victim = () => {
  const [locationsVisited, setLocationsVisited] = useState([
    { id: UUID.create().toString() }
  ]);
  const [isSubmitted, setIsSubmitted] = useState(false);

  const onInputChangeHandler = (data, type, id) => {
    console.log(data, type, id);
    // debugger;
    setLocationsVisited(prevState => {
      return prevState.map(locationVisited => {
        if (id === locationVisited.id) {
          if (type === "date") {
            locationVisited.visitedDate = data;
          } else {
            locationVisited.address = data;
          }
        }
        return locationVisited;
      });
    });
  };

  const addLocationHandler = () => {
    setLocationsVisited(prevState => [
      ...prevState,
      { id: UUID.create().toString() }
    ]);
  };

  const onSubmitHandler = () => {
    axios
      .post("http://localhost:5000/victim/locations", {
        locations: locationsVisited
      })
      .then(data => {
        if (data) {
          setIsSubmitted(true);
        }
      });
    console.log(locationsVisited);
  };

  return (
    <Container fliud="true">
      <Header as="h1" color="red" size="huge">
        Victim
      </Header>
      <Button primary onClick={addLocationHandler}>
        Add Location
      </Button>
      <Table celled>
        <Table.Header>
          <Table.Row>
            <Table.HeaderCell>Date</Table.HeaderCell>
            <Table.HeaderCell>Location</Table.HeaderCell>
          </Table.Row>
        </Table.Header>
        <Table.Body>
          {locationsVisited.map(locationVisited => {
            return (
              <Table.Row key={locationVisited.id}>
                <Table.Cell>
                  <Input
                    fluid
                    placeholder="Date.."
                    onChange={(e, data) => {
                      onInputChangeHandler(
                        data.value,
                        "date",
                        locationVisited.id
                      );
                    }}
                  />
                </Table.Cell>
                <Table.Cell>
                  <Input
                    fluid
                    placeholder="Location.."
                    onChange={(ee, data) => {
                      onInputChangeHandler(
                        data.value,
                        "location",
                        locationVisited.id
                      );
                    }}
                  />
                </Table.Cell>
              </Table.Row>
            );
          })}
        </Table.Body>
      </Table>
      <Button onClick={onSubmitHandler} color="teal">
        Submit
      </Button>
      {isSubmitted && <p style={{ fontWeight: "bold" }}>Form Submitted!</p>}
    </Container>
  );
};

export default Victim;
