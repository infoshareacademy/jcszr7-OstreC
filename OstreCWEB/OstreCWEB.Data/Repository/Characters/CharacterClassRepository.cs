using Microsoft.EntityFrameworkCore;
using OstreCWEB.Repository.DataBase;
using OstreCWEB.Repository.Repository.Characters.Interfaces;
using OstreCWEB.DomainModels.CharacterModels;
using System.Runtime.CompilerServices;

namespace OstreCWEB.Repository.Repository.Characters
{
    internal class CharacterClassRepository : EntityBaseRepo<PlayableClass>, ICharacterClassRepository<PlayableClass>
    { 
        public CharacterClassRepository(OstreCWebContext context):base(context)
        {
            _context = context;
        } 

        public override async Task DeleteAsync(int id)
        {
            var entity = await base.GetByIdAsync(id,x=>x.ActionsGrantedByClass,x=>x.ItemsGrantedByClass);
            _context.PlayableCharacterClasses.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
