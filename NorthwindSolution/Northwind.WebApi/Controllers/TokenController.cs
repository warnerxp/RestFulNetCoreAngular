using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.IdentityModel.JsonWebTokens;
using Northwind.Models;
using Northwind.UnitOfWork;
using Northwind.WebApi.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.WebApi.Controllers
{
    [Route("api/token")]
    public class TokenController : Controller
    {
        private ITokenProvider _tokenProvider;
        private IUnitOfWork _unitOfWork;

        public TokenController(ITokenProvider tokenProvider, IUnitOfWork unitOfWork)
        {
            _tokenProvider = tokenProvider;
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public Authentication.JsonWebToken Post([FromBody] User userLogin)
        {
            var user = _unitOfWork.User.ValidateUser(userLogin.Email, userLogin.Password);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var token = new Authentication.JsonWebToken
                {
                    Access_Token = _tokenProvider.CreateToken(user, DateTime.UtcNow.AddHours(8)),
                    Expires_in = 480,//minutes
                    Token_Type = "bearer"
                    
                };

            return token;
        } 
    }
}
