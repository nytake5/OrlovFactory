using BLL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace BLL;

public class UserLogic : BaseLogic, IUserLogic
{
    
    public UserLogic(ILogger<UserLogic> logger) 
        : base(logger)
    {
        
    }
    
    public Task AddNewUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> LoginUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> TokenizeUser(User user)
    {
        throw new NotImplementedException();
    }
}