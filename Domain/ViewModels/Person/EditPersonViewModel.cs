using Domain.Dtos.Person;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Person
{
    public class EditPersonViewModel
    {
        public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(50)]
		public string LastName { get; set; }

		[Required]
		[MaxLength(50)]
		public string Email { get; set; }
        public string? Description { get; set; }



		public EditPersonViewModel() { }
		public EditPersonViewModel(PersonDto dto)
        {
            Id = dto.Id;
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            Email = dto.FirstEmail;
            Description = dto.Description;
        }

		public PersonDto ConvertToDto()
		{
			return new PersonDto
			{
                Id = this.Id,
				FirstName = this.FirstName,
				LastName = this.LastName,
				FirstEmail = this.Email,
				Description = this.Description ?? string.Empty
			};
		}


    }
}
