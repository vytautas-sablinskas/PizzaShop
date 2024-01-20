using System.ComponentModel.DataAnnotations;

namespace PizzaShop.API.Dtos.PizzaOrders
{
    public record ToppingDto
    (
        [Required(ErrorMessage = "Topping name must be specified.")]
        string Name
    );
}