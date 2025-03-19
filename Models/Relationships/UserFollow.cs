using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JitAPI.Models.Follows
{
    public class UserFollow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }


        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

        public DateTime DateOfFollow { get; set; }

        // Follower

        [Required]
        public Guid UserFollowerId { get; set; }

        [ForeignKey("UserFollowerId")]
        public User UserFollower { get; set; }

        // Followee

        public Guid? UserFolloweeId { get; set; }

        [ForeignKey("UserFolloweeId")]
        public User UserFollowee { get; set; }
    }
}
