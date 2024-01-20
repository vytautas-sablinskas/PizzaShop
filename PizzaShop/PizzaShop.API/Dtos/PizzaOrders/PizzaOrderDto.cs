namespace PizzaShop.API.Dtos.PizzaOrders
{
    public record PizzaOrderDto(
        int Id,

        string Size,

        double Cost,

        IEnumerable<ToppingDto> Toppings
    );
}