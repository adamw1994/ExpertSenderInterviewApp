using Application.Common;
using Application.Interfaces.Person;
using Infrastructure.Data;
using Infrastructure.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(connectionString)
                );

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<ApplicationDbContextInitialiser>();
            services.AddScoped<IGetPersonService, GetPersonService>();
            services.AddScoped<IDeletePersonService, DeletePersonService>();
            services.AddScoped<ICreatePersonService, CreatePersonService>();
            services.AddScoped<IEditPersonService, EditPersonService>();

            return services;
        }
    }
}
