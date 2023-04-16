using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkillSet.Application.Models;
using SkillSet.Domain;
using SkillSet.Infrastructure;

namespace SkillSet.Application.Commands
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
            //person.Skills = request.Skills.Select( s => _mapper.Map<Skill>(s)).ToList();

            // Incoming skill ID's
            var skillIds = request.Skills.Select(s => s.Id);

            // Skills to Delete
            var deleteSkills = context.Skills.Where(s => !skillIds.Contains(s.Id) && s.Person.Id == request.Id);

            if (deleteSkills.Any())
            {
                context.RemoveRange(deleteSkills);
            }

            // Skill to update or insert
            foreach (var requestSkill in request.Skills)
            {
                var skill = person.Skills.Where(s => s.Id == requestSkill.Id).FirstOrDefault();

                // Update if a skill already exists
                if (skill != null)
                {
                    skill.Name = requestSkill.Name;
                    skill.Level = requestSkill.Level;
                }
                // Insert if it's a new skill
                else
                {
                    var addSkill = _mapper.Map<Skill>(requestSkill);
                    addSkill.PersonId = person.Id;

                    await context.AddAsync(addSkill);
                }
            }

            await context.SaveChangesAsync(cancellationToken);

            return person.Id;
        }
    }
}
