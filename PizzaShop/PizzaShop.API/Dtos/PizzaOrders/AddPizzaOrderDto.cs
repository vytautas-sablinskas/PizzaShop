using System.ComponentModel.DataAnnotations;

namespace PizzaShop.API.Dtos.PizzaOrders
{
    public record AddPizzaOrderDto
    (
        [Required(ErrorMessage = "Size must be specified.")]
        string Size,

        [Required(ErrorMessage = "Customer name must be specified.")]
        string CustomerName,

        IEnumerable<ToppingDto>? Toppings
    )
    {
        public AddPizzaOrderDto() : this("", "", new List<ToppingDto>()) { }
    }
}