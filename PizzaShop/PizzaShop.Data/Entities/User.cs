using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Data.Entities
{
    public class User
    {
        [Key]

        public string UserName { get; set; }

        public User() {}

        public User(string username)
        {
            UserName = username;
        }
    }
}
