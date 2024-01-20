using Microsoft.AspNetCore.Mvc;
using PizzaShop.API.Dtos.PizzaOrders;
using PizzaShop.API.Services;
using PizzaShop.API.Utilities;

namespace PizzaShop.API.Controllers
{
    [Route("/api/v1/")]
    [ApiController]
    public class PizzaController : Controller
    {
        private readonly IPizzaOrderService _orderService;

        public PizzaController(IPizzaOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("order")]
        public IActionResult AddPizzaOrder(AddPizzaOrderDto pizzaOrderDto)
        {
            var result = _orderService.AddPizzaOrder(pizzaOrderDto);
            if (result.ResultType == ResultType.Failure) 
            {
                return UnprocessableEntity(result.Message);
            }

            return Created("~/api/v1/order", result.Data);
        }

        [HttpGet("orders")]
        public IActionResult GetPizzaOrders([FromQuery] GetPizzaOrdersDto getUserOrdersDto)
        {
            var result = _orderService.GetUserPizzaOrderDtos(getUserOrdersDto);
            if (result.ResultType == ResultType.Failure)
                return UnprocessableEntity(result.Message);

            return Ok(result.Data);
        }
    }
}