using Microsoft.EntityFrameworkCore;
using SkillSet.Domain;
using MediatR;
using System.Reflection;

namespace SkillSet.Infrastructure;

public class PersonSkillsContext : DbContext
{
    public PersonSkillsContext(
        DbContextOptions<PersonSkillsContext> options)
        : base(options) { }

    public DbSet<Person> People { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<SkillHistory> SkillsHistory { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

}