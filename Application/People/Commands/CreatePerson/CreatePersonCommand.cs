using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillSet.Domain;
using SkillSet.Infrastructure;

namespace Application.People.Commands.CreatePerson
{
    public class CreatePersonCommand : IRequest<long>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public IEnumerable<SkillDto> Skills { get; set; }
    }

    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, long>
    {
        private readonly IDbContextFactory<PersonSkillsContext> _contextFactory;
        private readonly IMapper _mapper;

        public CreatePersonCommandHandler(IDbContextFactory<PersonSkillsContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<long> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();
            var newPerson = _mapper.Map<Person>(request);

            await context.AddAsync(newPerson, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return newPerson.Id;
        }
    }
}
