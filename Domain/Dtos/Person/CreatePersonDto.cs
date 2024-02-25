using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Person
{
    public class CreatePersonDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }

        public Entities.Person ConvertToDomainModel()
        {
            return new Entities.Person
			{
                FirstName = FirstName,
                LastName = LastName,
                Description = Description,
                Emails = new List<PersonEmail>
                {
                    new PersonEmail {Email = Email}
                }
            };
        }
    }
}
