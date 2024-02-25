using Domain.Dtos.Person;

namespace Application.Interfaces.Person
{
    public interface IGetPersonService
    {
        public IEnumerable<PersonDto> GetAllPersons();
        public Task<PersonDto> GetPerson(int id);

    }
}
