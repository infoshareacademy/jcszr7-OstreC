namespace OstreCWEB.Repository.Repository;

public interface IEntityBaseRepo <T> where T : class
{
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<T> GetByIdAsync(int id);
}