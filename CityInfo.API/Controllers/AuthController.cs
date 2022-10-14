using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public ActionResult<string> Autenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            var user = ValidateUserCredentials(authenticationRequestBody.UserName,
                authenticationRequestBody.Password);

            if (user is null) return Unauthorized();

            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Auth:Key"]));

            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
            claimsForToken.Add(new Claim("given_name", user.FirstName));
            claimsForToken.Add(new Claim("family_name", user.LastName));
            claimsForToken.Add(new Claim("city", user.City));

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Auth:Issuer"],
                _configuration["Auth:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var tokenReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return Ok(tokenReturn);
        }

        private CityInfoUser ValidateUserCredentials(string? userName, string? password)
        {
            //apenas a nivel didatico, as credencias "na vida real" estarao contidas em uma base de dados

            return new CityInfoUser(1, userName ?? "", "mateus",
                "castellar", "São Paulo");
        }
    }

    public class AuthenticationRequestBody
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    public class CityInfoUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }

        public CityInfoUser(int userId, string userName, string firstName, string lastName, string city)
        {
            UserId = userId;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            City = city;
        }
    }
}
