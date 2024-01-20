import {
  Alert,
  Box,
  Button,
  Checkbox,
  FormControl,
  FormControlLabel,
  FormGroup,
  FormLabel,
  Grid,
  InputLabel,
  MenuItem,
  Select,
  TextField,
  Typography,
} from "@mui/material";
import React, { useEffect, useState } from "react";
import axios from "axios";
import CheckIcon from "@mui/icons-material/Check";
import { Endpoints } from "../utilities/Endpoints";

const PizzaSelection = (): JSX.Element => {
  const pizzaSizes: PizzaSizeType[] = [
    {
      name: "Small",
      price: 8.0,
    },
    {
      name: "Medium",
      price: 10.0,
    },
    {
      name: "Large",
      price: 12.0,
    },
  ];

  const pizzaToppings: PizzaToppingType[] = [
    {
      name: "Pepperoni",
    },
    {
      name: "Meat",
    },
    {
      name: "Mushrooms",
    },
    {
      name: "Onions",
    },
    {
      name: "Green Peppers",
    },
    {
      name: "Cheese",
    },
  ];

  const [totalPrice, setTotalPrice] = useState<number>(8);
  const [selectedToppings, setSelectedToppings] = useState<{ name: string }[]>(
    []
  );
  const [selectedSize, setSelectedSize] = useState<string>(
    pizzaSizes.at(0)?.name ?? ""
  );
  const [buyerName, setBuyerName] = useState<string>("");
  const [errorMessage, setErrorMessage] = useState<string>("");
  const [isOrderAdded, setIsOrderAdded] = useState<boolean>(false);

  const handleToppingSelection = (e: React.ChangeEvent<HTMLInputElement>) => {
    const topping = { name: e.target.value };

    if (e.target.checked) {
      setSelectedToppings([...selectedToppings, topping]);
    } else {
      setSelectedToppings(
        selectedToppings.filter(
          (selectedTopping) => selectedTopping.name !== topping.name
        )
      );
    }
  };

  const handleAddToOrder = () => {
    if (buyerName === "") {
      setErrorMessage("Please enter your username");
      return;
    }

    const addOrderBody = {
      size: selectedSize,
      customerName: buyerName,
      toppings: selectedToppings,
    };

    axios
      .post(`${Endpoints.BASE_URL}/order`, addOrderBody)
      .then((response) => {
        console.log(response);
        if (response.status === 201) {
          setIsOrderAdded(true);
          setTimeout(() => {
            setIsOrderAdded(false);
          }, 4000);
        }
      })
      .catch((error) => {
        console.log(error);
      });
  };

  useEffect(() => {
    const selectedPizza = pizzaSizes.find(
      (pizzaSize) => pizzaSize.name === selectedSize
    );
    const totalPriceBeforeDeduction: number =
      selectedToppings.length * 1 + (selectedPizza?.price ?? 0);

    setTotalPrice(
      selectedToppings.length > 3
        ? totalPriceBeforeDeduction * 0.9
        : totalPriceBeforeDeduction
    );
  }, [selectedToppings, selectedSize]);

  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
        height: "100%",
      }}
    >
      {isOrderAdded && (
        <Alert
          icon={<CheckIcon fontSize="inherit" />}
          sx={{ mb: '20px' }}
          severity="success"
        >
          Your order was successfully added to checkout!
        </Alert>
      )}
      <Box
        sx={{
          display: "flex",
          flexDirection: "column",
          justifyContent: "center",
          alignItems: "center",
          width: "500px",
          border: "1px solid grey",
        }}
      >
        <Typography variant="h4" sx={{ my: "5px" }}>
          Make Your Own Pizza
        </Typography>
        <Box>
          <img
            src={"/pizzas/pizza.jpg"}
            alt={"Pizza"}
            style={{ height: "40%", borderRadius: "10px", maxWidth: "100%" }}
          />
          <Box
            sx={{
              my: 2,
              display: "flex",
              flexDirection: "row",
              justifyContent: "space-around",
            }}
          >
            <FormControl>
              <FormLabel component="legend">Select toppings</FormLabel>
              {pizzaToppings.map((pizzaTopping) => {
                return (
                  <React.Fragment key={pizzaTopping.name}>
                    <FormGroup>
                      <FormControlLabel
                        control={
                          <Checkbox
                            value={pizzaTopping.name}
                            checked={selectedToppings.some(
                              (topping) => topping.name === pizzaTopping.name
                            )}
                            onChange={handleToppingSelection}
                            size="small"
                          />
                        }
                        label={pizzaTopping.name}
                      />
                    </FormGroup>
                  </React.Fragment>
                );
              })}
            </FormControl>
            <FormControl sx={{ marginTop: "10px" }}>
              <InputLabel id="size-label">Pizza Size</InputLabel>
              <Select
                labelId="size-label"
                id="size-select"
                value={selectedSize}
                label="Pizza Size"
                onChange={(e) => {
                  setSelectedSize(e.target.value);
                }}
              >
                {pizzaSizes.map((pizzaSize) => {
                  return (
                    <MenuItem key={pizzaSize.name} value={pizzaSize.name}>
                      {pizzaSize.name}
                    </MenuItem>
                  );
                })}
              </Select>
            </FormControl>
          </Box>
          <Box
            sx={{
              display: "flex",
              justifyContent: "center",
              alignItems: "center",
            }}
          >
            <Typography>
              Total Price: <strong>{totalPrice.toFixed(2)}€</strong>
            </Typography>
          </Box>
          <Grid container spacing={2} sx={{ mt: "2px" }}>
            <Grid item xs={8}>
              <TextField
                id="username"
                label="Username"
                variant="filled"
                onChange={(e) => setBuyerName(e.target.value)}
                error={errorMessage !== ""}
                helperText={errorMessage}
                sx={{ height: "50px", paddingY: "0" }}
              >
                {buyerName}
              </TextField>
            </Grid>
            <Grid item xs={4}>
              <Button
                variant="contained"
                sx={{ paddingY: "0", height: "56px" }}
                onClick={handleAddToOrder}
              >
                Add To Order
              </Button>
            </Grid>
          </Grid>
        </Box>
      </Box>
    </Box>
  );
};

export default PizzaSelection;
