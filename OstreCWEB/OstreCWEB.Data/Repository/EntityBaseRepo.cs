using Microsoft.EntityFrameworkCore;
using OstreCWeb.DomainModels;
using OstreCWEB.Repository.DataBase;

namespace OstreCWEB.Repository.Repository
{
    public class EntityBaseRepo<T> : IEntityBaseRepo<T> where T:class,IEntityBase
    {
        private OstreCWebContext _context;

        public EntityBaseRepo(OstreCWebContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        { 
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
