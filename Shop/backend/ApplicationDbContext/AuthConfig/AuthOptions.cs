using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ShopWebApi.AuthConfig
{
    public class AuthOptions
    {
        // move all settings to appsettings.json
        public const string ISSUER = "EgorServer";
        public const string AUDIENCE = "EgorClient";
        const string KEY = "SecretKeySecretKeySecretKeySecretKeySecretKeySecretKeySecretKey";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
