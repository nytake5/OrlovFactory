using Entities;

namespace DAL.Interfaces;

public interface IUserDao
{
    Task AddNewUser(User user);
    Task<bool> LoginUser(User user);
    Task<bool> TokenizeUser(string username, Guid token);
    Task<User> GetUserByLogin(string login);
}