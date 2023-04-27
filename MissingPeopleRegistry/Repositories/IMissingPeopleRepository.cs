using MissingPeopleRegistry.Models;

namespace MissingPeopleRegistry.Repositories
{
    public interface IMissingPeopleRepository
    {
        Task CreateAsync(MissingPerson person);
        Task<IEnumerable<MissingPerson>> GetAllMissingPeople();
        Task<MissingPerson> GetMissingPersonById(Guid id);
        Task DeleteMissingPersonById(Guid id);
        Task UpdateAsync(MissingPerson person);
    }
}