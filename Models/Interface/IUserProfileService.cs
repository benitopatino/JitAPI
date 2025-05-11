using JitAPI.Models.DTOS;

namespace JitAPI.Models.Interface;

public interface IUserProfileService
{
    UserProfileDTO GetUserProfile(string username);
}