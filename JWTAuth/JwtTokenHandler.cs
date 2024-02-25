using JwtAuthHandler;
using JwtAuthHandler.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace JWTAuth
{
    public class JwtTokenHandler
    {
        //These values need to fetch from config in actual scenario
        public const string SECURITY_KEY = "HASH_KEY";
        private const int VALID_MINUTES = 90;
        private List<UserAccount> _accounts = new();
        private IConfiguration _configuration;


        public JwtTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            _accounts = new List<UserAccount>
            {
                new UserAccount() { UsernaName = "admin", Password = "admin123", Role = "Admin" },
                new UserAccount() { UsernaName = "user", Password = "user123", Role = "User" }
            };
        }

        public AuthenticateResponse? GenerateJwtToken(AuthenticateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.UserName))
                return null;

            var userAccount = _accounts.Where(x => x.UsernaName == request.UserName && x.Password == request.Password).FirstOrDefault();
            if(userAccount == null) return null;

            //var s = _configuration.GetSection("AppSettings");

            var tokenExpiery = DateTime.Now.AddMinutes(VALID_MINUTES);
            var tokenKey = Encoding.ASCII.GetBytes(SECURITY_KEY);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, request.UserName),
                new Claim(ClaimTypes.Role, userAccount.Role),
            });

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescripter = new SecurityTokenDescriptor()
            {
                Subject = claimsIdentity,
                Expires = tokenExpiery,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescripter);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticateResponse() {
                Token = token,
                UserName = request.UserName,
                ExpiredIn = VALID_MINUTES
            };
            
        }

        public bool ValidateToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            SecurityToken validatedToken;
            IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
            return true;
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = "Sample",
                ValidAudience = "Sample",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECURITY_KEY)) // The same key as the one that generate the token
            };
        }
    }
}
