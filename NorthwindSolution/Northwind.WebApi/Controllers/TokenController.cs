using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Northwind.Models;
using Northwind.UnitOfWork;
using Northwind.WebApi.Authentication;
using Northwind.WebApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Northwind.WebApi.Controllers
{
    [Route("api/token")]
    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;
        //private ITokenProvider _tokenProvider;
        private IUnitOfWork _unitOfWork;

        //public TokenController(ITokenProvider tokenProvider, IUnitOfWork unitOfWork)
        public TokenController( IUnitOfWork unitOfWork)
        {
            //_tokenProvider = tokenProvider;
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        //public Authentication.JsonWebToken Post([FromBody] User userLogin)
        public Token Post([FromBody] User userLogin)
        {
            var user = _unitOfWork.User.ValidateUser(userLogin.Email, userLogin.Password);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var token = BuildToken(userLogin);

            Token tokenInfo = new Token()
            {
                CreatedDate = DateTime.Now,
                GuidToken = new Guid(token.Id),
                ValueToken = new JwtSecurityTokenHandler().WriteToken(token)
            };



            return tokenInfo;
        }

        private JwtSecurityToken BuildToken(User userLogin)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userLogin.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("SecretKey").Value));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("waasdhjsahdgy5454kkjsa125asamqp"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble("30"));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: expiration,
                signingCredentials: creds);
            return token;
        }
    }
}
