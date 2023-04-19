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
            RuleFor(person => person.Name).NotEmpty().WithMessage("Person Name shouldn't be empty");
            RuleFor(person => person.DisplayName).NotEmpty().WithMessage("Person DisplayName shouldn't be empty");

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
