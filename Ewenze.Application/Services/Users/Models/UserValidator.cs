using Ewenze.Domain.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Application.Services.Users.Models
{
    public  class CreateUserValidator : AbstractValidator<UserApplicationModel>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(u => u.Name)
               .NotEmpty().WithMessage("{PropertyName} is required")
               .NotNull()
               .MaximumLength(60).WithMessage("{PropertyName} must be fewer than 60 charatecters");

            //RuleFor(u => u.PhoneNumber)
            //    .NotEmpty().WithMessage("{PropertyName} is required")
            //    .NotNull()
            //    .MaximumLength(60).WithMessage("{PropertyName} must be fewer than 60 charatecters");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .EmailAddress().WithMessage("{PropertyName must be a valid email adress}");

            RuleFor(u => u.Password)
                .NotEmpty()
                .NotNull();
        }
    }
}
