using Microsoft.EntityFrameworkCore;
using OstreCWEB.Repository.DataBase;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Repository.Repository.Characters
{
    internal class AbilitiesRepository : EntityBaseRepo<Ability>, IAbilitiesRepository<Ability>
    {
        private OstreCWebContext _db;
        public AbilitiesRepository(OstreCWebContext db) :base(db)
        {
            _db = db;
        }   
    }
}
