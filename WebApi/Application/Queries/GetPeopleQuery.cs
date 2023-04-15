using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillSet.Application.Models;
using SkillSet.Domain;
using SkillSet.Infrastructure;

namespace SkillSet.Application.Queries
{
    public class GetPeopleQuery : IRequest<IEnumerable<PersonDto>> { }

    internal class GetPeopleQueryHandler : IRequestHandler<GetPeopleQuery, IEnumerable<PersonDto>>
    {
        private readonly IDbContextFactory<PersonSkillsContext> _contextFactory;
        private readonly IMapper _mapper;

        public GetPeopleQueryHandler(IDbContextFactory<PersonSkillsContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonDto>> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.People.Include(s => s.Skills)
                                       .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                                       .ToListAsync();
        }
    }
}