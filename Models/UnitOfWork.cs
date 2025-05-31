
using JitAPI.Models.Follows;
using JitAPI.Models.Interface;

namespace JitAPI.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IJitRepository JitRepository { get; }
        public IRepository<User> UserRepository { get; }
        public IRepository<UserProfile> UserProfileRepository { get;}
        public IRepository<Login> LoginRepository { get; }
        
        public IRepository<UserFollow> UserFollowRepository { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            JitRepository = new JitRepository(_context);
            UserRepository = new Repository<User>(_context);
            LoginRepository = new Repository<Login>(_context);
            UserFollowRepository = new Repository<UserFollow>(_context);
        }

        public int Complete() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();
    }
}
