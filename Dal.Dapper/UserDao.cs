using DAL.Interfaces;
using Dapper;
using Entities;

namespace Dal.Dapper;

public class UserDao : BaseDapperDao, IUserDao
{
    public async Task AddNewUser(User user)
    {   
        await using var connection = GetConnection();
        const string query = 
            $""""
            INSERT INTO public."Users"("Login", "Password", "EmployeeId") 
                VALUES (@login, @password, @id);
            """";
        
        var parameters = new
        {
            login = user.Login,
            password = user.Password,
            id = user.EmployeeId
        };
        await connection.ExecuteAsync(query, parameters);
    }

    public async Task<bool> LoginUser(User user)
    {
        await using var connection = GetConnection();
        const string query = 
            $""""
                SELECT 1 FROM public."Users"
                WHERE "Login" = @login AND "Password" = @password
            """";
        
        var parameters = new
        {
            login = user.Login,
            password = user.Password
        };
        var result = await connection.QueryFirstOrDefaultAsync<int>(query, parameters);
        return result == 1;
    }
    
    public async Task<bool> TokenizeUser(string username, Guid token)
    {
        await using var connection = GetConnection();
        const string query = 
            $""""""
                UPDATE public."Users" 
                    SET "Token" = @token WHERE "Login" = @login 
                    RETURNING *;
            """""";
        
        var parameters = new
        {
            login = username,
            token   
        };
        
        var user = await connection.QueryFirstOrDefaultAsync<User>(query, parameters);
        return user != null;
    }

    public async Task<User> GetUserByLogin(string login)
    {
        await using var connection = GetConnection();
        const string query = 
            $""""
                SELECT * FROM public."Users"
                WHERE "Login" = @login
            """";
        
        var parameters = new
        {
            login = login
        };
        
        var result = await connection.QueryFirstOrDefaultAsync<User>(query, parameters);
        return result;
    }
}