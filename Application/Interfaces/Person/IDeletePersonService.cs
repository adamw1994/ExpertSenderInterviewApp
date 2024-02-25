namespace Application.Interfaces.Person
{
    public interface IDeletePersonService
    {
        public Task DeletePerson(int id);
    }
}
