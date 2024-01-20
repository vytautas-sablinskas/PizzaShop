import { Box, Button, TextField, Typography } from "@mui/material";
import axios from "axios";
import React, { useEffect, useState } from "react";
import { Endpoints } from "../utilities/Endpoints";

const SavedPizzaOrders = (): JSX.Element => {
  const [savedPizzas, setSavedPizzas] = useState<PizzaOrderType[]>([]);
  const [userOrdersToGet, setUserOrdersToGet] = useState<string>("");
  const [isUsernameEntered, setIsUsernameEntered] = useState<boolean>(false);
  const [errorMessage, setErrorMessage] = useState<string>("");

  const addUsername = () => {
    if (userOrdersToGet === "") {
      setErrorMessage("Please enter a username");
      return;
    }

    setErrorMessage("");
    setIsUsernameEntered(true);
  };

  useEffect(() => {
    if (!isUsernameEntered) {
      return;
    }

    const fetchData = async () => {
      try {
        const response = await axios.get(
          `${Endpoints.BASE_URL}/orders?CustomerName=${userOrdersToGet}`
        );
        setSavedPizzas(response.data);
      } catch (error) {
        setErrorMessage("Username not found");
        setIsUsernameEntered(false);
      }
    };

    fetchData();
  }, [isUsernameEntered]);

  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
        pt: '10px',
        ...(isUsernameEntered ? {} : { height: "100%" })
      }}
    >
      {!isUsernameEntered ? (
        <Box sx={{ display: "flex", flexDirection: "row", justifyContent: 'center', alignItems: 'center' }}>
          <TextField
            id="username"
            label="Username"
            variant="outlined"
            value={userOrdersToGet}
            onChange={(event) => setUserOrdersToGet(event.target.value)}
            error={errorMessage !== ""}
            helperText={errorMessage}
          ></TextField>
          <Button onClick={addUsername} sx={{ height: "56px", ml: "10px"}}>
            Save Username
          </Button>
        </Box>
      ) : (
        <Box sx={{mt: 'auto'}}>
          {savedPizzas.map((savedPizza) => {
            return (
              <Box
                key={savedPizza.id}
                sx={{
                  display: "flex",
                  flexDirection: "row",
                  justifyContent: "flex-start",
                  alignItems: "flex-start",
                  minWidth: "500px",
                  maxWidth: "550px",
                  border: "1px solid grey",
                  padding: '10px',
                  borderRadius: '10px',
                  marginBottom: '10px'
                }}
              >
                <img src="/pizzas/pizza.jpg" alt="Pizza" style={{maxWidth: "50%", borderRadius: '10px', marginLeft: '10px'}} />
                <Box sx={{display: 'flex', flexDirection: 'column', width: '150px', mx: '10px'}}>
                  <Typography sx={{whiteSpace: 'nowrap'}}>
                    Pizza size: {savedPizza.size}
                  </Typography>
                  <Typography>Total cost: <strong>{savedPizza.cost}€</strong></Typography>
                </Box>
                <Box sx={{display: 'flex', flexDirection: 'column', mx: '10px'}}>
                  {savedPizza.toppings.length <= 0 ? (
                    <Typography>No toppings</Typography>
                  ) : (
                    <Box sx={{flex: 1}}>
                      {savedPizza.toppings.map((topping) => {
                        return <Typography key={topping.name}>{topping.name}</Typography>
                      })}
                    </Box>
                    
                  )}
                </Box>
              </Box>
            );
          })}
        </Box>
      )}
    </Box>
  );
};

export default SavedPizzaOrders;
