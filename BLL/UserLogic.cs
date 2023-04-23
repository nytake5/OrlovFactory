using System.Text.Json;
using BLL.Interfaces;
using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace BLL;

public class UserLogic : BaseLogic, IUserLogic
{
    private readonly IUserDao _dao;
    private readonly IDistributedCache _distributedCache;
    
    public UserLogic(
        ILogger<UserLogic> logger,
        IUserDao dao,
        IDistributedCache distributedCache)   
        : base(logger)
    {
        _dao = dao;
        _distributedCache = distributedCache;
    }
    
    public async Task AddNewUser(User user)
    {
        await _dao.AddNewUser(user);
    }

    public async Task<bool> LoginUser(User user)
    {
        return await _dao.LoginUser(user);
    }

    public async Task<bool> TokenizeUser(string username, Guid token)
    {        
        return await _dao.TokenizeUser(username, token);
    }

    public async Task<User> GetUserByLogin(string login, long chatId)
    {
        var user =  await _dao.GetUserByLogin(login);

        var userString = JsonSerializer.Serialize(user);

        await _distributedCache.SetStringAsync(chatId.ToString(), userString);
        
        return user;
    }

    public async Task<User> GetUserByChatId(long chatId)
    {
        var userString = await _distributedCache.GetStringAsync(chatId.ToString());
        if (!string.IsNullOrWhiteSpace(userString))
        {
            var user = JsonSerializer.Deserialize<User>(userString);

            return user;
        }
        return null;
    }
}