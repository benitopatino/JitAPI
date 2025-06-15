namespace JitAPI.Models.DTOS;

public class RegisterUserDTO
{
    public string Email { get; set; }
    public string Password { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Username { get; set; }
    public string City { get; set; }
    public string StateOrProvince { get; set; }
    public string Country { get; set; }
}
