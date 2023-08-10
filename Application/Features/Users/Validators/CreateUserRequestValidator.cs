using Application.Features.Users.Commands;
using FluentValidation;

namespace Application.Features.Users.Validators
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(request => request.UserRequest)
                .SetValidator(new NewUserValidator());
        }
    }
}
