using Application.Common.Models;
using Application.People.Commands.CreatePerson;
using Microsoft.EntityFrameworkCore;
using SkillSet.Infrastructure;
using Moq;
using AutoMapper;
using Application.Common.Mappings;
using Xunit.Microsoft.DependencyInjection.ExampleTests.Fixtures;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.People.Commands;
public class CreatePersonCommandTests 
      : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly IDbContextFactory<PersonSkillsContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public CreatePersonCommandTests( CustomWebApplicationFactory<Program> factory)
    {
        _dbContextFactory = factory.Services.GetRequiredService<IDbContextFactory<PersonSkillsContext>>();
        _mapper = factory.Services.GetRequiredService<IMapper>();
    }

    [Fact]
    public async void Create_Person()
    {
        // Arrange
        var newSkillOne = new SkillDto
        {
            Name = "Песни",
            Level = 8
        };

        var newSkillTwo = new SkillDto
        {
            Name = "Танцы",
            Level = 10
        };

        var newPersonCommand = new CreatePersonCommand
        {
            Name = "Артур Пирожков",
            DisplayName = "Артур Пирожков",
            Skills = new[] { newSkillOne, newSkillTwo }
        };

        
        var handler = new CreatePersonCommandHandler(_dbContextFactory, _mapper);

        //Act
        var newPersonId = await handler.Handle(newPersonCommand, new CancellationToken());

        using var context = _dbContextFactory.CreateDbContext();
        var newPerson = context.People.Include(p => p.Skills).Where(p => p.Id ==newPersonId).FirstOrDefault();


        //Assert
        Assert.NotNull(newPerson);
        Assert.True(newPerson.Skills.Any());
        Assert.True(newPerson.Skills.Where(x => x.Name == "Танцы").FirstOrDefault() != null);
        Assert.True(newPerson.Skills.Where(x => x.Name == "Песни" && x.Level==8).FirstOrDefault() != null);
    }
}
