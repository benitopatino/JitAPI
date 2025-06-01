using JitAPI.Models.DTOS;
using JitAPI.Models.Services;

namespace JitAPI.Models.Interface;

public interface IUserProfileService
{
    UserProfileDTO GetUserProfile(string username);
    void UpdateFollowersCount(Guid userId, UpdateAction action);
    void UpdateFolloweeCount(Guid userId, UpdateAction action);
    void CreateUserProfile(CreateUserProfileDTO newUserProfile);
}