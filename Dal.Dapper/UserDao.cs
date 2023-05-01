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
                INSERT INTO "FactoryUsers"("Login", "Password", "EmployeeId") 
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
                SELECT * FROM "FactoryUsers"
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
    
    public async Task<bool> TokenizeUser(string username, Guid token, long chatId)
    {
        await using var connection = GetConnection();
        const string query = 
            $""""""
                UPDATE "FactoryUsers"
                    SET "Token" = @token,
                        "ChatId" = @chatId
                     WHERE "Login" = @login 
                    RETURNING *;
            """""";
        
        var parameters = new
        {
            login = username,
            token,
            chatId
        };
        
        var user = await connection.QueryFirstOrDefaultAsync<User>(query, parameters);
        return user != null;
    }

    public async Task<User> GetUserByLogin(string login)
    {
        await using var connection = GetConnection();
        const string query = 
            $""""
                SELECT * FROM "FactoryUsers"
                WHERE "Login" = @login
            """";
        
        var parameters = new
        {
            login = login
        };
        
        var result = await connection.QueryFirstOrDefaultAsync<User>(query, parameters);
        return result;
    }
    
    public IAsyncEnumerable<User> GetAllUsers()
    {
        const string QueryString = "SELECT * FROM FactoryUsers";
        var result = GetEnumerable<User>(QueryString, null);

        return result;
    }

    public async Task<bool> LoginByToken(User user)
    {
        await using var connection = GetConnection();
        const string query = 
            $""""
                SELECT * FROM "FactoryUsers"
                WHERE "Login" = @login AND "Token" = @token
            """";
        
        var parameters = new
        {
            login = user.Login,
            token = user.Token
        };
        var result = await connection.QueryFirstOrDefaultAsync<int>(query, parameters);
        return result == 1;
    }
}