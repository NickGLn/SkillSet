using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SkillSet.Infrastructure;

namespace Xunit.Microsoft.DependencyInjection.ExampleTests.Fixtures;

public class CustomWebApplicationFactory<TProgram>: WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // remove the existing context configuration
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IDbContextFactory<PersonSkillsContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContextFactory<PersonSkillsContext>(options =>
                options.UseInMemoryDatabase("personSkillsInMemory"));
        });
    }

}