using FluentValidation;

namespace Application.People.Queries.GetPerson
{
    public class GetPersonQueryValidator: AbstractValidator<GetPersonQuery>
    {
        public GetPersonQueryValidator()
        {
            RuleFor(person => person.Id).NotEmpty();
        }
    }
}
