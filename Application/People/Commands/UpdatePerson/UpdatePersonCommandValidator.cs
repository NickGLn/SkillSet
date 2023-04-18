using FluentValidation;
using FluentValidation.AspNetCore;

namespace Application.People.Commands.UpdatePerson
{
    public class UpdatePersonCommandValidator: AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator()
        {
            RuleFor(p => p.Name)
                .MaximumLength(200)
                .NotEmpty();

            RuleForEach(person => person.Skills)
                .ChildRules(skill =>
                {
                    skill.RuleFor(s => s.Name)
                            .NotEmpty();

                    skill.RuleFor(s => s.Level)
                            .NotEmpty()
                            .Must(val => val > 0 && val <= 10)
                            .WithMessage("Level value must be between 1 and 10");
                });
        }
    }
}