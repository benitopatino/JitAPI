namespace JitAPI.Models.Interface
{
    public interface IJitRepository : IRepository<Jit>
    {
        IEnumerable<Jit>GetJitsByUserId(Guid userId);
    }
}
