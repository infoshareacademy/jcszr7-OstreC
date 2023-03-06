using Microsoft.EntityFrameworkCore;
using OstreCWEB.Repository.DataBase;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Repository.Repository.Characters
{
    internal class StatusRepository : IStatusRepository
    {
        private readonly OstreCWebContext _db;
        public StatusRepository(OstreCWebContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Status status)
        {
            _db.Statuses.AddAsync(status);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Status status)
        {
            _db.Statuses.Remove(status);

            await _db.SaveChangesAsync();
        }

        public async Task<List<Status>> GetAllAsync()
        {

            return await _db.Statuses.Include(p => p.CharacterActions).ToListAsync();
        }

        public async Task<Status> GetByIdAsync(int id)
        {
            return await _db.Statuses.Include(s => s.Id == id).SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateAsync(Status status)
        {
            _db.Statuses.Update(status);
            await _db.SaveChangesAsync();
        }
    }
}
