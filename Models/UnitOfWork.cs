
using JitAPI.Models.Interface;

namespace JitAPI.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IRepository<Jit> JitRepository { get; }
        public IRepository<User> UserRepository { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            JitRepository = new Repository<Jit>(_context);
            UserRepository = new Repository<User>(_context);
        }

        public int Complete() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();
    }
}
