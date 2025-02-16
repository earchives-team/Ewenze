using Ewenze.Domain.Repositories;
using FluentValidation;

namespace Ewenze.Application.Features.UserFeature.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserRepository userRepository; 

        public CreateUserCommandValidator(IUserRepository userRepository)
        {
            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(60).WithMessage("{PropertyName} must be fewer than 60 charatecters");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .EmailAddress().WithMessage("{PropertyName must be a valid email adress}");
            this.userRepository = userRepository;

        }
    }
}
