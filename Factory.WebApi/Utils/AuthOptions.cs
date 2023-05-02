using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Factory.WebApi.Utils;

public static class AuthOptions
{
    public const string Issuer = "MyService";
    public const string Audience = "MyClient";
    const string Key = "mysupersecret_secretkey!123";
    public static readonly TimeSpan Lifetime = TimeSpan.FromMinutes(60);
    
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}