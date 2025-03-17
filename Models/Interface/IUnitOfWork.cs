using JitAPI.Models.Relationships;

namespace JitAPI.Models.Interface
{

    public interface IUnitOfWork : IDisposable
    {
        IJitRepository JitRepository { get; }
        IRepository<User> UserRepository { get; }       
        IRepository<Login> LoginRepository { get; }
        IRepository<Relationship> Relationships { get; }
        int Complete();
    }
}
