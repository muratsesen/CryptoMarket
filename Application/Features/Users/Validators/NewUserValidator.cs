using Application.Models;
using FluentValidation;

namespace Application.Features.Users.Validators
{
    public class NewUserValidator : AbstractValidator<NewUser>
    {
        public NewUserValidator() 
        {
            RuleFor(np => np.Name)
                .NotEmpty().WithMessage("User name is required.")
                .MaximumLength(15).WithMessage("Name should not exceed 15 charactors.");
        }
    }
}
