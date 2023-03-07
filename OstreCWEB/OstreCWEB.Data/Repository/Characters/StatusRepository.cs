using Microsoft.EntityFrameworkCore;
using OstreCWEB.Repository.DataBase;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Repository.Repository.Characters
{
    internal class StatusRepository : EntityBaseRepo<Status>,IStatusRepository<Status>
    {
        private readonly OstreCWebContext _db;
        public StatusRepository(OstreCWebContext db):base(db)
        {
            _db = db;
        }

        //public async Task<List<Status>> GetAllAsync()
        //{

        //    return await _db.Statuses.ToListAsync();
        //}
    }
}
