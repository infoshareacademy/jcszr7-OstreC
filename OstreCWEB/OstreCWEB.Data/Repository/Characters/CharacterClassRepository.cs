using Microsoft.EntityFrameworkCore;
using OstreCWEB.Repository.DataBase;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Repository.Repository.Characters
{
    internal class CharacterClassRepository : EntityBaseRepo<PlayableClass>, ICharacterClassRepository<PlayableClass>
    {
        OstreCWebContext _context;
        public CharacterClassRepository(OstreCWebContext context):base(context)
        {
            _context = context;
        } 
    }
}
