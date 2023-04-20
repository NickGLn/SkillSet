using FluentValidation;

namespace Application.People.Commands.CreatePerson
{
    public class CreatePersonCommandValidator: AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleForEach(person => person.Skills).ChildRules(s =>
            {
                s.RuleFor(skill => skill.Name)
                    .NotEmpty()
                    .WithMessage("Skill should have a name");

                s.RuleFor(skill => skill.Level)
                    .NotEmpty()
                    .Must(value => value > 0 && value <= 10)
                    .WithMessage("Skill Level should be between 1 and 10");
            });
        }
    }
}
