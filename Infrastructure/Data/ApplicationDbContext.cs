using Application.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Domain.Entities.Person> Persons { get; set; }
        public DbSet<PersonEmail> Emails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Person>(eb =>
            {
                eb.Property(p => p.FirstName).HasMaxLength(50);
                eb.Property(p => p.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<PersonEmail>(eb =>
            {
                eb.Property(p => p.Email).HasMaxLength(50);
            });

            modelBuilder.Entity<Domain.Entities.Person>()
                .HasMany(p => p.Emails)
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.PersonId);
        }
    }
}
