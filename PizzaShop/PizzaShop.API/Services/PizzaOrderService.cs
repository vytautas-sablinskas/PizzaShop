using PizzaShop.API.Calculations;
using PizzaShop.API.Dtos.PizzaOrders;
using PizzaShop.API.Utilities;
using PizzaShop.Data.Constants;
using PizzaShop.Data.Entities;
using PizzaShop.Data.Repositories;

namespace PizzaShop.API.Services
{
    public class PizzaOrderService : IPizzaOrderService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPizzaOrderRepository _pizzaOrderRepository;

        public PizzaOrderService(IUserRepository userRepository, IPizzaOrderRepository pizzaOrderRepository)
        {
            _userRepository = userRepository;
            _pizzaOrderRepository = pizzaOrderRepository;
        }

        public ResultWithData<PizzaOrderDto> AddPizzaOrder(AddPizzaOrderDto orderDto)
        {
            var isViablePizzaSize = Pizzas.Sizes.Select(size => size.ToLower()).Contains(orderDto.Size.ToLower());
            if (!isViablePizzaSize)
            {
                return new ResultWithData<PizzaOrderDto>
                {
                    ResultType = ResultType.Failure,
                    Message = "Only available sizes are: 1. Small, 2. Medium, 3. Large"
                };
            }

            var user = _userRepository.GetUser(orderDto.CustomerName);
            if (user == null)
            {
                user = _userRepository.AddUser(orderDto.CustomerName);
            }

            var order = MapToPizzaOrder(orderDto, user);
            AddCostToOrder(order);
            var addedPizzaOrder = _pizzaOrderRepository.AddPizzaOrder(order);

            return new ResultWithData<PizzaOrderDto>
            {
                ResultType = ResultType.Success,
                Data = MapToPizzaOrderDto(addedPizzaOrder)
            };
        }

        public ResultWithData<IEnumerable<PizzaOrderDto>> GetUserPizzaOrderDtos(GetPizzaOrdersDto getOrdersDto)
        {
            var user = _userRepository.GetUser(getOrdersDto.CustomerName);
            if (user == null)
            {
                return new ResultWithData<IEnumerable<PizzaOrderDto>>
                {
                    ResultType = ResultType.Failure,
                    Message = $"User with username: '{getOrdersDto.CustomerName}' was not found"
                };
            }

            var orders = _pizzaOrderRepository.GetUserOrders(getOrdersDto.CustomerName);

            return new ResultWithData<IEnumerable<PizzaOrderDto>>
            {
                ResultType = ResultType.Success,
                Data = orders.Select(MapToPizzaOrderDto)
            };
        }

        private static PizzaOrder MapToPizzaOrder(AddPizzaOrderDto pizzaOrder, User customer)
        {
            return new PizzaOrder
            {
                Customer = customer,
                Size = pizzaOrder.Size,
                Toppings = pizzaOrder.Toppings.Select(MapToTopping).ToList()
            };
        }

        private static PizzaOrderDto MapToPizzaOrderDto(PizzaOrder pizzaOrder)
        {
            return new PizzaOrderDto
            (
                Id: pizzaOrder.Id,
                Size: pizzaOrder.Size,
                Cost: pizzaOrder.Cost,
                Toppings: pizzaOrder.Toppings.Select(MapToToppingDto)
            );
        }

        private static Topping MapToTopping(ToppingDto toppingDto)
        {
            return new Topping
            {
                Name = toppingDto.Name,
            };
        }

        private static ToppingDto MapToToppingDto(Topping topping)
        {
            return new ToppingDto(
                Name: topping.Name
            );
        }

        private static void AddCostToOrder(PizzaOrder order)
        {
            IPizzaOrderCostDecorator orderCostBuilder = new PizzaOrderCostDecorator(order);

            orderCostBuilder
                .AddPriceForSize()
                .AddPriceForToppings()
                .AddToppingsDiscountIfEligible();
        }
    }
}
