using MissingPeopleRegistry.ViewModels.MissingPeople;

namespace MissingPeopleRegistry.Services
{
    public interface IMissingPeopleService
    {
        Task CreateAsync(MissingPersonVM viewmodel, Guid userId);
        Task<IEnumerable<MissingPersonVM>> GetAllMissingPeople();
        Task<MissingPersonVM> GetMissingPersonById(Guid id);
        Task DeleteMissingPersonById(Guid id);
        Task UpdateAsync(MissingPersonVM model, Guid userId);
    }
}