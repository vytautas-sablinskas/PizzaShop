import { AppBar, Box, Button, Toolbar } from "@mui/material";
import React from "react";
import { Link } from "react-router-dom";

const Header = () : JSX.Element => {
    return (
        <Box sx={{ flexGrow: 1 }}>
            <AppBar position="relative" sx={{ backgroundColor: '#404040'}}>
                <Toolbar>
                    <Button component={Link} to="/" sx={{ color: '#E1D9D1', flex: 1}}>
                        Pizza Selection
                    </Button>
                    <Button component={Link} to="/orders" sx={{ color: '#E1D9D1', flex: 1}}>
                        Orders
                    </Button>
                </Toolbar>
            </AppBar>
        </Box>
    )
}

export default Header;