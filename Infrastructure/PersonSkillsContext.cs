using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using SkillSet.Domain;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MediatR;

namespace SkillSet.Infrastructure;

public class PersonSkillsContext : DbContext
{
    private readonly IMediator _mediator;

    public PersonSkillsContext(
        DbContextOptions<PersonSkillsContext> options,
        IMediator mediator)
        :base(options)
    {
        _mediator = mediator;
    }
    public DbSet<Person> People { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<SkillHistory> SkillsHistory { get; set; }
}
