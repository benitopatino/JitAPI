using JitAPI.Models.Interface;

namespace JitAPI.Models.Services;

public class UserProfileService : IUserProfileService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly INewsfeedService _newsfeedService;

    public UserProfileService(IUnitOfWork unitOfWork, INewsfeedService newsfeedService)
    {
        _unitOfWork = unitOfWork;
        _newsfeedService = newsfeedService;
    }

    public UserProfileDTO GetUserProfile(string username)
    {
        if(string.IsNullOrEmpty(username))
            throw new ArgumentNullException(nameof(username));
        
        var userInfo = _unitOfWork.UserRepository.GetAll()
            .SingleOrDefault(u => u.Username == username);
        if(userInfo == null)
            throw new KeyNotFoundException("User not found");
        
        var newsfeedItems = _newsfeedService.GetNewsfeed(userInfo.UserId);
       
        return new UserProfileDTO()
        {
            FirstName = userInfo.FirstName,
            LastName = userInfo.LastName,
            Username = userInfo.Username,
            NewsfeedItems = newsfeedItems.ToList()
        };
    }
}