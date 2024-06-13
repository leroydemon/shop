using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ShopWebApi.AuthConfig
{
    public class AuthOptions
    {
        public const string ISSUER = "EgorServer";
        public const string AUDIENCE = "EgorClient";
        const string KEY = "SecretKeySecretKeySecretKeySecretKeySecretKeySecretKeySecretKey";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
