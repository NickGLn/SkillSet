using FluentValidation;

namespace Application.People.Commands.CreatePerson
{
    public class CreatePersonCommandValidator: AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(person => person.Name)
                .NotEmpty()
                .WithMessage("Please specify a value for the Name field");

            RuleFor(person => person.DisplayName)
                .NotEmpty()
                .WithMessage("Please specify a value for the DisplayName field");
        }
    }
}
