using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JitAPI.Models;

public class UserProfile
{
    [Key]
    [ForeignKey(nameof(User))] 
    public Guid UserId { get; set; }
    
    [Column(TypeName = "varchar(250)")]
    public string Title { get; set; }
    
    [Column(TypeName = "varchar(500)")]
    public string AvatarUrl { get; set; }
    
    [Column(TypeName = "text")]
    public string Bio { get; set; }
    
    [Column(TypeName = "varchar(250)")]
    public string City { get; set; }
    
    [Column(TypeName = "varchar(250)")]
    public string StateOrProvince { get; set; }
    
    [Column(TypeName = "varchar(250)")]
    public string Country { get; set; }
    
    public int FollowerCount { get; set; }
    public int FolloweeCount { get; set; }

    public User User { get; set; }
}