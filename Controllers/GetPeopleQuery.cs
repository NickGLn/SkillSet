using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillSet.Domain;
using SkillSet.Infrastructure;

namespace SkillSet.Controllers
{
    public class GetPeopleQuery : IRequest<List<Person>>
    {
        public int Take { get; set; }
        public int Skip { get; set; }
    }

    internal class GetPeopleQueryHandler : IRequestHandler<GetPeopleQuery, List<Person>>
    {
        private readonly IDbContextFactory<PersonSkillsContext> _contextFactory;
        public GetPeopleQueryHandler(IDbContextFactory<PersonSkillsContext> contextFactory) 
            => _contextFactory = contextFactory;

        public async Task<List<Person>> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.People.Include(s=>s.Skills).ToListAsync();
        }
    }

    public class PersonDto
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public List<SkillDto> Skills { get; set; }
    }

    public class SkillDto
    {
        public string Name { get; set; }
        public byte Level { get; set; }
    }
}