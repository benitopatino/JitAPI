using JitAPI.Auth;
using JitAPI.Models.DTOS;
using JitAPI.Models.Interface;

namespace JitAPI.Models.Services;

public class UserRegistrationService
{
    private readonly IAuthService _authService;
    private readonly IUserProfileService _userProfileService;
    private readonly IUnitOfWork _unitOfWork;

    public UserRegistrationService(IAuthService authService, IUserProfileService userProfileService, IUnitOfWork unitOfWork)
    {
        _authService = authService;
        _userProfileService = userProfileService;
        _unitOfWork = unitOfWork;
    }

    public bool RegisterUser(RegisterUserDTO dto)
    {
        var user = new User
        {
            UserId = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Username = dto.Username,
        };

        var success = _authService.Register(user, dto.Password);
        if (!success) return false;

        _userProfileService.CreateUserProfile(new CreateUserProfileDTO()
        {
            UserId = user.UserId,
            City = dto.City,
            StateOrProvince = dto.StateOrProvince,
            Country = dto.Country
        });

        return _unitOfWork.Complete() > 0;
    }
}
