using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.People.Commands.UpdatePerson
{
    public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator() 
        {
            RuleFor(person => person.Id)
                .NotNull();

            RuleFor(person => person.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(person => person.DisplayName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            RuleForEach(person => person.Skills).ChildRules(s =>
            {
                s.RuleFor(skill => skill.Name)
                    .NotNull()
                    .NotEmpty();

                s.RuleFor(skill => skill.Level)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .Must(value => value > 0 && value <= 10)
                    .WithMessage("Skill Level should be between 1 and 10");
            });
        }
    }
}
