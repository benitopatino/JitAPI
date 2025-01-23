namespace JitAPI.Models.Interface
{
    public interface IRepository<T> where T : class
    {
        T Get(Guid id);
        IQueryable<T> GetAll();
        void Add(T entity);
        void Remove(T entity);
    }
}
