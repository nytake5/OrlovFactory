using BLL.Interfaces;
using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace BLL;

public class UserLogic : BaseLogic, IUserLogic
{
    private readonly IUserDao _dao;
    
    public UserLogic(
        ILogger<UserLogic> logger,
        IUserDao dao)   
        : base(logger)
    {
        _dao = dao;
    }
    
    public async Task AddNewUser(User user)
    {
        await _dao.AddNewUser(user);
    }

    public async Task<bool> LoginUser(User user)
    {
        return await _dao.LoginUser(user);
    }

    public async Task<bool> TokenizeUser(User user)
    {        
        return await _dao.TokenizeUser(user);
    }
}