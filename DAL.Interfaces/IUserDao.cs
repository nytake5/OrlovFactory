using Entities;

namespace DAL.Interfaces;

public interface IUserDao
{
    Task AddNewUser(User user);
    Task<bool> LoginUser(User user);
    Task<bool> TokenizeUser(string username, Guid token, long chatId);
    Task<User> GetUserByLogin(string login);
    IAsyncEnumerable<User> GetAllUsers();
    Task<bool> LoginByToken(User user);
}