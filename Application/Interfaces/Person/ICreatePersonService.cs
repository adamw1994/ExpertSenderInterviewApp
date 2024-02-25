using Domain.Dtos.Person;

namespace Application.Interfaces.Person
{
    public interface ICreatePersonService
    {
        public Task CreatePerson(CreatePersonDto dto);
    }
}
