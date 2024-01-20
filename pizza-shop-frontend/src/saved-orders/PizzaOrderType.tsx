type PizzaOrderType = {
    id: number,
    size: string,
    cost: number,
    toppings: { name: string }[],
}