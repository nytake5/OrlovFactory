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
        await DbContext.Users.AddAsync(user);
        await DbContext.SaveChangesAsync();
    }
    public async Task<bool> LoginUser(User user)
    {
        var existUser = DbContext.Users
            .FirstOrDefault(u => u.Login == user.Login
                                        && u.Password == user.Password);
        
        return await Task.FromResult(existUser != null);
    }
    
    public async Task<bool> TokenizeUser(string username, Guid token)
    {
        var existUser = await DbContext.Users
            .FirstOrDefaultAsync(u => u.Login == username);
        
        if (existUser == null)
        {
            return false;
        }
        existUser.Token = token;
        DbContext.Users.Update(existUser);
        var cnt = await DbContext.SaveChangesAsync();
        return cnt != 0;
    }

    public async Task<User> GetUserByLogin(string login)
    {
        var user = await DbContext.Users.FirstOrDefaultAsync(u => u.Login == login);

        return user;
    }
}