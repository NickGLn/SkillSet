using AutoMapper;
using SkillSet.Application.Commands;
using SkillSet.Application.Models;
using SkillSet.Domain;

namespace SkillSet.Application.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Skill, SkillDto>().ReverseMap();
            CreateMap<UpdateSkillDto, Skill>();
            CreateMap<CreatePersonCommand, Person>(); 
            CreateMap<UpdatePersonCommand, Person>();
        }
    }
}
