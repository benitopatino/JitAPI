using JitAPI.Models.DTOS;
using JitAPI.Models.Services;

namespace JitAPI.Models.Interface;

public interface IUserProfileService
{
    UserProfileDTO GetUserProfile(string username);
    UserProfileDTO GetUserProfile(Guid userId);
    /// <summary>
    /// Return a collection of usernames belonging to followees.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    IEnumerable<string> GetFollowees(Guid userId);
    void UpdateFollowersCount(Guid userId, UpdateAction action);
    void UpdateFolloweeCount(Guid userId, UpdateAction action);
    void CreateUserProfile(CreateUserProfileDTO newUserProfile);
    bool UpdateUserProfile(UserProfileUpdateDTO profile, Guid userId);
}