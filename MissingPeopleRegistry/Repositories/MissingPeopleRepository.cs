using Microsoft.EntityFrameworkCore;
using MissingPeopleRegistry.Models;

namespace MissingPeopleRegistry.Repositories
{
    public class MissingPeopleRepository: IMissingPeopleRepository
    {
        private readonly ApplicationDbContext _context;
        public MissingPeopleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(MissingPerson person)
        {
            await _context.MissingPeople.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MissingPerson>> GetAllMissingPeople()
        {
            return await _context.MissingPeople.Where(_ => true).OrderByDescending(x => x.CreateDate).ToListAsync();
        }

        public async Task<MissingPerson> GetMissingPersonById(Guid id)
        {
            return await _context.MissingPeople.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteMissingPersonById(Guid id)
        {
            var missingPerson = await _context.MissingPeople.FirstOrDefaultAsync(x => x.Id == id);
            _context.MissingPeople.Remove(missingPerson);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MissingPerson person)
        {
            _context.MissingPeople.Update(person);
            await _context.SaveChangesAsync();
        }
    }
}