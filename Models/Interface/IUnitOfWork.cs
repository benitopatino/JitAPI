using JitAPI.Models.Follows;
using JitAPI.Models.Follows;

namespace JitAPI.Models.Interface
{

    public interface IUnitOfWork : IDisposable
    {
        IJitRepository JitRepository { get; }
        IRepository<User> UserRepository { get; }       
        IRepository<Login> LoginRepository { get; }
        IRepository<UserFollow> UserFollows { get; }
        int Complete();
    }
}
