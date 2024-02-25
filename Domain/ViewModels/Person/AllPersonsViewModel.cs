using Domain.Dtos.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Person
{
    public class AllPersonsViewModel
    {
        public IEnumerable<PersonDto> Persons { get; set; }

        public AllPersonsViewModel(IEnumerable<PersonDto> persons)
        {
            Persons = persons;
        }
    }
}
