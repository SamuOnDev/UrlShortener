using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UrlShortenerApiBackend.Models.DataModels;

namespace UrlShortenerApiBackend.Helpers
{
    public static class JwtHelpers
    {
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccount, Guid Id)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", userAccount.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccount.UserName),
                new Claim(ClaimTypes.Email, userAccount.EmailId),
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt")),
            };

            if (userAccount.UserRole == Role.Administrator)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }
            else if (userAccount.UserRole == Role.User)
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }

            return claims;
        }

        public static IEnumerable<Claim> GetClaims(this UserTokens userAccount, out Guid Id)
        {
            Id = Guid.NewGuid();
            return GetClaims(userAccount, Id);
        }

        public static UserTokens GenTokenKey(UserTokens model, JwtSettings jwtSettings)
        {
            try
            {
                var userToken = new UserTokens();
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model));
                }

                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);

                Guid Id;

                DateTime expireTime = DateTime.UtcNow.AddDays(1);

                userToken.Validity = expireTime.TimeOfDay;

                var jwToken = new JwtSecurityToken(

                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudicence,
                    claims: GetClaims(model, out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256));

                userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwToken);
                userToken.UserName = model.UserName;
                userToken.Id = model.Id;
                userToken.GuidId = model.GuidId;
                userToken.UserRole = model.UserRole;

                return userToken;
            }
            catch (Exception exception)
            {
                throw new Exception("Error Generating the JWT", exception);
            }
        }
    }
}
