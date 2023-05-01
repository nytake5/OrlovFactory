using DAL.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class UserDao : BaseDao, IUserDao
{
    public UserDao(
        FactoryContext dbContext)
        : base(dbContext)
    {
    }

    public async Task AddNewUser(User user)
    {
        await DbContext.FactoryUsers.AddAsync(user);
        await DbContext.SaveChangesAsync();
    }

    public async Task<bool> LoginUser(User user)
    {
        var existUser = DbContext.FactoryUsers
            .FirstOrDefault(u => u.Login == user.Login
                                 && u.Password == user.Password);

        return await Task.FromResult(existUser != null);
    }

    public async Task<bool> LoginByToken(User user)
    {
        var existUser = await DbContext.FactoryUsers
            .FirstOrDefaultAsync(u => u.Login == user.Login
                                 && u.Token == user.Token);

        return existUser != null;
    }

    public async Task<bool> TokenizeUser(string username, Guid token, long chatId)
    {
        var existUser = await DbContext.FactoryUsers
            .FirstOrDefaultAsync(u => u.Login == username);
        
        if (existUser == null)
        {
            return false;
        }
        existUser.Token = token;
        existUser.ChatId = chatId;
        DbContext.FactoryUsers.Update(existUser);
        var cnt = await DbContext.SaveChangesAsync();
        return cnt != 0;
    }

    public async Task<User> GetUserByLogin(string login)
    {
        var user = await DbContext.FactoryUsers.FirstOrDefaultAsync(u => u.Login == login);

        return user;
    }

    public async IAsyncEnumerable<string> GetAllUsernames()
    {
        var result = DbContext.FactoryUsers.Select(user => user.Login);

        foreach (var login in result)
        {
            yield return login;
        }
        await Task.CompletedTask;
    }

    public async IAsyncEnumerable<User> GetAllUsers()
    {
        var result = DbContext.FactoryUsers;

        foreach (var user in result)
        {
            yield return user;
        }
        await Task.CompletedTask;
    }
}