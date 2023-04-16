using Microsoft.EntityFrameworkCore;
using SkillSet.Domain;
using MediatR;
using System.Reflection;

namespace SkillSet.Infrastructure;

public class PersonSkillsContext : DbContext
{
    private readonly IMediator _mediator;
    public PersonSkillsContext(
        DbContextOptions<PersonSkillsContext> options,
        IMediator mediator)
        : base(options)
    {
        _mediator = mediator;
    }
    public DbSet<Person> People { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<SkillHistory> SkillsHistory { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

}