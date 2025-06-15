namespace JitAPI.Models.DTOS;

public class CreateUserProfileDTO
{
    public Guid UserId { get; set; }
    public string? City { get; set; }
    public string? StateOrProvince { get; set; }
    public string? Country { get; set; }
}