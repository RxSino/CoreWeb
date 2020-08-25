using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWeb.Errors;
using MyWeb.Models;
using MyWeb.Requests;
using MyWeb.utils;
using MyWeb.Validators;
using MyWeb.ViewModels;
using MyWeb.ViewModels.User;

namespace MyWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IMapper _mapper;

        public TestController(ILogger<TestController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("content")]
        public ContentResult ReturnContentResult()
        {
            return Content("content result");
        }


        [HttpGet("string")]
        public String ReturnString()
        {
            return "Hallo";
        }

        [HttpGet("object")]
        public ActionResult<object> ReturnObject()
        {
            var todoItem = new TodoItem
            {
                Id = 1,
                Name = "hallo",
                IsComplete = false,
                Secret = "secret"
            };
            var resp = _mapper.Map<TodoItemDTO>(todoItem);
            return resp;
        }

        [HttpGet("get/{id}")]
        public ActionResult<object> TestHttpGet(int id)
        {
            if (id == -1)
            {
                throw new MyException("自定义异常");
            }

            if (id == -2)
            {
                throw new ArgumentException("异常-2");
            }

            return Ok(new { data = "hello, world" });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<UserVM> Login(LoginRequest request)
        {
            _logger.LogDebug("login start");

            //var validator = new LoginValidator();
            //ValidationResult result = validator.Validate(request);
            //if (!result.IsValid)
            //{
            //    _logger.LogError("error");
            //    IList<ValidationFailure> failures = result.Errors;
            //    return new OkObjectResult(failures);
            //}

            //if (!ModelState.IsValid)
            //{
            //    _logger.LogDebug("error");
            //    var resp = Responses.ValidationError();
            //    resp.Message = ModelState.GetFirstErrorMessage();
            //    return new OkObjectResult(resp);
            //}

            var response = new UserVM
            {
                Username = "admin",
                Token = "token token token"
            };

            return Ok(response);
        }


    }
}
