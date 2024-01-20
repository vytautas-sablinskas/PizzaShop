using System.ComponentModel.DataAnnotations;

namespace PizzaShop.API.Dtos.PizzaOrders
{
    public record GetPizzaOrdersDto
    (
        [Required(ErrorMessage = "Customer name must be specified.")]
        string CustomerName
    );
}