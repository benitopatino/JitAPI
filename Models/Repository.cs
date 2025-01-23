using JitAPI.Models;
using JitAPI.Models.Interface;

namespace JitAPI.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext context) => _context = context;
        public T Get(Guid id) => _context.Set<T>().Find(id);
        public IQueryable<T> GetAll() => _context.Set<T>();
        public void Add(T entity) => _context.Set<T>().Add(entity);
        public void Remove(T entity) => _context.Set<T>().Remove(entity);
    }
}
