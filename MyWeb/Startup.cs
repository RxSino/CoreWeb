using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MyWeb.Models;
using MyWeb.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
using MyWeb.ViewModels;
using MyWeb.utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using IdentityModel;
using Microsoft.IdentityModel.Logging;
using System.Text;
using MyWeb.Repositories;
using MyWeb.Services;
using AutoMapper;
using FluentValidation.AspNetCore;
using MyWeb.Validators;

namespace MyWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("ToDoList"));

            services.AddControllers();

            services.AddSwaggerGen();

            services.AddMvc(options =>
            {
                options.Filters.Add<MyExceptionFilter>();

                //模型验证的第二种方案
                //options.Filters.Add(typeof(MyValidationFilter));

                //需要使用默认的模型验证
                //options.MaxModelValidationErrors = 1;
            });

            //JSON
            //services.AddMvc().AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            //    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            //});

            //FluentValidation
            services.AddMvc().AddFluentValidation(cfg =>
            {
                cfg.ValidatorOptions.CascadeMode = FluentValidation.CascadeMode.Stop;
                cfg.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                cfg.RegisterValidatorsFromAssemblyContaining(typeof(LoginValidator));
            });


            services.Configure<ApiBehaviorOptions>(options =>
            {
                //遗留问题
                //模型验证的顺序问题
                //模型验证的翻译问题

                //关闭模型验证
                //options.SuppressModelStateInvalidFilter = true;

                //模型验证的第三种方案
                //or
                //FluentValidation
                options.InvalidModelStateResponseFactory = context =>
                {
                    var response = Responses.ValidationError();

                    //获取第一条错误消息
                    //response.Message = context.ModelState.GetFirstErrorMessage();

                    //异常列表
                    var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).ToList();
                    response.Message = String.Join(",", errors.ToArray());

                    return new OkObjectResult(response);
                };
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = TokenValidationParameters();
                    options.Events = JwtBearerEvents();
                });


            // AutoMapper
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(AutoMapperProfile));
            });
            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);


            // Dapper
            services.AddSingleton<IDapperConfiguration>(new DapperConfiguration
            {
                WriteConnectionString = Configuration.GetConnectionString("WriteConnection"),
                ReadConnectionStrign = Configuration.GetConnectionString("ReadConnection")
            });


            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserServiceImpl>();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //中间件,返回200
            //app.UseMiddleware<MyExceptionMiddleware>();

            //异常处理器，默认返回500
            //app.UseExceptionHandler(appbuilder =>
            //{
            //    appbuilder.Use(MyExceptionHandler);
            //});

            IdentityModelEventSource.ShowPII = true;

            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public async Task MyExceptionHandler(HttpContext httpContext, Func<Task> next)
        {
            httpContext.Response.StatusCode = StatusCodes.Status200OK;
            httpContext.Response.ContentType = "application/json";

            var response = Responses.Error();
            var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionDetails?.Error;

            if (exception != null)
            {
                if (exception is MyException ex)
                {
                    response = new RawResponse
                    {
                        Code = ex.Code,
                        Message = ex.Value,
                        Data = ex.ToString()
                    };
                }

                var stream = httpContext.Response.Body;
                await JsonSerializer.SerializeAsync(stream, response, JsonSerializerOptions());
            }
        }

        private JsonSerializerOptions JsonSerializerOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }

        private TokenValidationParameters TokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = Configuration["Jwt:Issuer"],
                ValidAudience = Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Jwt:SecurityKey"])),
            };
        }

        private JwtBearerEvents JwtBearerEvents()
        {
            return new JwtBearerEvents
            {
                OnChallenge = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    context.Response.ContentType = "application/json";
                    var response = Responses.TokenError();
                    var stream = context.Response.Body;
                    JsonSerializer.SerializeAsync(stream, response, JsonSerializerOptions());
                    return Task.CompletedTask;
                }
            };
        }
    }
}
