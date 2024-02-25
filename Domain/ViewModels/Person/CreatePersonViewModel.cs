using Domain.Dtos.Person;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Person
{
    public class CreatePersonViewModel
	{
		[Required]
		[MaxLength(50)]
		public string FirstName { get; set; }
		[Required]
		[MaxLength(50)]
		public string LastName { get; set; }
		[Required]
		[MaxLength(50)]
		public string Email { get; set; }
		public string Description { get; set; }

		public CreatePersonDto ConvertToDto()
		{
			return new CreatePersonDto
			{
				FirstName = this.FirstName,
				LastName = this.LastName,
				Email = this.Email,
				Description = this.Description
			};
		}
	}
}
