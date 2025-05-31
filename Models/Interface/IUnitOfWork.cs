using JitAPI.Models.Follows;
using JitAPI.Models.Follows;

namespace JitAPI.Models.Interface
{

    public interface IUnitOfWork : IDisposable
    {
        IJitRepository JitRepository { get; }
        IRepository<User> UserRepository { get; }       
        IRepository<UserProfile> UserProfileRepository { get; }
        IRepository<Login> LoginRepository { get; }
        IRepository<UserFollow> UserFollowRepository { get; }
        int Complete();
    }
}
