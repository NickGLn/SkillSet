using Microsoft.EntityFrameworkCore;
using SkillSet.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Register DbContext
builder.Services.AddDbContextFactory<PersonSkillsContext>(
    options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("PersonSkillsDatabase")),
        ServiceLifetime.Scoped);
        //builder => builder.MigrationsAssembly(typeof(PersonSkillsContext).Assembly.FullName)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
