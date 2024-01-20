import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import Header from './shared-components/Header';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import PizzaSelection from './pizza-selection/PizzaSelection';
import SavedPizzaOrders from './saved-orders/SavedPizzaOrders';

const root = document.getElementById("root") as HTMLElement;
const appRoot = ReactDOM.createRoot(root);

appRoot.render(
  <React.StrictMode>
    <BrowserRouter>
      <Header />
      <Routes>
        <Route path="/" element={<PizzaSelection />} />
        <Route path="/orders" element={<SavedPizzaOrders />} />
      </Routes>
    </BrowserRouter>
  </React.StrictMode>
);