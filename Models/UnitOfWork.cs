
using JitAPI.Models.Interface;
using JitAPI.Models.Relationships;

namespace JitAPI.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IJitRepository JitRepository { get; }
        public IRepository<User> UserRepository { get; }
        public IRepository<Login> LoginRepository { get; }
        
        public IRepository<Relationship> Relationships { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            JitRepository = new JitRepository(_context);
            UserRepository = new Repository<User>(_context);
            LoginRepository = new Repository<Login>(_context);
            Relationships = new Repository<Relationship>(_context);
        }

        public int Complete() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();
    }
}
