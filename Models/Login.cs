using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JitAPI.Models
{
    public class Login
    {
        [Key]
        public Guid LoginId { get; set; }

        public string PasswordHash { get; set; }
        public DateTime LastLogin { get; set; }

    }
}
