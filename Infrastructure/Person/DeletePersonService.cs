using Application.Interfaces.Person;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Person
{
    internal class DeletePersonService : IDeletePersonService
    {
        private readonly ApplicationDbContext context;

        public DeletePersonService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task DeletePerson(int id)
        {
            var personToDelete = await context.Persons.FindAsync(id);
            if (personToDelete != null)
            {
                context.Persons.Remove(personToDelete);
                await context.SaveChangesAsync();
            }
        }
    }
}
