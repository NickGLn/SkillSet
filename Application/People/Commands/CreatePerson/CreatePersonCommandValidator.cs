using FluentValidation;

namespace Application.People.Commands.CreatePerson
{
    public class CreatePersonCommandValidator: AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(person => person.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(person => person.DisplayName)
                .NotEmpty()
                .MaximumLength(100);

            RuleForEach(person => person.Skills).ChildRules(s =>
            {
                s.RuleFor(skill => skill.Name)
                    .NotEmpty()
                    .WithMessage("Skill should have a name");

                s.RuleFor(skill => skill.Level)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .Must(value => value > 0 && value <= 10)
                    .WithMessage("Skill Level should be between 1 and 10");
            });
        }
    }
}
