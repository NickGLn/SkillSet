using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using SkillSet.Domain;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using Microsoft.Extensions.Configuration;

namespace SkillSet.Infrastructure;

public class PersonSkillsContext : DbContext
{
    public DbSet<Person> People { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<SkillHistory> SkillsHistory { get; set; }

    protected readonly IConfiguration _configuration;

    public PersonSkillsContext (IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(_configuration.GetConnectionString("PersonSkillsDatabase"));
}
