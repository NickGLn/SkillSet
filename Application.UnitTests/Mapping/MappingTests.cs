using AutoMapper;
using Application.Common.Mappings;
using System.Runtime.Serialization;
using SkillSet.Domain;
using Application.Common.Models;

namespace Application.UnitTests
{
    public class MappingTests
    {

        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<PersonMappingProfile>();
                config.AddProfile<SkillMappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void MappingConfigurationIsValid()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(Person), typeof(PersonDto))]
        [InlineData(typeof(PersonDto), typeof(Person))]
        [InlineData(typeof(Skill), typeof(SkillDto))]
        [InlineData(typeof(SkillDto), typeof(Skill))]
        public void MappingForEntitiesExists(Type source, Type destination)
        {
            var instance = GetInstanceOf(source);
            _mapper.Map(instance, source, destination);
        }

        private object GetInstanceOf(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) != null)
                return Activator.CreateInstance(type)!;

            // Return uninitializedObject, if public cunstructor of a given type is not found
            return FormatterServices.GetUninitializedObject(type);
        }
    }
}