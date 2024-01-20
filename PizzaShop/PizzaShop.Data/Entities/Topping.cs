using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Data.Entities
{
    public class Topping
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public Topping()
        {
            
        }

        public Topping(string name)
        {
            Name = name;
        }
    }
}