//using Microsoft.AspNetCore.Identity;
//using Microsoft.IdentityModel.Tokens;
//using ShopWebApi.AuthConfig;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;

//namespace Infrastucture.Services
//{
//    public class TokenGeneratorService
//    {
//        public string GenerateJwtToken(IdentityUser user)
//        {
//            List<Claim> claims = new List<Claim>
//            {
//                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//            };
//            if (user.Email == "Admin")
//            {
//                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
//            }
//            else
//            {
//                claims.Add(new Claim(ClaimTypes.Role, "User"));
//            }
//            var key = AuthOptions.GetSymmetricSecurityKey();
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var jwt = new JwtSecurityToken(
//            issuer: AuthOptions.ISSUER,
//            audience: AuthOptions.AUDIENCE,
//            claims: claims,
//            expires: DateTime.Now.AddMinutes(60),
//            signingCredentials: creds);

//            return new JwtSecurityTokenHandler().WriteToken(jwt);
//        }
//    }
//}

// Move everything regarding authorization in new project called Authorization
