using JitAPI.Models.DTOS;

public class UserProfileDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public List<NewsfeedItemDTO> NewsfeedItems { get; set; }
}