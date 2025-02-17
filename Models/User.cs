using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JitAPI.Models
{
    public class User
    {
        public User()
        {
            
        }
        public User(User newUser)
        {
            FirstName = newUser.FirstName;
            LastName = newUser.LastName;
            Email = newUser.Email;
        }

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

        public Login Login { get; set; }

    }

}
