using Application.Interfaces.Person;
using Domain.Dtos.Person;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Person
{
    public class EditPersonService : IEditPersonService
	{
		private readonly ApplicationDbContext context;

		public EditPersonService(ApplicationDbContext context)
        {
			this.context = context;
		}
        public async Task EditPerson(PersonDto dto)
		{
			var person = await context
				.Persons
				.Include(p => p.Emails)
				.FirstOrDefaultAsync(p => p.Id == dto.Id);

			if (person is not null)
			{
				person.FirstName = dto.FirstName;
				person.LastName = dto.LastName;
				person.Description = dto.Description;

				if (person.Emails.Any())
					person.Emails.First().Email = dto.FirstEmail;
				else
					person.Emails.Add(new Domain.Entities.PersonEmail { Email = dto.FirstEmail });

				await context.SaveChangesAsync();
			}

		}
	}
}
