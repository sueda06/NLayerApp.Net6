using FluentValidation;
using NLayer.Core.DTOs;
using PointoFrameworks.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Validations
{
    public class RegisterModelDtoValidator : AbstractValidator<RegisterModelDto>
    {
        public RegisterModelDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.FirstName).Must(NameIsValid).WithMessage("{PropertyName} is not valid");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.LastName).Must(NameIsValid).WithMessage("{PropertyName} is not valid");

            RuleFor(x => x.UserName).NotNull().WithMessage("{PropertyName} is required");
            RuleFor(x => x.UserName).Must(NameIsValid).WithMessage("{PropertyName} is not valid");

            RuleFor(x => x.Email).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Email).Must(EmailIsValid).WithMessage("{PropertyName} is not valid");

            RuleFor(x => x.Password).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Password).Must(PasswordIsValid).WithMessage("{PropertyName} is not valid");
        }
        private bool NameIsValid(string? name) => NameValidator.IsValid(name!);
        private bool EmailIsValid(string? email) => EmailValidator.IsValid(email!);
        private bool PasswordIsValid(string? password) => PasswordValidator.IsValid(password!);
    }
}
