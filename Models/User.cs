using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JitAPI.Models
{
    public class User
    {
        [Key]

        public Guid UserId { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "varchar(500)")]
        public string Email { get; set; }

        // Foreign 

        [Required]
        public Guid LoginId { get; set; }

        [ForeignKey("Login")]
        public Login Login { get; set; }

    }

}
