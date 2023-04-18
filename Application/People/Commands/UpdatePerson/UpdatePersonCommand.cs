using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillSet.Domain;
using SkillSet.Infrastructure;

namespace Application.People.Commands.UpdatePerson
{
    public class UpdatePersonCommand : IRequest<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public IEnumerable<UpdateSkillDto> Skills { get; set; }
    }

    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, long>
    {
        private readonly IDbContextFactory<PersonSkillsContext> _contextFactory;
        private readonly IMapper _mapper;

        public UpdatePersonCommandHandler(IDbContextFactory<PersonSkillsContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<long> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();
            var person = await context.People.Include(p => p.Skills)
                                             .Where(p => p.Id == request.Id)
                                             .FirstOrDefaultAsync();

            if (person == null)
            {
                return 0;
            }

            person.Name = request.Name;
            person.DisplayName = request.DisplayName;
            person.Skills = request.Skills.Select(s => _mapper.Map<Skill>(s)).ToList();

            await context.SaveChangesAsync(cancellationToken);

            return person.Id;
        }
    }
}
