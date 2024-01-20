using PizzaShop.Data.Entities;

namespace PizzaShop.Data.Repositories
{
    public interface IPizzaOrderRepository
    {
        PizzaOrder AddPizzaOrder(PizzaOrder pizzaOrder);
        IQueryable<PizzaOrder> GetUserOrders(string customerName);
    }
}