namespace JitAPI.Models.Interface
{
    public interface IJitRepository : IRepository<Jit>
    {
        IQueryable<Jit>GetJitsByUserId(Guid userId);
    }
}
