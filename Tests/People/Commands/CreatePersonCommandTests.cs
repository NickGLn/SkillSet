using Application.Common.Models;
using Application.People.Commands.CreatePerson;
using Microsoft.EntityFrameworkCore;
using SkillSet.Infrastructure;
using Moq;
using AutoMapper;
using Application.Common.Mappings;

namespace Tests.People.Commands;
public class CreatePersonCommandTests
{
    private static IMapper _mapper = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile(new PersonMappingProfile());
        cfg.AddProfile(new SkillMappingProfile());
    }).CreateMapper();

    private static DbContextOptions<PersonSkillsContext> _dbContextOptions = new DbContextOptionsBuilder<PersonSkillsContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

    private readonly IDbContextFactory<PersonSkillsContext> _dbContextFactory;

    public CreatePersonCommandTests()
    {
        var dbContextFactoryMock = new Mock<IDbContextFactory<PersonSkillsContext>>();
        dbContextFactoryMock.Setup(f => f.CreateDbContext()).Returns(new PersonSkillsContext(_dbContextOptions));

        _dbContextFactory = dbContextFactoryMock.Object;
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

        using var context = new PersonSkillsContext(_dbContextOptions);
        var newPerson = context.People.Include(p => p.Skills).Where(p => p.Id ==newPersonId).FirstOrDefault();


        //Assert
        Assert.NotNull(newPerson);
        Assert.True(newPerson.Skills.Any());
        Assert.True(newPerson.Skills.Where(x => x.Name == "Танцы").FirstOrDefault() != null);
        Assert.True(newPerson.Skills.Where(x => x.Name == "Песни" && x.Level==8).FirstOrDefault() != null);
    }
}
