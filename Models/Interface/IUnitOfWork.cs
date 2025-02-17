namespace JitAPI.Models.Interface
{

    public interface IUnitOfWork : IDisposable
    {
        IJitRepository JitRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Login> LoginRepository { get; }
        int Complete();
    }
}
