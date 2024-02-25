using Application.Interfaces.Person;
using Domain.Dtos.Person;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Person
{
    public class GetPersonService : IGetPersonService
    {
        private readonly ApplicationDbContext context;

        public GetPersonService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<PersonDto> GetAllPersons()
        {
            var persons = context.Persons
                .Include(p => p.Emails)
                .Select(p => new PersonDto
                { 
                    Id = p.Id,
                    FirstEmail = p.Emails.First().Email,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Description = p.Description
                    
                });
            return persons;
                
        }

		public async Task<PersonDto> GetPerson(int id)
		{
            var person = await context
                .Persons
                .Include(p => p.Emails)
                .FirstOrDefaultAsync(p => p.Id == id);
                
			if (person is not null)
            {
                return new PersonDto
                {
                    Id = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
					FirstEmail = person.Emails.FirstOrDefault()?.Email,
					Description = person.Description
                };
			}
            return null;

		}
	}
}
