using Application.Common.Models;
using Application.People.Commands.CreatePerson;
using Application.People.Commands.UpdatePerson;
using AutoMapper;
using SkillSet.Domain;

namespace Application.Common.Mappings
{
    public class PersonMappingProfile : Profile
    {
        public PersonMappingProfile()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<CreatePersonCommand, Person>().ForMember(d => d.Id, s => s.Ignore());
            CreateMap<UpdatePersonCommand, Person>();
        }
    }
}
