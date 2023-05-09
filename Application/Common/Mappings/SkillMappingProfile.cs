using Application.Common.Models;
using Application.People.Commands.UpdatePerson;
using AutoMapper;
using SkillSet.Domain;

namespace Application.Common.Mappings
{
    public class SkillMappingProfile : Profile
    {
        public SkillMappingProfile()
        {
            CreateMap<Skill, SkillDto>().ReverseMap();
            CreateMap<UpdateSkillDto, Skill>()
                .ForMember(d => d.PersonId, s => s.Ignore())
                .ForMember(d => d.Person, s => s.Ignore());
        }
    }
}
