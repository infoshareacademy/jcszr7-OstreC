using OstreCWEB.Data.DataBase;
using OstreCWEB.DomainModels.Identity;

namespace OstreCWEB.Data.Repository.SuperAdmin
{

    internal class SuperAdminRepository : ISuperAdminRepository
    {
        public OstreCWebContext _db;
        public SuperAdminRepository(OstreCWebContext db)
        {
            _db = db;
        }

        public void Test()
        {
            var x = _db.Paragraphs.Where(s => s.Id == 2).FirstOrDefault();
            _db.Paragraphs.Remove(x);
            _db.SaveChanges();



        }
        public User GetRandomUser()
        {
            return _db.Users.First();
        }
    }
}
