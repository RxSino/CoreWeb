using System;
using FluentValidation;
using MyWeb.Requests;

namespace MyWeb.Validators
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            //When a rule fails, validation is immediately halted.
            //CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Username).NotEmpty().WithMessage("用户名错误");
            RuleFor(x => x.Password).MinimumLength(6).WithMessage("密码错误");
        }
    }
}
