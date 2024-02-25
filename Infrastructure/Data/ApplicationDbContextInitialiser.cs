using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Bogus;
using Domain.Entities;
using System.Security.Cryptography;

namespace Infrastructure.Data
{
    public static class InitialiserExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

            await initialiser.InitialiseAsync();

            await initialiser.SeedAsync();
        }
    }
    internal class ApplicationDbContextInitialiser
    {
        private readonly ApplicationDbContext context;

        public ApplicationDbContextInitialiser(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task InitialiseAsync()
        {
            await context.Database.MigrateAsync();
        }

        public async Task SeedAsync()
        {
            await TrySeedAsync();
        }

        public async Task TrySeedAsync()
        {
            if (!context.Persons.Any())
            {
                var emailsGenerator = new Faker<PersonEmail>()
                    .RuleFor(e => e.Email, f => f.Person.Email);

                var personsGenerator = new Faker<Domain.Entities.Person>()
                    .RuleFor(p => p.FirstName, f => f.Person.FirstName)
                    .RuleFor(p => p.LastName, f => f.Person.LastName)
                    .RuleFor(p => p.Description, f => f.Music.Genre())
                    .RuleFor(p => p.Emails,f => emailsGenerator.Generate(RandomNumberGenerator.GetInt32(5)).ToList());

                var persons = personsGenerator.Generate(10);

                await context.AddRangeAsync(persons);
                await context.SaveChangesAsync();
            }
        }
    }
}
