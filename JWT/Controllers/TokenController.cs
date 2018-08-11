using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JWT.Helpers;
using JWT.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private IConfiguration _config;

        public TokenController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginModel login)
        {
            //  applied by me source https://auth0.com/blog/securing-asp-dot-net-core-2-applications-with-jwts/ 
            IActionResult response = Unauthorized();
            var user = Authenticate(login);
            // await _db.Login(userForLoginDto.Username.ToLower() , userForLoginDto.Password);
            if (user != null)
            {
                var tokenString = Token.BuildToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private UserModel Authenticate(LoginModel login)
        {
            UserModel user = null;

            if (login.Username == "mario" && login.Password == "secret")
            {
                user = new UserModel { Name = "Mario Rossi", Email = "mario.rossi@domain.com" };
            }
            return user;
        }

    }
}

