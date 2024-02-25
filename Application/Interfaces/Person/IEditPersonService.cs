using Domain.Dtos.Person;

namespace Application.Interfaces.Person
{
    public interface IEditPersonService
    {
        Task EditPerson(PersonDto dto);
    }
}
