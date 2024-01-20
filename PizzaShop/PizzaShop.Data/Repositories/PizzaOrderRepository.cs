using Microsoft.EntityFrameworkCore;
using PizzaShop.Data.Database;
using PizzaShop.Data.Entities;

namespace PizzaShop.Data.Repositories
{
    public class PizzaOrderRepository : IPizzaOrderRepository
    {
        private readonly PizzaShopContext _context;

        public PizzaOrderRepository(PizzaShopContext context)
        {
            _context = context;
        }

        public PizzaOrder AddPizzaOrder(PizzaOrder pizzaOrder)
        {
            AddToppings(pizzaOrder.Toppings);

            var addedOrder = _context.PizzaOrders.Add(pizzaOrder).Entity;
            _context.SaveChanges();

            return addedOrder;
        }

        private void AddToppings(ICollection<Topping> toppings)
        {
            foreach (var topping in toppings)
            {
                var toppingExists = _context.Toppings.Any(t => t.Name.ToLower() == topping.Name.ToLower());
                if (!toppingExists)
                {
                    _context.Toppings.Add(topping);
                }
            }
        }

        public IQueryable<PizzaOrder> GetUserOrders(string customerName)
        {
            return _context.PizzaOrders.Where(order => order.Customer.UserName == customerName)
                                       .Include(o => o.Toppings);
        }
    }
}