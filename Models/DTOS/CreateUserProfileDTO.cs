namespace JitAPI.Models.DTOS;

public class CreateUserProfileDTO
{
    public Guid UserId { get; set; }
    public string? Title { get; set; }
    public string? AvatarUrl { get; set; }
    public string? Bio { get; set; }
    public string? City { get; set; }
    public string? StateOrProvince { get; set; }
    public string? Country { get; set; }
}