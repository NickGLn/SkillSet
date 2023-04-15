using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillSet.Domain;
using SkillSet.Infrastructure;

namespace SkillSet.Application.Commands
{
    public class DeletePersonCommand: IRequest<long>
    {
        public long Id;

        public DeletePersonCommand(long Id)
        {
            this.Id = Id;
        }
    }
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, long>
    {
        private readonly IDbContextFactory<PersonSkillsContext> _contextFactory;
        private readonly IMapper _mapper;

        public DeletePersonCommandHandler(IDbContextFactory<PersonSkillsContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<long> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();
            var person = await context.People.Include(p => p.Skills)
                                             .Where(p => p.Id == request.Id)
                                             .FirstOrDefaultAsync();

            if (person == null)
            {
                return 0;
            }

            context.Remove(person);

            await context.SaveChangesAsync(cancellationToken);

            return request.Id;
        }
    }
}