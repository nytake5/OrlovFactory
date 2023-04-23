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
        var existUser = await DbContext.Users
            .FirstOrDefaultAsync(u => u.Login == user.Login
                                        && u.Password == user.Password);
        return existUser != null;
    }

    public async Task<bool> TokenizeUser(User user)
    {
        var existUser = await DbContext.Users
            .FirstOrDefaultAsync(u => u.Login == user.Login);
        if (existUser == null)
        {
            return false;
        }

        DbContext.Users.Update(user);
        var cnt = await DbContext.SaveChangesAsync();
        return cnt != 0;
    }
}