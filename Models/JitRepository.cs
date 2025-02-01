using JitAPI.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace JitAPI.Models
{
    public class JitRepository : Repository<Jit>, IJitRepository
    {

        public JitRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Jit> GetJitsByUserId(Guid userId)
        {
            var allJits = GetAll()
                .Where(j => j.UserId.Equals(userId));
                

            return allJits;
        }
    }
}
