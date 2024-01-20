using PizzaShop.API.Calculations;
using PizzaShop.Data.Entities;

namespace PizzaShop.UnitTests
{
    public class PizzaOrderCostDecoratorTests
    {
        private readonly PizzaOrder pizzaOrder;
        private readonly PizzaOrderCostDecorator underTest;

        public PizzaOrderCostDecoratorTests()
        {
            pizzaOrder = new PizzaOrder();
            underTest = new PizzaOrderCostDecorator(pizzaOrder);
        }

        [Theory]
        [InlineData("Small", 8)]
        [InlineData("small", 8)]
        [InlineData("Medium", 10)]
        [InlineData("medium", 10)]
        [InlineData("Large", 12)]
        [InlineData("large", 12)]
        [InlineData("Incorrect", -1)]
        public void AddPriceForSize_DependingOnInput_ShouldReturnDifferentCost(string size, int expectedCost)
        {
            pizzaOrder.Size = size;

            underTest.AddPriceForSize();

            Assert.Equal(expectedCost, pizzaOrder.Cost);
        }

        [Theory]
        [MemberData(nameof(AddPriceForToppingsTestData))]
        public void AddPriceForToppings_ShouldAddCostEqualToToppingCount(List<Topping> toppings, int expectedCost)
        {
            pizzaOrder.Toppings = toppings;

            underTest.AddPriceForToppings();

            Assert.Equal(expectedCost, pizzaOrder.Cost);
        }

        [Theory]
        [MemberData(nameof(AddAddToppingsDiscountIfEligibleTestData))]
        public void AddToppingsDiscountIfEligible_DependingOnEligibility_ShouldDecideToGiveOrNotGiveDiscount(List<Topping> toppings, int initialCost, int expectedCost)
        {
            pizzaOrder.Cost = initialCost;
            pizzaOrder.Toppings = toppings;

            underTest.AddToppingsDiscountIfEligible();

            Assert.Equal(expectedCost, pizzaOrder.Cost);
        }

        public static IEnumerable<object[]> AddPriceForToppingsTestData()
        {
            yield return new object[] { new List<Topping>(), 0 };
            yield return new object[] { new List<Topping> { new Topping(), new Topping() }, 2 };
        }

        public static IEnumerable<object[]> AddAddToppingsDiscountIfEligibleTestData()
        {
            yield return new object[] { new List<Topping>(), 10, 10 };
            yield return new object[] { new List<Topping> { new Topping(), new Topping(), new Topping(), new Topping() }, 10, 9 };
        }
    }
}
