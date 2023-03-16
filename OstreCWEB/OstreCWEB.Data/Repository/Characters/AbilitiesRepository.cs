using Microsoft.EntityFrameworkCore;
using OstreCWEB.Repository.DataBase;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.DomainModels.CharacterModels;
using System.Security.Cryptography.X509Certificates;

namespace OstreCWEB.Repository.Repository.Characters
{
    internal class AbilitiesRepository : EntityBaseRepo<Ability>, IAbilitiesRepository<Ability>
    {
        private OstreCWebContext _db;
        public AbilitiesRepository(OstreCWebContext db) :base(db)
        {
            _db = db;
        }

        public override async Task DeleteAsync(int id)
        {
            var entity = await base.GetByIdAsync(id, x => x.PlayableClass,x=>x.LinkedItems);
            _context.CharacterActions.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
