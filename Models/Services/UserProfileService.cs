using JitAPI.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace JitAPI.Models.Services;

public enum UpdateAction
{
    Increase,
    Decrease,
}

public class UserProfileService : IUserProfileService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly INewsfeedService _newsfeedService;

    public UserProfileService(IUnitOfWork unitOfWork, INewsfeedService newsfeedService)
    {
        _unitOfWork = unitOfWork;
        _newsfeedService = newsfeedService;
    }

    public void UpdateFolloweeCount(Guid userId, UpdateAction action)
    {
        var profile = _unitOfWork.UserProfileRepository.Get(userId);
        profile.FolloweeCount = action == UpdateAction.Increase ? profile.FolloweeCount + 1: profile.FolloweeCount - 1;
    }

    public void UpdateFollowersCount(Guid userId, UpdateAction action)
    {
        var profile = _unitOfWork.UserProfileRepository.Get(userId);
        profile.FollowerCount = action == UpdateAction.Increase ? profile.FollowerCount + 1 : profile.FollowerCount - 1;
    }
    
    public UserProfileDTO GetUserProfile(string username)
    {
        if(string.IsNullOrEmpty(username))
            throw new ArgumentNullException(nameof(username));

        var profile = _unitOfWork.UserProfileRepository.GetAll()
            .Include(p => p.User) 
            .AsNoTracking()
            .FirstOrDefault(p => p.Username == username);

        if (profile == null) return null;
        
        var newsfeedItems = _newsfeedService.GetNewsfeed(profile.UserId, true);
        return new UserProfileDTO
        {
            FirstName = profile.User.FirstName,
            LastName = profile.User.LastName,
            Username = profile.Username,
            Title = profile.Title,
            AvatarUrl = profile.AvatarUrl,
            Bio = profile.Bio,
            City = profile.City,
            StateOrProvince = profile.StateOrProvince,
            Country = profile.Country,
            FollowerCount = profile.FollowerCount,
            FolloweeCount = profile.FolloweeCount,
             NewsfeedItems = newsfeedItems.ToList()
        };
    }
}