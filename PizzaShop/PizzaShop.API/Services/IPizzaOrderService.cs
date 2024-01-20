using PizzaShop.API.Dtos.PizzaOrders;
using PizzaShop.API.Utilities;

namespace PizzaShop.API.Services
{
    public interface IPizzaOrderService
    {
        ResultWithData<PizzaOrderDto> AddPizzaOrder(AddPizzaOrderDto orderDto);
        ResultWithData<IEnumerable<PizzaOrderDto>> GetUserPizzaOrderDtos(GetPizzaOrdersDto getOrdersDto);
    }
}