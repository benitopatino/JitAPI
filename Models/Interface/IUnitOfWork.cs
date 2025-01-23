namespace JitAPI.Models.Interface
{

    public interface IUnitOfWork : IDisposable
    {
        IRepository<Jit> JitRepository { get; }
        IRepository<User> UserRepository { get; }
        int Complete();
    }
}
