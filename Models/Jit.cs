using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JitAPI.Models
{
    public class Jit
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(500)")]
        public string Content { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
