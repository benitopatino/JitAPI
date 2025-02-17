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

        [Required]
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }

        public User User { get; set; }

    }
}
