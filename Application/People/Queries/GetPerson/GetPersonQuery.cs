using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SkillSet.Infrastructure;

namespace Application.People.Queries.GetPerson
{
    public class GetPersonQuery : IRequest<PersonDto>
    {
        public long Id { get; set; }
        public GetPersonQuery(long Id)
        {
            this.Id = Id;
        }

    }

    internal class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, PersonDto>
    {
        private readonly IDbContextFactory<PersonSkillsContext> _contextFactory;
        private readonly IMapper _mapper;

        public GetPersonQueryHandler(IDbContextFactory<PersonSkillsContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<PersonDto> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();
            var person = await context.People.Where(x => x.Id == request.Id)
                                             .Include(s => s.Skills)
                                             .FirstOrDefaultAsync();

            return _mapper.Map<PersonDto>(person);
        }
    }

}