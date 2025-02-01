namespace JitAPI.Models.Interface
{

    public interface IUnitOfWork : IDisposable
    {
        IJitRepository JitRepository { get; }
        IRepository<User> UserRepository { get; }
        int Complete();
    }
}
