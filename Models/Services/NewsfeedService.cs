using AutoMapper;
using JitAPI.Models.DTOS;
using JitAPI.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace JitAPI.Models;

public class NewsfeedService : INewsfeedService
{
    private readonly IUnitOfWork _unitOfWork;
    public NewsfeedService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IEnumerable<NewsfeedItemDTO> GetNewsfeed(Guid userId, bool isProfile)
    {
        List<NewsfeedItemDTO> newsfeedItems = new List<NewsfeedItemDTO>();
        
        // validate user exists
        if (!_unitOfWork.UserRepository.Exists(userId))
            throw new ArgumentException($"User with user id {userId.ToString()}does not exist. ", nameof(userId));


        if (isProfile) // Get own profile newsfeed
        {
            var userJits = _unitOfWork.JitRepository.GetJitsByUserId(userId)
                .Include(j => j.User)
                .ToList();
            
            newsfeedItems.AddRange(BuildNewsfeed(userJits, userId));
        }
        else // Get newsfeed items of followees
        {
            // Get follows

            var followeeIds = _unitOfWork.UserFollowRepository.GetAll()
                .Where(f => f.UserFollowerId == userId && f.UserFolloweeId != null)
                .Select(f => f.UserFolloweeId.Value)
                .ToList();

            List<Jit> followeeJits = new List<Jit>();
            foreach (var id in followeeIds)
            {
                var userJits = _unitOfWork.JitRepository.GetJitsByUserId(id)
                    .Include(j => j.User)
                    .ToList();
                newsfeedItems.AddRange(BuildNewsfeed(userJits, id));
            }
        }
        return newsfeedItems;
    }

    private IEnumerable<NewsfeedItemDTO> BuildNewsfeed(List<Jit> jits, Guid userId)
    {
        List<NewsfeedItemDTO> newsFeed = new List<NewsfeedItemDTO>();
        UserProfile profile = _unitOfWork.UserProfileRepository.Get(userId);
        foreach (var j in jits)
        {
            newsFeed.Add(new NewsfeedItemDTO(j.Id, j.Content, j.DateCreated, j.UserId, j.User.FirstName + " " +
            j.User.LastName, profile.AvatarUrl));
        }

        return newsFeed;
    }
    
    
    
    
}