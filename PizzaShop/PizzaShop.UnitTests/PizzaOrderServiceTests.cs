using Moq;
using PizzaShop.API.Dtos.PizzaOrders;
using PizzaShop.API.Services;
using PizzaShop.API.Utilities;
using PizzaShop.Data.Entities;
using PizzaShop.Data.Repositories;

namespace PizzaShop.UnitTests
{
    public class PizzaOrderServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IPizzaOrderRepository> _mockOrderRepository;
        private readonly PizzaOrderService underTest;

        public PizzaOrderServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockOrderRepository = new Mock<IPizzaOrderRepository>();
            underTest = new PizzaOrderService(_mockUserRepository.Object, _mockOrderRepository.Object);
        }

        [Fact]
        public void AddPizzaOrder_InvalidSize_ShouldReturnFailureWithMessage()
        {
            var invalidSize = "invalid size";
            var expectedMessage = "Only available sizes are: 1. Small, 2. Medium, 3. Large";

            var result = underTest.AddPizzaOrder(new AddPizzaOrderDto(Size: invalidSize, CustomerName: "test", new List<ToppingDto>()));

            Assert.True(result.ResultType == ResultType.Failure);
            Assert.Equal(expectedMessage, result.Message);
        }

        [Fact]
        public void AddPizzaOrder_ValidSizeInvalidUser_ShouldCallAddUserMethodAndReturnCorrectPizzaOrderDto()
        {
            var validSize = "Medium";
            var expectedUser = new User();
            var expectedPizzaProperties = new PizzaOrder
            {
                Cost = 5,
                Toppings = new List<Topping>
                {
                    new Topping("Expected")
                },
                Customer = expectedUser,
                Size = validSize,
                Id = 4
            };

            _mockUserRepository.Setup(m => m.GetUser(It.IsAny<string>()))
                .Returns((User)null);
            _mockUserRepository.Setup(m => m.AddUser(It.IsAny<string>()))
                .Returns(new User());
            _mockOrderRepository.Setup(m => m.AddPizzaOrder(It.IsAny<PizzaOrder>()))
                .Returns(expectedPizzaProperties);

            var result = underTest.AddPizzaOrder(new AddPizzaOrderDto(Size: validSize, CustomerName: "test", new List<ToppingDto> { new ToppingDto("Cheese")}));

            _mockUserRepository.Verify(m => m.AddUser(It.IsAny<string>()), Times.Once);
            Assert.True(result.ResultType == ResultType.Success);
            Assert.Equal(expectedPizzaProperties.Cost, result.Data.Cost);
            Assert.Equal(expectedPizzaProperties.Toppings.Count, result.Data.Toppings.Count());
            Assert.Equal(expectedPizzaProperties.Size, result.Data.Size);
            Assert.Equal(expectedPizzaProperties.Id, result.Data.Id);
        }

        [Fact]
        public void AddPizzaOrder_ValidSizeValidUser_ShouldNotCallAddUserMethodAndReturnCorrectPizzaOrderDto()
        {
            var validSize = "Medium";
            var expectedUser = new User();
            var expectedPizzaProperties = new PizzaOrder
            {
                Cost = 5,
                Toppings = new List<Topping>
                {
                    new Topping("Expected")
                },
                Customer = expectedUser,
                Size = validSize,
                Id = 4
            };

            _mockUserRepository.Setup(m => m.GetUser(It.IsAny<string>()))
                .Returns(new User());
            _mockOrderRepository.Setup(m => m.AddPizzaOrder(It.IsAny<PizzaOrder>()))
                .Returns(expectedPizzaProperties);

            var result = underTest.AddPizzaOrder(new AddPizzaOrderDto(Size: validSize, CustomerName: "test", new List<ToppingDto> { new ToppingDto("Cheese") }));

            _mockUserRepository.Verify(m => m.AddUser(It.IsAny<string>()), Times.Never);
            Assert.True(result.ResultType == ResultType.Success);
            Assert.Equal(expectedPizzaProperties.Cost, result.Data.Cost);
            Assert.Equal(expectedPizzaProperties.Toppings.Count, result.Data.Toppings.Count());
            Assert.Equal(expectedPizzaProperties.Size, result.Data.Size);
            Assert.Equal(expectedPizzaProperties.Id, result.Data.Id);
        }

        [Fact]
        public void GetUserPizzaOrdersDto_InvalidUser_ReturnsFailureWithMessage()
        {
            var invalidName = "invalidName";
            var expectedMessage = $"User with username: '{invalidName}' was not found";

            _mockUserRepository.Setup(m => m.GetUser(It.IsAny<string>()))
                .Returns((User)null);

            var result = underTest.GetUserPizzaOrderDtos(new GetPizzaOrdersDto(invalidName));

            Assert.True(result.ResultType == ResultType.Failure);
            Assert.Equal(expectedMessage, result.Message);
        }

        [Fact]
        public void GetUserPizzaOrdersDto_ValidUser_ShouldReturnCorrectOrders()
        {
            var validName = "validName";
            var expectedList = new List<PizzaOrder>
            {
                new PizzaOrder(1, "Medium", 3, new List<Topping>(), new User()),
                new PizzaOrder(1, "Medium", 3, new List<Topping>(), new User())
            };

            _mockUserRepository.Setup(m => m.GetUser(It.IsAny<string>()))
                .Returns(new User());
            _mockOrderRepository.Setup(m => m.GetUserOrders(It.IsAny<string>()))
                .Returns(expectedList.AsQueryable());

            var result = underTest.GetUserPizzaOrderDtos(new GetPizzaOrdersDto(validName));

            Assert.True(result.ResultType == ResultType.Success);
            Assert.Equal(expectedList.Count, result.Data.Count());
        }
    }
}
