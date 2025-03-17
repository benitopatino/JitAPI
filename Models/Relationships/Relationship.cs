using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JitAPI.Models.Relationships
{
    public class Relationship
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }


        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

        public DateTime DateOfFollow { get; set; }

        // Follower

        [Required]
        public Guid FollowerId { get; set; }

        [ForeignKey("FollowerId")]
        public User Follower { get; set; }

        // Followee

        [Required]
        public Guid FolloweeId { get; set; }

        [ForeignKey("FolloweeId")]
        public User Followee { get; set; }
    }
}
