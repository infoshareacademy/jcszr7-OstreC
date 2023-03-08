using Microsoft.EntityFrameworkCore;
using OstreCWEB.Repository.DataBase;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.DomainModels.CharacterModels;

namespace OstreCWEB.Repository.Repository.Characters
{
    internal class CharacterRaceRepository : EntityBaseRepo<PlayableRace>, ICharacterRaceRepository<PlayableRace>
    {
        private readonly OstreCWebContext _context;
        public CharacterRaceRepository(OstreCWebContext context):base(context)
        {
            _context = context;
        } 
    }
}
