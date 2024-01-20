using PizzaShop.Data.Database;
using PizzaShop.Data.Entities;

namespace PizzaShop.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PizzaShopContext _context;

        public UserRepository(PizzaShopContext context)
        {
            _context = context;
        }

        public User AddUser(string userName)
        {
            var user = _context.Users.Add(new User(userName))
                                 .Entity;
            _context.SaveChanges();

            return user;
        }

        public User? GetUser(string username)
        {
            return _context.Users.FirstOrDefault(user => string.Equals(username, user.UserName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
