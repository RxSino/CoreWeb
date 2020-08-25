using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using MyWeb.Errors;
using MyWeb.Models;
using MyWeb.Services;
using MyWeb.ViewModels;
using MyWeb.ViewModels.User;

namespace MyWeb.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/login")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        //private readonly IStringLocalizer<Program> _localizer;

        private IUserService _userService;

        public AuthorizationController(IConfiguration configuration,
            //IStringLocalizer<Program> localizer,
            IUserService userService)
        {
            _configuration = configuration;
            //_localizer = localizer;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserVM>> Login(LoginDTO loginDTO)
        {
            //模型验证的第一种方案
            //if (!ModelState.IsValid)
            //{
            //    return new OkObjectResult(ModelState);
            //}

            var result = await _userService.login(loginDTO.Username, loginDTO.Password);
            if (result != null)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, result.Username)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: credentials);

                var response = new UserVM
                {
                    Username = result.Username,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };

                return Ok(response);
            }
            else
            {
                throw new MyException("UsernameOrPasswordError");
            }
        }

        [HttpPost("refresh")]
        public ActionResult<UserVM> Refresh(TokenDTO tokenDTO)
        {
            var response = new UserVM
            {
                Username = "admin",
                Token = "token token token"
            };

            return Ok(response);
        }
    }
}

/*
{
    "Password": {
        "rawValue": null,
        "attemptedValue": null,
        "errors": [
            {
                "exception": null,
                "errorMessage": "The Password field is required."
            }
        ],
        "validationState": 1,
        "isContainerNode": false,
        "children": null
    },
    "Username": {
        "rawValue": null,
        "attemptedValue": null,
        "errors": [
            {
                "exception": null,
                "errorMessage": "The Username field is required."
            }
        ],
        "validationState": 1,
        "isContainerNode": false,
        "children": null
    }
}
*/
