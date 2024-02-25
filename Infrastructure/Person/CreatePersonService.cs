using Application.Interfaces.Person;
using Domain.Dtos.Person;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Person
{
    internal class CreatePersonService: ICreatePersonService
	{
		private readonly ApplicationDbContext context;

		public CreatePersonService(ApplicationDbContext context)
        {
			this.context = context;
		}

		public async Task CreatePerson(CreatePersonDto dto)
		{
			await context.Persons.AddAsync(dto.ConvertToDomainModel());
			await context.SaveChangesAsync();
		}
	}
}
