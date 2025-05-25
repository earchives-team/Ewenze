﻿using Ewenze.Domain.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Application.Services.Users.Models
{
    public  class CreateUserValidator : AbstractValidator<User>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(u => u.UserName)
               .NotEmpty().WithMessage("{PropertyName} is required")
               .NotNull()
               .MaximumLength(60).WithMessage("{PropertyName} must be fewer than 60 charatecters");

            RuleFor(u => u.LastName)
               .NotEmpty().WithMessage("{PropertyName} is required")
               .NotNull()
               .MaximumLength(60).WithMessage("{PropertyName} must be fewer than 60 charatecters");

            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(60).WithMessage("{PropertyName} must be fewer than 60 charatecters");

            RuleFor(u => u.PhoneNumber)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(60).WithMessage("{PropertyName} must be fewer than 60 charatecters");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .EmailAddress().WithMessage("{PropertyName must be a valid email adress}");

            RuleFor(u => u.Password)
                .NotEmpty()
                .NotNull()
                .MaximumLength(5).WithMessage("Password must have 5 or more than characters");
        }
    }
}
