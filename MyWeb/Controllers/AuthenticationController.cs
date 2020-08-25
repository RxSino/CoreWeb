using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Models;
using MyWeb.ViewModels.User;

namespace MyWeb.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {

        public AuthenticationController()
        {
        }

        [HttpGet]
        public ActionResult<UserVM> GetInfo()
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
