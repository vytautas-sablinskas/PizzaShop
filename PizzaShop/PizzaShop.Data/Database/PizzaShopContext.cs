using Microsoft.EntityFrameworkCore;
using PizzaShop.Data.Entities;

namespace PizzaShop.Data.Database
{
    public class PizzaShopContext : DbContext
    {
        public DbSet<PizzaOrder> PizzaOrders { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Topping> Toppings { get; set; }

        public PizzaShopContext()
        {
            
        }

        public PizzaShopContext(DbContextOptions<PizzaShopContext> options) : base(options)
        {
        }
    }   
}