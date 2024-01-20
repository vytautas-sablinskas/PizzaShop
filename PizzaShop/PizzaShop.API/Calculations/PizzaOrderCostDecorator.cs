using PizzaShop.Data.Entities;

namespace PizzaShop.API.Calculations
{
    public class PizzaOrderCostDecorator : IPizzaOrderCostDecorator
    {
        private const int MINIMUM_AMOUNT_OF_TOPPINGS_FOR_DISCOUNT = 4;

        private readonly PizzaOrder _pizzaOrder;

        public PizzaOrderCostDecorator(PizzaOrder pizzaOrder)
        {
            _pizzaOrder = pizzaOrder;
        }

        public IPizzaOrderCostDecorator AddPriceForSize()
        {
            _pizzaOrder.Cost += GetPriceBySize(_pizzaOrder.Size);

            return this;
        }

        public IPizzaOrderCostDecorator AddPriceForToppings()
        {
            _pizzaOrder.Cost += _pizzaOrder.Toppings.Count;

            return this;
        }

        public IPizzaOrderCostDecorator AddToppingsDiscountIfEligible()
        {
            if (_pizzaOrder.Toppings.Count >= MINIMUM_AMOUNT_OF_TOPPINGS_FOR_DISCOUNT)
                _pizzaOrder.Cost *= 0.9;

            return this;
        }

        private static int GetPriceBySize(string size)
        {
            return size.ToLower() switch
            {
                "small" => 8,
                "medium" => 10,
                "large" => 12,
                _ => -1,
            };
        }
    }
}
