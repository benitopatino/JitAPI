using JitAPI.Models.DTOS;

public class UserProfileDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Title { get; set; }
    public string AvatarUrl { get; set; }
    public string Bio { get; set; }
    public string City { get; set; }
    public string StateOrProvince { get; set; }
    public string Country { get; set; }
    public int FollowerCount { get; set; }
    public int FolloweeCount { get; set; }
    public List<NewsfeedItemDTO> NewsfeedItems { get; set; }
}