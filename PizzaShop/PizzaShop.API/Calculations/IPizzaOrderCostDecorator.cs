namespace PizzaShop.API.Calculations
{
    public interface IPizzaOrderCostDecorator
    {
        IPizzaOrderCostDecorator AddPriceForSize();
        IPizzaOrderCostDecorator AddPriceForToppings();
        IPizzaOrderCostDecorator AddToppingsDiscountIfEligible();
    }
}