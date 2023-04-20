using DAL.Interfaces;
using Entities;

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

    public Task<bool> LoginUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> TokenizeUser(User user)
    {
        throw new NotImplementedException();
    }
}