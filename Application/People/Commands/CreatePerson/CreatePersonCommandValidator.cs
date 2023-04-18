using FluentValidation;

namespace Application.People.Commands.CreatePerson
{
    public class CreatePersonCommandValidator: AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(person => person.Name)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(person => person.DisplayName)
                .MaximumLength(200)
                .NotEmpty()
                .WithName("Display Name");
        }
    }
}
