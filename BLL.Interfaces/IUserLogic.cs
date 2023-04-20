using Entities;

namespace BLL.Interfaces;

public interface IUserLogic
{
    Task AddNewUser(User user);

    Task<bool> LoginUser(User user);

    Task<bool> TokenizeUser(User user);
}