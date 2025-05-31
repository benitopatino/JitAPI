using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JitAPI.Models;

public class UserProfile
{
    [Key]
    [ForeignKey(nameof(User))] 
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Title { get; set; }
    public string AvatarUrl { get; set; }
    public string Bio { get; set; }
    public string City { get; set; }
    public string StateOrProvince { get; set; }
    public string Country { get; set; }
    public int FollowerCount { get; set; }
    public int FolloweeCount { get; set; }

    public User User { get; set; }
}