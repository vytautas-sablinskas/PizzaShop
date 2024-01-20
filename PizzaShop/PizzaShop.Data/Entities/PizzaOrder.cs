using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Data.Entities
{
    public class PizzaOrder
    {
        [Key]
        public int Id { get; set; }

        public string Size { get; set; }

        public double Cost { get; set; }

        public ICollection<Topping> Toppings { get; set; }

        public User Customer { get; set; }

        public PizzaOrder()
        {
            
        }

        public PizzaOrder(int id, string size, double cost, List<Topping> toppings, User customer)
        {
            Id = id;
            Size = size;
            Cost = cost;
            Toppings = toppings;
            Customer = customer;
        }
    }
}