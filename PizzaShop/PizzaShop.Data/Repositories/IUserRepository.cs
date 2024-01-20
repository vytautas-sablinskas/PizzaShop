using PizzaShop.Data.Entities;

namespace PizzaShop.Data.Repositories
{
    public interface IUserRepository
    {
        User AddUser(string userName);
        User? GetUser(string username);
    }
}